using System;
using TheatricalPlayersRefactoringKata.Domain.Model;

namespace TheatricalPlayersRefactoringKata.Domain.Services
{
	public abstract class PlayCalculator
	{
		protected decimal price = 0;
		protected int credit = 0;

		protected void BasePrice(int lines)
		{
			price = (decimal)Math.Clamp(lines, 1000, 4000) / 10m;
		}

		protected void BaseCredit(int audience)
		{
			credit = Math.Max(audience - 30, 0);
		}

		public virtual decimal CalculatePrice(Performance performance, Play play)
		{
			BasePrice(play.Lines);
			return price;
		}
		
		public virtual int CalculateCredit(Performance performance, Play play)
		{
			BaseCredit(performance.Audience);
			return credit;
		}
	}
}
