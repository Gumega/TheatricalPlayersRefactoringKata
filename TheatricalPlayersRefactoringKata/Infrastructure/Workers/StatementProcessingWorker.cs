using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application;
using TheatricalPlayersRefactoringKata.Domain.Model;
using TheatricalPlayersRefactoringKata.Infrastructure.Formatter;
using TheatricalPlayersRefactoringKata.Infrastructure.Persistence;
using TheatricalPlayersRefactoringKata.Infrastructure.Persistence.Entities;
using TheatricalPlayersRefactoringKata.Infrastructure.Queues;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Workers
{
	public class StatementProcessingWorker : BackgroundService
	{
		private readonly IStatementQueue _queue;
		private readonly IServiceScopeFactory _scopeFactory;
		private readonly ILogger<StatementProcessingWorker> _logger;
		private readonly string _outputDirectory;

		public StatementProcessingWorker(
			IStatementQueue queue,
			IServiceScopeFactory scopeFactory,
			ILogger<StatementProcessingWorker> logger)
		{
			_queue = queue;
			_scopeFactory = scopeFactory;
			_logger = logger;

			_outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedStatements");
			Directory.CreateDirectory(_outputDirectory);
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("Worker de processamento de extratos iniciado.");

			while (!stoppingToken.IsCancellationRequested)
			{
				try
				{
					var statementId = await _queue.DequeueAsync(stoppingToken);
					await ProcessStatementAsync(statementId, stoppingToken);
				}
				catch (OperationCanceledException)
				{
					break;
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Erro ao processar item da fila.");
				}
			}
		}

		private async Task ProcessStatementAsync(Guid statementId, CancellationToken ct)
		{
			using IServiceScope scope = _scopeFactory.CreateScope();
			AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
			IStatementFormatter xmlFormatter = scope.ServiceProvider.GetRequiredService<IStatementFormatter>();

			StatementEntity entity = await db.Statements
				.Include(s => s.Items)
				.FirstOrDefaultAsync(s => s.Id == statementId, ct);

			if (entity == null) return;

			try
			{
				entity.Status = StatementStatus.Processing;
				await db.SaveChangesAsync(ct);

				Invoice invoice = new Invoice(
					entity.Customer,
					entity.Items.Select(i => new Performance(i.PlayId, i.Audience)).ToList()
				);

				Dictionary<string, Play> plays = entity.Items.ToDictionary(
					i => i.PlayId,
					i => new Play(i.PlayName, 1000, i.PlayType)
				);

				StatementData statementData = StatementBuilder.CreateModel(invoice, plays);

				string xmlContent = xmlFormatter.Format(statementData);

				string filePath = Path.Combine(_outputDirectory, $"Statement_{entity.Id}.xml");
				await File.WriteAllTextAsync(filePath, xmlContent, ct);

				entity.Status = StatementStatus.Completed;
				entity.OutputFilePath = filePath;
				entity.TotalAmount = statementData.TotalPrice;
				entity.TotalCredits = statementData.TotalCredits;

				for (int i = 0; i < entity.Items.Count; i++)
				{
					entity.Items[i].Amount = statementData.PerformanceResultList[i].Price;
					entity.Items[i].Credits = statementData.PerformanceResultList[i].Credits;
				}

				await db.SaveChangesAsync(ct);
				_logger.LogInformation("Extrato {StatementId} processado com sucesso em {FilePath}", statementId, filePath);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Falha ao processar extrato {StatementId}", statementId);
				entity.Status = StatementStatus.Failed;
				await db.SaveChangesAsync(ct);
			}
		}
	}
}
