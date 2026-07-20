using TheatricalPlayersRefactoringKata.Abstract;
using TheatricalPlayersRefactoringKata.Info;

namespace TheatricalPlayersRefactoringKata.Calculus
{
	public class TragedyCalculator : PlayCalculator
	{
		public override float CalculatePrice(Performance performance, Play play)
		{
			base.CalculatePrice(performance, play);
			if (performance.Audience > 30)
				price += 10 * (performance.Audience - 30);
			return price;
		}
	}
}
