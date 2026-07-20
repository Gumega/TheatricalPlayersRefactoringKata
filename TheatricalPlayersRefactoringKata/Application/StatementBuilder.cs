using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Domain.Model;
using TheatricalPlayersRefactoringKata.Domain.Services;

namespace TheatricalPlayersRefactoringKata.Application
{
	public class StatementBuilder
	{
		public static StatementData CreateModel(Invoice invoice, Dictionary<string, Play> playDictionary)
		{
			List<PerformanceResult> resultList = [];
			decimal totalPrice = 0m;
			int totalCredits = 0;

			foreach (Performance performance in invoice.Performances)
			{
				//Play play = playDictionary[performance.PlayId];
				if (!playDictionary.TryGetValue(performance.PlayId, out Play play))
					throw new KeyNotFoundException($"Play '{performance.PlayId}' not found.");
				PlayCalculator calculator = PlayCalculatorManager.GetPlayCalculator(play.Type);

				decimal price = calculator.CalculatePrice(performance, play);
				int credits = calculator.CalculateCredit(performance, play);

				totalPrice += price;
				totalCredits += credits;

				resultList.Add(new PerformanceResult(play.Name, price, credits, performance.Audience));
			}

			return new StatementData(invoice.Customer, resultList, totalPrice, totalCredits);
		}
	}
}
