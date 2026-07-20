using System;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain.Model;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Formatter
{
	public class TextStatementFormatter : IStatementFormatter
	{
		private readonly CultureInfo cultureInfo = new("en-US");

		public string Format(StatementData statementData)
		{
			string result = $"Statement for {statementData.Customer}\n";
			foreach (PerformanceResult performance in statementData.PerformanceResultList)
			{
				result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", performance.PlayName, performance.Price, performance.Audience);
			}
			result += String.Format(cultureInfo, "Amount owed is {0:C}\n", statementData.TotalPrice);
			result += $"You earned {statementData.TotalCredits} credits\n";
			return result;
		}
	}
}
