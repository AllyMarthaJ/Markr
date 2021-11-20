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
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ImportController : ControllerBase {
        private readonly StorageContext context;

        public ImportController(StorageContext context) {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<object>> PostImportResultsDbAsync(McqTestResults results) {
            context.Database.EnsureCreated();

            McqResultDb[] dbResults = new McqResultDb[results.McqTestResult.Length];

            try {
                for (int i = 0; i < results.McqTestResult.Length; i++) {
                    dbResults[i] = results.McqTestResult[i].ToDatabaseData();
                }
            } catch {
                return BadRequest(results);
            }

            try {
                context.Result.AddRange(dbResults);
                await context.SaveChangesAsync();
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }

            return Ok(dbResults);
        }
    }
}
