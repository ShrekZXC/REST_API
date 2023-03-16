using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.DB;
using RestApi.Models;

namespace RestApi.Controllers;

public class HolePointController : Controller
{
    private readonly dbContext _dbContext;

    public HolePointController(dbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HolePoint>>> GetHolePoints()
    {
        return await _dbContext.HolePoints.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HolePoint>> GetHolePoints(int id)
    {
        var holePoint = await _dbContext.HolePoints.FindAsync(id);

        if (holePoint == null)
        {
            return NotFound();
        }

        return holePoint;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHolePoint(int id, HolePoint holePoint)
    {
        if (id != holePoint.Id)
        {
            return BadRequest();
        }

        _dbContext.Entry(holePoint).State = EntityState.Modified;

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!HolePointExists(id))
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
    public async Task<ActionResult<HolePoint>> CreateHolePoint(HolePoint holePoint)
    {
        _dbContext.HolePoints.Add(holePoint);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHolePoints), new { id = holePoint.Id }, holePoint);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHolePoint(int id)
    {
        var holePoint = await _dbContext.HolePoints.FindAsync(id);

        if (holePoint == null)
        {
            return NotFound();
        }

        _dbContext.HolePoints.Remove(holePoint);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    private bool HolePointExists(int id)
    {
        return _dbContext.HolePoints.Any(x => x.Id == id);
    }
}