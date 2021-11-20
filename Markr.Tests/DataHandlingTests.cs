using Markr.DataHandling.Data;
using NUnit.Framework;
using System;

namespace Markr.Tests {
    public class DataHandlingTests {
        [SetUp]
        public void Setup() {
        }

        /// <summary>
        /// Attempts translation of data from input to database format.
        /// </summary>
        [Test]
        public void TranslateGoodMcqResult() {
            var result = new McqTestResult() {
                FirstName = "Bob",
                LastName = "TheBuilder",
                ScannedOn = System.DateTime.UtcNow,
                StudentNumber = 1,
                SummaryMarks = new SummaryMarks() { Available = 10, Obtained = 5 },
                TestId = 1,
                Text = null
            };

            var translation = new McqResultDb() {
                FirstName = "Bob",
                LastName = "TheBuilder",
                StudentNumber = 1,
                ObtainedMarks = 5,
                AvailableMarks = 10,
                TestId = 1,
                ResultId = 0
            };
            var converted = result.ToDatabaseData();

            Assert.AreEqual(converted.FirstName, translation.FirstName);
            Assert.AreEqual(converted.LastName, translation.LastName);
            Assert.AreEqual(converted.StudentNumber, translation.StudentNumber);
            Assert.AreEqual(converted.ObtainedMarks, translation.ObtainedMarks);
            Assert.AreEqual(converted.AvailableMarks, translation.AvailableMarks);
            Assert.AreEqual(converted.TestId, translation.TestId);
            Assert.AreEqual(converted.ResultId, translation.ResultId);
        }

        /// <summary>
        /// Tests whether incomplete data is detected or not (should be).
        /// </summary>
        [Test]
        public void TranslateBadMcqResult() {
            var result = new McqTestResult() {
                FirstName = "Bob",
                ScannedOn = System.DateTime.UtcNow,
                StudentNumber = 1,
                SummaryMarks = new SummaryMarks() { Available = 10, Obtained = 5 },
                TestId = 1,
                Text = null
            };

            Assert.Throws<FormatException>(() => result.ToDatabaseData());
        }

        /// <summary>
        /// Tests whether aggregation calculations are correct.
        /// </summary>
        [Test]
        public void IsAggregateAccurate() {
            McqResultDb[] mathResults = new McqResultDb[] {
                new McqResultDb() {StudentNumber = 1, AvailableMarks = 10, ObtainedMarks= 4 },
                new McqResultDb() {StudentNumber = 2, AvailableMarks = 10, ObtainedMarks= 5 },
                new McqResultDb() {StudentNumber = 3, AvailableMarks = 10, ObtainedMarks= 1 },
                new McqResultDb() {StudentNumber = 4, AvailableMarks = 10, ObtainedMarks= 2 },
                new McqResultDb() {StudentNumber = 5, AvailableMarks = 10, ObtainedMarks= 3 }
            };
            var aggregate = TestAggregate.FormatTest(mathResults);

            Assert.AreEqual(5, aggregate.Count);
            Assert.AreEqual(10, aggregate.Min);
            Assert.AreEqual(50, aggregate.Max);
            Assert.AreEqual(30, aggregate.Mean);
            Assert.LessOrEqual(Math.Abs(aggregate.StandardDeviation - 14), 1);
            Assert.AreEqual(20, aggregate.P25);
            Assert.AreEqual(30, aggregate.P50);
            Assert.AreEqual(40, aggregate.P75);
        }

        /// <summary>
        /// Tests whether the aggregation filtering works by picking the students' highest scores only.
        /// Then tests correctness.
        /// </summary>
        [Test]
        public void IsAggregateFiltering() {
            McqResultDb[] mathResults = new McqResultDb[] {
                new McqResultDb() {StudentNumber = 1, AvailableMarks = 10, ObtainedMarks= 4 },
                new McqResultDb() {StudentNumber = 1, AvailableMarks = 10, ObtainedMarks= 5 },
                new McqResultDb() {StudentNumber = 1, AvailableMarks = 10, ObtainedMarks= 1 },
                new McqResultDb() {StudentNumber = 2, AvailableMarks = 10, ObtainedMarks= 1 },
                new McqResultDb() {StudentNumber = 2, AvailableMarks = 10, ObtainedMarks= 2 }
            };
            var aggregate = TestAggregate.FormatTest(mathResults);

            Assert.AreEqual(2, aggregate.Count);
            Assert.AreEqual(20, aggregate.Min);
            Assert.AreEqual(50, aggregate.Max);

            //Assert.AreEqual(35, aggregate.Mean);
            //Assert.LessOrEqual(Math.Abs(aggregate.StandardDeviation - 15), 1);
            //Assert.AreEqual(20, aggregate.P25);
            //Assert.AreEqual(20, aggregate.P50);
            //Assert.AreEqual(50, aggregate.P75);
        }
    }
}