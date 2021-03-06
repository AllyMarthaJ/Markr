using System;
using System.Xml.Serialization;

namespace Markr.DataHandling.Data {
    [XmlRoot(ElementName = "mcq-test-result")]
    public class McqTestResult {
        [XmlElement(ElementName = "first-name")]
        public string FirstName { get; set; }

        [XmlElement(ElementName = "last-name")]
        public string LastName { get; set; }

        [XmlElement(ElementName = "student-number")]
        public string StudentNumber { get; set; }

        [XmlElement(ElementName = "test-id")]
        public int TestId { get; set; }

        //[XmlElement(ElementName = "answer")]
        //public Answer[] Answer { get; set; }

        [XmlElement(ElementName = "summary-marks")]
        public SummaryMarks SummaryMarks { get; set; }

        [XmlAttribute(AttributeName = "scanned-on")]
        public DateTime ScannedOn { get; set; }

        [XmlText]
        public string Text { get; set; }

        public McqResultDb ToDatabaseData() {
            return new McqResultDb() {
                FirstName = FirstName,
                LastName = LastName,
                StudentNumber = StudentNumber,
                TestId = TestId,
                AvailableMarks = SummaryMarks.Available,
                ObtainedMarks = SummaryMarks.Obtained,
                //ScannedOn = ScannedOn
            };
        }
    }
}
