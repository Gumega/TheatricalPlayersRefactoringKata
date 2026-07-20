using TheatricalPlayersRefactoringKata.Domain.Model;

namespace TheatricalPlayersRefactoringKata.Domain.Services
{
	public class TragedyCalculator : PlayCalculator
	{
		public override decimal CalculatePrice(Performance performance, Play play)
		{
			base.CalculatePrice(performance, play);
			if (performance.Audience > 30)
				price += 10m * (performance.Audience - 30);
			return price;
		}
	}
}
