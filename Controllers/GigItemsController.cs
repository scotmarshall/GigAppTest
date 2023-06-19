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
    [Route("api/gigs")]
    [ApiController]
    public class GigItemsController : ControllerBase
    {
        private readonly GigContext _context;

        public GigItemsController(GigContext context)
        {
            _context = context;
        }

        // GET: api/GigItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GigItem>>> GetGigItems()
        {
          if (_context.GigItems == null)
          {
              return NotFound();
          }
            return await _context.GigItems.ToListAsync();
        }

        // GET: api/GigItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GigItem>> GetGigItem(long id)
        {
          if (_context.GigItems == null)
          {
              return NotFound();
          }
            var gigItem = await _context.GigItems.FindAsync(id);

            if (gigItem == null)
            {
                return NotFound();
            }

            return gigItem;
        }

        // PUT: api/GigItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGigItem(long id, GigItem gigItem)
        {
            if (id != gigItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(gigItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GigItemExists(id))
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

        // POST: api/GigItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GigItem>> PostGigItem(GigItem gigItem)
        {
          if (_context.GigItems == null)
          {
              return Problem("Entity set 'GigContext.GigItems'  is null.");
          }
            _context.GigItems.Add(gigItem);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetGigItem", new { id = gigItem.Id }, gigItem);
            return CreatedAtAction(nameof(GetGigItem), new { id = gigItem.Id }, gigItem);
        }

        // DELETE: api/GigItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGigItem(long id)
        {
            if (_context.GigItems == null)
            {
                return NotFound();
            }
            var gigItem = await _context.GigItems.FindAsync(id);
            if (gigItem == null)
            {
                return NotFound();
            }

            _context.GigItems.Remove(gigItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GigItemExists(long id)
        {
            return (_context.GigItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
