using Markr.DataHandling.Data;
using Markr.DataHandling.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Markr.Controllers {
    [Route("")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public class ResultController : ControllerBase {
        private readonly StorageContext context;

        public ResultController(StorageContext context) {
            this.context = context;
        }

        [HttpPost("import")]
        public async Task<ActionResult<object>> PostImportResultsDbAsync(McqTestResults results) {
            // Create db if not already.
            context.Database.EnsureCreated();

            // Prepare to transform data.
            McqResultDb[] dbResults = new McqResultDb[results.McqTestResult.Length];

            // Transform/translate data to database form.
            try {
                for (int i = 0; i < results.McqTestResult.Length; i++) {
                    dbResults[i] = results.McqTestResult[i].ToDatabaseData();
                }
            } catch {
                return BadRequest(results);
            }

            // Attempt to save data to database.
            try {
                context.Result.AddRange(dbResults);
                await context.SaveChangesAsync();
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }

            // If everything went well, return Ok with no body.
            return Ok();
        }

        [HttpGet("{testId}/aggregate")]
        public ActionResult<TestAggregate> GetTestAggregate(int testId) {
            // Fetch the test results with that ID.
            McqResultDb[] testResults = context.Result.Where(t => t.TestId == testId).ToArray();

            // No results; 404.
            if (testResults.Length == 0) {
                return NotFound("No tests with that ID were found.");
            }
 
            // Format the test as aggregate; will filter data further.
            return TestAggregate.FormatTest(testResults);
        }
    }
}
