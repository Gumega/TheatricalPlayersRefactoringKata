using FluentAssertions;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Application;
using TheatricalPlayersRefactoringKata.Domain.Model;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Application
{
	public class StatementCalculatorTests
	{
		[Fact]
		public void Calculate_ShouldConsolidateMultiplePerformancesCorrectly()
		{
			Invoice invoice = new("BigCo",
			[
				new Performance("hamlet", 55),
				new Performance("as-like", 35)
			]);

			var plays = new Dictionary<string, Play>
			{
				{ "hamlet", new Play("Hamlet", 1000, "tragedy") },
				{ "as-like", new Play("As You Like It", 1000, "comedy") }
			};

			var result = StatementBuilder.CreateModel(invoice, plays);

			result.Customer.Should().Be("BigCo");
			result.PerformanceResultList.Should().HaveCount(2);
			result.TotalPrice.Should().BeGreaterThan(0);
			result.TotalCredits.Should().BeGreaterThan(0);
		}
	}
}
