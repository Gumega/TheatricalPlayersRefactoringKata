using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.DTO;
using TheatricalPlayersRefactoringKata.Info;
using TheatricalPlayersRefactoringKata.Interface;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
	public static string GenerateStatement(Invoice invoice, Dictionary<string, Play> plays, IStatementFormatter formatter)
	{
		StatementData data = StatementBuilder.CreateModel(invoice, plays);
		return formatter.Format(data);
	}
}
