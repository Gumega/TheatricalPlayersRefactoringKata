using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Abstract;
using TheatricalPlayersRefactoringKata.Calculus;
using TheatricalPlayersRefactoringKata.Info;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
	private static readonly CultureInfo cultureInfo = new("en-US");
	public static string Print(Invoice invoice, Dictionary<string, Play> plays)
	{
		float totalAmount = 0;
		int volumeCredits = 0;
		string result = string.Format("Statement for {0}\n", invoice.Customer);

		foreach (Performance performance in invoice.Performances)
		{
			ProccessPerformance(performance, plays[performance.PlayId], ref result, ref totalAmount, ref volumeCredits);
		}
		result += String.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
		result += String.Format("You earned {0} credits\n", volumeCredits);
		return result;
	}

	private static void ProccessPerformance(Performance performance, Play play, ref string result, ref float totalAmount, ref int volumeCredits)
	{
		PlayCalculator playCalculator = PlayCalculatorManager.GetPlayCalculator(play.Type);

		float thisPrice = playCalculator.CalculatePrice(performance, play);
		volumeCredits += playCalculator.CalculateCredit(performance, play);

		// print line for this order
		result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisPrice, performance.Audience);
		totalAmount += thisPrice;
	}
}
