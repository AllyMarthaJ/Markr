using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Markr.DataHandling.Data {
    public class TestAggregate {
        [JsonPropertyName("mean")]
        public double Mean { get; set; }

        [JsonPropertyName("stddev")]
        public double StandardDeviation { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("p25")]
        public double P25 { get; set; }

        [JsonPropertyName("p50")]
        public double P50 { get; set; }

        [JsonPropertyName("p75")]
        public double P75 { get; set; }

        [JsonPropertyName("min")]
        public double Min { get; set; }

        [JsonPropertyName("max")]
        public double Max { get; set; }

        public static TestAggregate FormatTest(McqResultDb[] testResults) {
            // Filters data by grouping duplicate students, then selecting by the greatest marks (maxby).
            // Orders remaining data for use in percentiles and gives array.
            var rTestResults = testResults
                .GroupBy(s => s.StudentNumber)
                .Select(g => g.MaxBy(s => s.ObtainedMarks))
                .OrderBy(t => t.ObtainedMarks)
                .ToArray();

            var avMarks = rTestResults.First().AvailableMarks;

            var count = rTestResults.Length;

            // Select all data and average with percentage (used in stddev)
            var mean = rTestResults.Select(t => t.ObtainedMarks).Average() / avMarks * 100;

            // Select all data and use StdDev formula to calculate deviation. Automatically deals in percentages.
            var stdDev = Math.Sqrt(rTestResults.Select(t => Math.Pow(t.ObtainedMarks / avMarks * 100 - mean, 2)).Sum() / count);

            // Percentiles. Could just call a function here.
            var p25Index = (int)Math.Ceiling(25d / 100 * rTestResults.Length) - 1;
            var p50Index = (int)Math.Ceiling(50d / 100 * rTestResults.Length) - 1;
            var p75Index = (int)Math.Ceiling(75d / 100 * rTestResults.Length) - 1;

            // Return a new dump of data, including properly formatted (percentage) percentiles and min/max.
            return new TestAggregate() {
                Count = count,
                Mean = mean,
                StandardDeviation = stdDev,
                P25 = rTestResults[p25Index].ObtainedMarks / avMarks * 100,
                P50 = rTestResults[p50Index].ObtainedMarks / avMarks * 100,
                P75 = rTestResults[p75Index].ObtainedMarks / avMarks * 100,
                Min = rTestResults.Select(t => t.ObtainedMarks).Min() / avMarks * 100,
                Max = rTestResults.Select(t => t.ObtainedMarks).Max() / avMarks * 100
            };
        }
    }
}
