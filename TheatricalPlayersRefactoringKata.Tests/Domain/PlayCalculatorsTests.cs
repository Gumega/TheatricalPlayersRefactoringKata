using System;
using FluentAssertions;
using TheatricalPlayersRefactoringKata.Domain.Model;
using TheatricalPlayersRefactoringKata.Domain.Services;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Domain
{
	public class PlayCalculatorsTests
	{
		[Theory]
		[InlineData(800, 20, 100)]   //Min lines (clamped para 1000) -> 1000/10 = 100. Without audience bonus.
		[InlineData(1000, 30, 100)]  // Exactly 30 people -> No extra charge
		[InlineData(1000, 35, 150)]  // 35 people -> 100 + (5 * 10) = 150
		public void TragedyCalculator_CalculateAmount_ShouldCalculateCorrectly(int lines, int audience, decimal expectedAmount)
		{
			var calculator = new TragedyCalculator();
			var play = new Play("Hamlet", lines, "tragedy");
			var perf = new Performance("hamlet", audience);

			var amount = calculator.CalculatePrice(perf, play);

			amount.Should().Be(expectedAmount);
		}

		[Fact]
		public void ComedyCalculator_CalculateCredits_ShouldIncludeBonusForEveryFiveAttendees()
		{
			var calculator = new ComedyCalculator();
			var play = new Play("As You Like It", 1000, "comedy");
			var perf = new Performance("like-it", 20);

			var credits = calculator.CalculateCredit(perf, play);

			//Default credits: Math.Max(20 - 30, 0) = 0
			//Bonus credits: floor(20 / 5) = 4
			credits.Should().Be(4);
		}

		[Fact]
		public void HistoryCalculator_ShouldApplyDoublePriceOnLines()
		{
			var calculator = new HistoryCalculator();
			var play = new Play("Henry V", 1000, "history"); // 1000 / 10 * 2 = 200
			var perf = new Performance("henry-v", 20);      // Comedy (20*3) = 60, total = 260

			var amount = calculator.CalculatePrice(perf, play);

			amount.Should().Be(260m);
		}

		[Fact]
		public void PlayCalculatorFactory_WithUnknownType_ShouldThrowArgumentException()
		{
			Action act = () => PlayCalculatorManager.GetPlayCalculator("sci-fi");

			act.Should().Throw<ArgumentException>()
			   .WithMessage("Genre sci-fi is not defined.");
		}
	}
}
