using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.DTO
{
	public record StatementData(
		string Customer,
		List<PerformanceResult> PerformanceResultList,
		float TotalPrice,
		int TotalCredits
	);

	public record PerformanceResult(
		string PlayName,
		float Price,
		int Credits,
		int Audience
	);
}
