using System;
using System.Xml.Serialization;

namespace Markr.DataHandling {
	[XmlRoot(ElementName = "mcq-test-result")]
	public class McqTestResult {

		[XmlElement(ElementName = "first-name")]
		public string Firstname { get; set; }

		[XmlElement(ElementName = "last-name")]
		public string Lastname { get; set; }

		[XmlElement(ElementName = "student-number")]
		public int Studentnumber { get; set; }

		[XmlElement(ElementName = "test-id")]
		public int Testid { get; set; }

		[XmlElement(ElementName = "answer")]
		public Answer[] Answer { get; set; }

		[XmlElement(ElementName = "summary-marks")]
		public SummaryMarks SummaryMarks { get; set; }

		[XmlAttribute(AttributeName = "scanned-on")]
		public DateTime ScannedOn { get; set; }

		[XmlText]
		public string Text { get; set; }
	}
}
