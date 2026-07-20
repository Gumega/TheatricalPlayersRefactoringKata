using TheatricalPlayersRefactoringKata.Domain.Model;

namespace TheatricalPlayersRefactoringKata.Domain.Services
{
	public class HistoryCalculator : PlayCalculator
	{
		public override decimal CalculatePrice(Performance performance, Play play)
		{
			BasePrice(play.Lines);
			price *= 2m;
			if (performance.Audience > 30)
				price += 10m * (performance.Audience - 30);

			price += (performance.Audience * 3m) + (performance.Audience > 20 ? 100m + ((performance.Audience - 20) * 5m) : 0m);
			return price;
		}
	}
}
