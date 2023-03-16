using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.DB;
using RestApi.Models;

namespace RestApi.Controllers;

public class HoleController: Controller
{
    private readonly dbContext _dbContext;

        public HoleController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hole>>> GetHoles()
        {
            return await _dbContext.Holes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hole>> GetHoles(int id)
        {
            var hole = await _dbContext.Holes.FindAsync(id);

            if (hole == null)
            {
                return NotFound();
            }

            return hole;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHole(int id, Hole hole)
        {
            if (id != hole.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(hole).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoleExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Hole>> CreateHole(Hole hole)
        {
            _dbContext.Holes.Add(hole);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHoles), new { id = hole.Id }, hole);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHole(int id)
        {
            var hole = await _dbContext.Holes.FindAsync(id);

            if (hole == null)
            {
                return NotFound();
            }

            _dbContext.Holes.Remove(hole);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool HoleExists(int id)
        {
            return _dbContext.Holes.Any(x => x.Id == id);
        }
}