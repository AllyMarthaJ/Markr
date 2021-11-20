using Markr.DataHandling.Data;
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
    public class ImportController : ControllerBase {
        private readonly ILogger<ImportController> logger;

        public ImportController(ILogger<ImportController> logger) {
            this.logger = logger;
        }

        [HttpPost]
        public ActionResult PostImportData(McqTestResults result) {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(McqTestResults));
            using (StringWriter textWriter = new StringWriter()) {
                xmlSerializer.Serialize(textWriter, result);
                return Ok(textWriter.ToString());
            }
        }
    }
}
