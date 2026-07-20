using System;
using TheatricalPlayersRefactoringKata.Domain.Model;

namespace TheatricalPlayersRefactoringKata.Domain.Services
{
	public class ComedyCalculator : PlayCalculator
	{
		public override decimal CalculatePrice(Performance performance, Play play)
		{
			base.CalculatePrice(performance, play);
			price += (performance.Audience * 3m) + (performance.Audience > 20 ? 100m + ((performance.Audience - 20) * 5m) : 0m);
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
