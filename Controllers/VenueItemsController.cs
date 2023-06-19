using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GigAppTest.Models;

namespace GigAppTest.Controllers
{
    [Route("api/venues")]
    [ApiController]
    public class VenueItemsController : ControllerBase
    {
        private readonly VenueContext _context;

        public VenueItemsController(VenueContext context)
        {
            _context = context;
        }

        // GET: api/VenueItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VenueItem>>> GetVenueItems()
        {
          if (_context.VenueItems == null)
          {
              return NotFound();
          }
            return await _context.VenueItems.ToListAsync();
        }

        // GET: api/VenueItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VenueItem>> GetVenueItem(long id)
        {
          if (_context.VenueItems == null)
          {
              return NotFound();
          }
            var venueItem = await _context.VenueItems.FindAsync(id);

            if (venueItem == null)
            {
                return NotFound();
            }

            return venueItem;
        }

        // PUT: api/VenueItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenueItem(long id, VenueItem venueItem)
        {
            if (id != venueItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(venueItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenueItemExists(id))
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

        // POST: api/VenueItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VenueItem>> PostVenueItem(VenueItem venueItem)
        {
          if (_context.VenueItems == null)
          {
              return Problem("Entity set 'VenueContext.VenueItems'  is null.");
          }
            _context.VenueItems.Add(venueItem);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetVenueItem", new { id = venueItem.Id }, venueItem);
            return CreatedAtAction(nameof(GetVenueItem), new { id = venueItem.Id }, venueItem);
        }

        // DELETE: api/VenueItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenueItem(long id)
        {
            if (_context.VenueItems == null)
            {
                return NotFound();
            }
            var venueItem = await _context.VenueItems.FindAsync(id);
            if (venueItem == null)
            {
                return NotFound();
            }

            _context.VenueItems.Remove(venueItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VenueItemExists(long id)
        {
            return (_context.VenueItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
