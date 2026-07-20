using System.Collections.Generic;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.DTO
{
	[XmlRoot("Statement")]
	public class StatementXMLDTO
	{
		public string Customer { get; set; }
		[XmlArray("Items")]
		[XmlArrayItem("Item")]
		public List<XmlStatementItem> Items { get; set; } = [];
		public float AmountOwed { get; set; }
		public int EarnedCredits { get; set; }
	}

	public class XmlStatementItem
	{
		public float AmountOwed { get; set; }
		public int EarnedCredits { get; set; }
		public int Seats { get; set; }
	}
}
