using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Markr.DataHandling.Data {
    [XmlRoot(ElementName = "mcq-test-results")]
    public class McqTestResults {

        [XmlElement(ElementName = "mcq-test-result")]
        public McqTestResult[] McqTestResult { get; set; }
    }
}
