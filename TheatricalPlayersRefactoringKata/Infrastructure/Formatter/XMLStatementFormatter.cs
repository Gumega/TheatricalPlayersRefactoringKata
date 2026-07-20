using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata.Domain.Model;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Formatter
{
	[XmlRoot("Statement")]
	public class StatementXMLDTO
	{
		public string Customer { get; set; }
		[XmlArray("Items")]
		[XmlArrayItem("Item")]
		public List<XmlStatementItem> Items { get; set; } = [];
		public decimal AmountOwed { get; set; }
		public int EarnedCredits { get; set; }
	}

	public class XmlStatementItem
	{
		public decimal AmountOwed { get; set; }
		public int EarnedCredits { get; set; }
		public int Seats { get; set; }
	}

	public class XMLStatementFormatter : IStatementFormatter
	{
		public string Format(StatementData statementData)
		{
			var xmlData = new StatementXMLDTO
			{
				Customer = statementData.Customer,
				AmountOwed = statementData.TotalPrice,
				EarnedCredits = statementData.TotalCredits,
				Items = statementData.PerformanceResultList.Select(p => new XmlStatementItem
				{
					AmountOwed = p.Price,
					EarnedCredits = p.Credits,
					Seats = p.Audience
				}).ToList()
			};

			XmlSerializer serializer = new(typeof(StatementXMLDTO));

			var settings = new XmlWriterSettings
			{
				Encoding = new UTF8Encoding(false),
				Indent = true,
				IndentChars = "  "
			};

			using StringWriterWithEncoding stringWriter = new(Encoding.UTF8);
			using XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings);
			serializer.Serialize(xmlWriter, xmlData);
			return stringWriter.ToString();
		}

		public class StringWriterWithEncoding(Encoding encoding) : StringWriter()
		{
			public override Encoding Encoding => encoding;
		}
	}
}
