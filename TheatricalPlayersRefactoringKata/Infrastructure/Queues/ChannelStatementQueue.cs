using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Queues
{
	public interface IStatementQueue
	{
		ValueTask EnqueueAsync(Guid statementId, CancellationToken cancellationToken = default);
		ValueTask<Guid> DequeueAsync(CancellationToken cancellationToken = default);
	}

	public class ChannelStatementQueue : IStatementQueue
	{
		private readonly Channel<Guid> _queue = Channel.CreateUnbounded<Guid>(new UnboundedChannelOptions
		{
			SingleReader = true
		});

		public ValueTask EnqueueAsync(Guid statementId, CancellationToken cancellationToken = default) =>
			_queue.Writer.WriteAsync(statementId, cancellationToken);

		public ValueTask<Guid> DequeueAsync(CancellationToken cancellationToken = default) =>
			_queue.Reader.ReadAsync(cancellationToken);
	}
}
