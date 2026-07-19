using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
	private static readonly CultureInfo cultureInfo = new("en-US");
	public static string Print(Invoice invoice, Dictionary<string, Play> plays)
	{
		int totalAmount = 0;
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

	private static void ProccessPerformance(Performance performance, Play play, ref string result, ref int totalAmount, ref int volumeCredits)
	{
		int lines = play.Lines < 1000 ? 1000 : play.Lines > 4000 ? 4000 : play.Lines;
		int thisAmount = lines / 10;
		// add volume credits
		volumeCredits += Math.Max(performance.Audience - 30, 0);
		switch (play.Type)
		{
			case "tragedy":
				if (performance.Audience > 30)
					thisAmount += 10 * (performance.Audience - 30);
				break;
			case "comedy":
				thisAmount += (performance.Audience * 3) + (performance.Audience > 20 ? 100 + ((performance.Audience - 20) * 5) : 0);
				// add extra credit for every ten comedy attendees
				volumeCredits += (int)Math.Floor((decimal)performance.Audience / 5);
				break;
			case "history":

				break;
			default:
				throw new Exception("unknown type: " + play.Type);
		}

		// print line for this order
		result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount, performance.Audience);
		totalAmount += thisAmount;
	}
}
