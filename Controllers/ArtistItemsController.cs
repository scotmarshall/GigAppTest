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
    [Route("api/artists")]
    [ApiController]
    public class ArtistItemsController : ControllerBase
    {
        private readonly ArtistContext _context;

        public ArtistItemsController(ArtistContext context)
        {
            _context = context;
        }

        // GET: api/ArtistItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistItem>>> GetArtistItems()
        {
          if (_context.ArtistItems == null)
          {
              return NotFound();
          }
            return await _context.ArtistItems.ToListAsync();
        }

        // GET: api/ArtistItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistItem>> GetArtistItem(long id)
        {
          if (_context.ArtistItems == null)
          {
              return NotFound();
          }
            var artistItem = await _context.ArtistItems.FindAsync(id);

            if (artistItem == null)
            {
                return NotFound();
            }

            return artistItem;
        }

        // PUT: api/ArtistItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtistItem(long id, ArtistItem artistItem)
        {
            if (id != artistItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(artistItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistItemExists(id))
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

        // POST: api/ArtistItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArtistItem>> PostArtistItem(ArtistItem artistItem)
        {
          if (_context.ArtistItems == null)
          {
              return Problem("Entity set 'ArtistContext.ArtistItems'  is null.");
          }
            _context.ArtistItems.Add(artistItem);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetArtistItem", new { id = artistItem.Id }, artistItem);
            return CreatedAtAction(nameof(GetArtistItem), new { id = artistItem.Id }, artistItem);
        }

        // DELETE: api/ArtistItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtistItem(long id)
        {
            if (_context.ArtistItems == null)
            {
                return NotFound();
            }
            var artistItem = await _context.ArtistItems.FindAsync(id);
            if (artistItem == null)
            {
                return NotFound();
            }

            _context.ArtistItems.Remove(artistItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtistItemExists(long id)
        {
            return (_context.ArtistItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
