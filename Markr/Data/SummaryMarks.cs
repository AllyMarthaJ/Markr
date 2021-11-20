using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Markr.DataHandling.Data {
	[XmlRoot(ElementName = "summary-marks")]
	public class SummaryMarks {

		[XmlAttribute(AttributeName = "available")]
		public int Available { get; set; }

		[XmlAttribute(AttributeName = "obtained")]
		public int Obtained { get; set; }
	}
}
