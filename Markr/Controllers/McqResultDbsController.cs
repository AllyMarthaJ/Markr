using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Markr.DataHandling.Data;
using Markr.DataHandling.Storage;

namespace Markr.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class McqResultDbsController : ControllerBase {
        private readonly StorageContext _context;

        public McqResultDbsController(StorageContext context) {
            _context = context;
        }

        // GET: api/McqResultDbs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<McqResultDb>>> GetResult() {
            return await _context.Result.ToListAsync();
        }

        // GET: api/McqResultDbs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<McqResultDb>> GetMcqResultDb(int id) {
            var mcqResultDb = await _context.Result.FindAsync(id);

            if (mcqResultDb == null) {
                return NotFound();
            }

            return mcqResultDb;
        }

        // PUT: api/McqResultDbs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMcqResultDb(int id, McqResultDb mcqResultDb) {
            if (id != mcqResultDb.ResultId) {
                return BadRequest();
            }

            _context.Entry(mcqResultDb).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!McqResultDbExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/McqResultDbs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<McqResultDb>> PostMcqResultDb(McqResultDb mcqResultDb) {
            _context.Result.Add(mcqResultDb);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMcqResultDb", new { id = mcqResultDb.ResultId }, mcqResultDb);
        }

        // DELETE: api/McqResultDbs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMcqResultDb(int id) {
            var mcqResultDb = await _context.Result.FindAsync(id);
            if (mcqResultDb == null) {
                return NotFound();
            }

            _context.Result.Remove(mcqResultDb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool McqResultDbExists(int id) {
            return _context.Result.Any(e => e.ResultId == id);
        }
    }
}
