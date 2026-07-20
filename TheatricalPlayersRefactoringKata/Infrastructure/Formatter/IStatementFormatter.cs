using TheatricalPlayersRefactoringKata.Domain.Model;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Formatter
{
	public interface IStatementFormatter
	{
		string Format(StatementData statementData);
	}
}
