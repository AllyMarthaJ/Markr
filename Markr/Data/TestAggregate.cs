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
            var rTestResults = testResults
                .GroupBy(s => s.StudentNumber)
                .Select(g => g.MaxBy(s => s.ObtainedMarks))
                .OrderBy(t => t.ObtainedMarks)
                .ToArray();

            var avMarks = rTestResults.First().AvailableMarks;

            var count = rTestResults.Length;
            var mean = rTestResults.Select(t => t.ObtainedMarks / avMarks).Average();
            var stdDev = Math.Sqrt(rTestResults.Select(t => Math.Pow(t.ObtainedMarks / avMarks - mean, 2)).Sum() / count);
            var p25Index = (int)Math.Floor(25d / 100 * rTestResults.Length);
            var p50Index = (int)Math.Floor(50d / 100 * rTestResults.Length);
            var p75Index = (int)Math.Floor(75d / 100 * rTestResults.Length);

            return new TestAggregate() {
                Count = count,
                Mean = mean * 100,
                StandardDeviation = stdDev * 100,
                P25 = rTestResults[p25Index].ObtainedMarks / avMarks * 100,
                P50 = rTestResults[p50Index].ObtainedMarks / avMarks * 100,
                P75 = rTestResults[p75Index].ObtainedMarks / avMarks * 100,
                Min = rTestResults.Select(t => t.ObtainedMarks).Min() / avMarks * 100,
                Max = rTestResults.Select(t => t.ObtainedMarks).Max() / avMarks * 100
            };
        }
    }
}
