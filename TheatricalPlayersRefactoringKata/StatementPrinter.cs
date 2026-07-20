using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Application;
using TheatricalPlayersRefactoringKata.Domain.Model;
using TheatricalPlayersRefactoringKata.Infrastructure.Formatter;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
	public static string GenerateStatement(Invoice invoice, Dictionary<string, Play> plays, IStatementFormatter formatter)
	{
		StatementData data = StatementBuilder.CreateModel(invoice, plays);
		return formatter.Format(data);
	}
}
