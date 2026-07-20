using FluentAssertions;
using TheatricalPlayersRefactoringKata.Domain.Model;
using TheatricalPlayersRefactoringKata.Infrastructure.Formatter;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Infrastructure
{
	public class XmlStatementFormatterTests
	{
		[Fact]
		public void Format_ShouldGenerateValidXmlMatchingExpectedSchema()
		{
			var formatter = new XMLStatementFormatter();
			var data = new StatementData(
				"BigCo",
				[
					new PerformanceResult("Hamlet", 650m, 25, 55)
				],
				650m,
				25
			);

			string xmlResult = formatter.Format(data);

			xmlResult.Should().Contain("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
			xmlResult.Should().Contain("<Customer>BigCo</Customer>");
			xmlResult.Should().Contain("<AmountOwed>650</AmountOwed>");
			xmlResult.Should().Contain("<EarnedCredits>25</EarnedCredits>");
			xmlResult.Should().Contain("<Seats>55</Seats>");
		}
	}
}
