using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Markr.DataHandling.Data {
	[XmlRoot(ElementName = "summary-marks")]
	public class SummaryMarks {

		[XmlAttribute(AttributeName = "available")]
		[DefaultValue(-1)]
		public double Available { get; set; }

		[XmlAttribute(AttributeName = "obtained")]
		[DefaultValue(-1)]
		public double Obtained { get; set; }
	}
}
