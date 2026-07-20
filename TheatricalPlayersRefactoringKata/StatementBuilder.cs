using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Abstract;
using TheatricalPlayersRefactoringKata.Calculus;
using TheatricalPlayersRefactoringKata.DTO;
using TheatricalPlayersRefactoringKata.Info;

namespace TheatricalPlayersRefactoringKata
{
	public class StatementBuilder
	{
		public static StatementData CreateModel(Invoice invoice, Dictionary<string, Play> playDictionary)
		{
			List<PerformanceResult> resultList = [];
			float totalPrice = 0;
			int totalCredits = 0;

			foreach (Performance performance in invoice.Performances)
			{
				Play play = playDictionary[performance.PlayId];
				PlayCalculator calculator = PlayCalculatorManager.GetPlayCalculator(play.Type);

				float price = calculator.CalculatePrice(performance, play);
				int credits = calculator.CalculateCredit(performance, play);

				totalPrice += price;
				totalCredits += credits;

				resultList.Add(new PerformanceResult(play.Name, price, credits, performance.Audience));
			}

			return new StatementData(invoice.Customer, resultList, totalPrice, totalCredits);
		}
	}
}
