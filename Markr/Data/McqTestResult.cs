using System;
using System.ComponentModel;
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

        internal McqResultDb ToDatabaseData() {
            if (String.IsNullOrEmpty(FirstName) ||
                String.IsNullOrEmpty(LastName) ||
                String.IsNullOrEmpty(StudentNumber) ||
                TestId == -1 ||
                SummaryMarks.Available == -1 ||
                SummaryMarks.Obtained == -1 ||
                ScannedOn == default(DateTime)) {

                throw new FormatException("One or more of the fields were malformed.");
            }

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
