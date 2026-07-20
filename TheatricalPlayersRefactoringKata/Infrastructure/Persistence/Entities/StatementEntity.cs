using System;
using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Persistence.Entities
{
	public enum StatementStatus
	{
		Pending,
		Processing,
		Completed,
		Failed
	}

	public class StatementEntity
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Customer { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public StatementStatus Status { get; set; } = StatementStatus.Pending;
		public string? OutputFilePath { get; set; }
		public decimal? TotalAmount { get; set; }
		public int? TotalCredits { get; set; }

		public List<StatementItemEntity> Items { get; set; } = new();
	}

	public class StatementItemEntity
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public Guid StatementId { get; set; }
		public string PlayId { get; set; } = string.Empty;
		public string PlayName { get; set; } = string.Empty;
		public string PlayType { get; set; } = string.Empty;
		public int Audience { get; set; }
		public decimal? Amount { get; set; }
		public int? Credits { get; set; }
	}
}
