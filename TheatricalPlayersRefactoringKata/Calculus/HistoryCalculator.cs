using TheatricalPlayersRefactoringKata.Abstract;
using TheatricalPlayersRefactoringKata.Info;

namespace TheatricalPlayersRefactoringKata.Calculus
{
	public class HistoryCalculator : PlayCalculator
	{
		public override float CalculatePrice(Performance performance, Play play)
		{
			BasePrice(play.Lines);
			price *= 2;
			if (performance.Audience > 30)
				price += 10 * (performance.Audience - 30);

			price += (performance.Audience * 3) + (performance.Audience > 20 ? 100 + ((performance.Audience - 20) * 5) : 0);
			return price;
		}
	}
}
