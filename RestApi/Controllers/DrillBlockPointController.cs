using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RestApi.DB;
using RestApi.Models;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrillBlockPointController : Controller
    {
        private readonly dbContext _dbContext;

        public DrillBlockPointController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrillBlockPoint>>> GetDrillBlocksPoints()
        {
            return await _dbContext.DrillBlockPoints.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DrillBlockPoint>> GetDrillBlockPoint(int id)
        {
            var drillBlockPoint = await _dbContext.DrillBlockPoints.FindAsync(id);

            if (drillBlockPoint == null)
            {
                return NotFound();
            }

            return drillBlockPoint;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDrillBlockPoint(int id, DrillBlockPoint drillBlockPoint)
        {
            if (id != drillBlockPoint.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(drillBlockPoint).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrillBlockPointExists(id))
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
        public async Task<ActionResult<DrillBlockPoint>> CreateDrillBBlockPoint(DrillBlockPoint drillBlockPoint)
        {
            _dbContext.DrillBlockPoints.Add(drillBlockPoint);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDrillBlockPoint), new { id = drillBlockPoint.Id }, drillBlockPoint);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrillBlockPoint(int id)
        {
            var drillBlockPoint = await _dbContext.DrillBlockPoints.FindAsync(id);

            if (drillBlockPoint == null)
            {
                return NotFound();
            }

            _dbContext.DrillBlockPoints.Remove(drillBlockPoint);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool DrillBlockPointExists(int id)
        {
            return _dbContext.DrillBlocks.Any(x => x.Id == id);
        }


    }
    
    
    
    
    
    
    
    
}