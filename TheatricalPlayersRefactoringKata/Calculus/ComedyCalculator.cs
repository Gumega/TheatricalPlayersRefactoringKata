using System;
using TheatricalPlayersRefactoringKata.Abstract;
using TheatricalPlayersRefactoringKata.Info;

namespace TheatricalPlayersRefactoringKata.Calculus
{
	public class ComedyCalculator : PlayCalculator
	{
		public override float CalculatePrice(Performance performance, Play play)
		{
			base.CalculatePrice(performance, play);
			price += (performance.Audience * 3) + (performance.Audience > 20 ? 100 + ((performance.Audience - 20) * 5) : 0);
			return price;
		}

		public override int CalculateCredit(Performance performance, Play play)
		{
			base.CalculateCredit(performance, play);
			credit += (int)Math.Floor((decimal)performance.Audience / 5);
			return credit;
		}
	}
}
