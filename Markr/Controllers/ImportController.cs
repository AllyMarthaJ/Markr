using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Markr.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class ImportController : ControllerBase {
        private ILogger<ImportController> logger;

        public ImportController(ILogger<ImportController> logger) {
            this.logger = logger;   
        }

        [HttpPost]
        [AcceptVerbs]
        public ActionResult PostExamData(gStruct content) {
            return Ok(content.Content + "gay");
        }
    }

    public struct gStruct {
        public string Content { get; set; }
    }
}
