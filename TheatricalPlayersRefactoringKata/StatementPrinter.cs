using System;
using System.Collections.Generic;
using System.Globalization;

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
		int lines = play.Lines < 1000 ? 1000 : play.Lines > 4000 ? 4000 : play.Lines;
		float thisAmount = (float)lines / 10 * (play.Type == "history" ? 2 : 1);
		// add volume credits
		volumeCredits += Math.Max(performance.Audience - 30, 0);
		if (play.Type == "tragedy" || play.Type == "history")
		{
			if (performance.Audience > 30)
				thisAmount += 10 * (performance.Audience - 30);
		}
		if (play.Type == "comedy" || play.Type == "history")
		{
			thisAmount += (performance.Audience * 3) + (performance.Audience > 20 ? 100 + ((performance.Audience - 20) * 5) : 0);
			// add extra credit for every ten comedy attendees
			if (play.Type == "comedy")
				volumeCredits += (int)Math.Floor((decimal)performance.Audience / 5);
		}

		// print line for this order
		result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount, performance.Audience);
		totalAmount += thisAmount;
	}
}
