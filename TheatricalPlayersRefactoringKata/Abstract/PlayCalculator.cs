using System;
using TheatricalPlayersRefactoringKata.Info;

namespace TheatricalPlayersRefactoringKata.Abstract
{
	public abstract class PlayCalculator
	{
		protected float price = 0;
		protected int credit = 0;

		protected void BasePrice(int lines)
		{
			price = (float)Math.Clamp(lines, 1000, 4000) / 10;
		}

		protected void BaseCredit(int audience)
		{
			credit = Math.Max(audience - 30, 0);
		}

		public virtual float CalculatePrice(Performance performance, Play play)
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
