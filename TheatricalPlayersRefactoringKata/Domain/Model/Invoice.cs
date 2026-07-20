using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Domain.Model
{
	public record Invoice(string Customer, List<Performance> Performances);
}
