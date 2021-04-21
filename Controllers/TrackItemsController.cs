using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WzBeatsApi.Models;

namespace wz_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackItemsController : ControllerBase
    {
        private readonly WzBeatsApiContext _context;

        public TrackItemsController(WzBeatsApiContext context)
        {
            _context = context;
        }

        // GET: api/TrackItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrackItem>>> GetTrackItems()
        {
            return await _context.TrackItems.ToListAsync();
        }

        // GET: api/TrackItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrackItem>> GetTrackItem(long id)
        {
            var trackItem = await _context.TrackItems.FindAsync(id);

            if (trackItem == null)
            {
                return NotFound();
            }

            return trackItem;
        }

        // PUT: api/TrackItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrackItem(long id, TrackItem trackItem)
        {
            if (id != trackItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(trackItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TrackItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrackItem>> PostTrackItem(TrackItem trackItem)
        {
            _context.TrackItems.Add(trackItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrackItem", new { id = trackItem.Id }, trackItem);
        }

        // DELETE: api/TrackItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrackItem(long id)
        {
            var trackItem = await _context.TrackItems.FindAsync(id);
            if (trackItem == null)
            {
                return NotFound();
            }

            _context.TrackItems.Remove(trackItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrackItemExists(long id)
        {
            return _context.TrackItems.Any(e => e.Id == id);
        }
    }
}
