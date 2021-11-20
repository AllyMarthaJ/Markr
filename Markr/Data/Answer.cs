using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Markr.DataHandling.Data {
	[XmlRoot(ElementName = "answer")]
	public class Answer {

		[XmlAttribute(AttributeName = "question")]
		public int Question { get; set; }

		[XmlAttribute(AttributeName = "marks-available")]
		public int MarksAvailable { get; set; }

		[XmlAttribute(AttributeName = "marks-awarded")]
		public int MarksAwarded { get; set; }

		[XmlText]
		public string Text { get; set; }
	}
}
