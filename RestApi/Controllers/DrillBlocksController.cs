using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RestApi.DB;
using RestApi.Models;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrillBlocksController : Controller
    {
        private readonly dbContext _dbContext;

        public DrillBlocksController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrillBlock>>> GetDrillBlocks()
        {
            return await _dbContext.DrillBlocks.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DrillBlock>> GetDrillBlock(int id)
        {
            var drillBlock = await _dbContext.DrillBlocks.FindAsync(id);

            if (drillBlock == null)
            {
                return NotFound();
            }

            return drillBlock;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDrillBlock(int id, DrillBlock drillBlock)
        {
            if (id != drillBlock.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(drillBlock).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrillBlockExists(id))
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
        public async Task<ActionResult<DrillBlock>> CreateDrillBBlock(DrillBlock drillBlock)
        {
            _dbContext.DrillBlocks.Add(drillBlock);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDrillBlock), new { id = drillBlock.Id }, drillBlock);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrillBlock(int id)
        {
            var drillBlock = await _dbContext.DrillBlocks.FindAsync(id);

            if (drillBlock == null)
            {
                return NotFound();
            }

            _dbContext.DrillBlocks.Remove(drillBlock);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool DrillBlockExists(int id)
        {
            return _dbContext.DrillBlocks.Any(x => x.Id == id);
        }


    }
    
    
    
    
    
    
    
    
}