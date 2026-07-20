using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Domain.Model
{
	public record StatementData(
		string Customer,
		List<PerformanceResult> PerformanceResultList,
		decimal TotalPrice,
		int TotalCredits
	);

	public record PerformanceResult(
		string PlayName,
		decimal Price,
		int Credits,
		int Audience
	);
}
