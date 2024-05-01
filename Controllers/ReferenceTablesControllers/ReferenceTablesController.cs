using FoodStoreAPI.Models.ReferencesTables;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("[controller]")]
[ApiController]
public class ReferenceController<T> : ControllerBase where T : class, IEntityWithId
{
    private readonly ApplicationContext _context;

    public ReferenceController(ApplicationContext context)
    {
        _context = context;
    }

    // GET: api/Reference
    [HttpGet]
    public async Task<ActionResult<IEnumerable<T>>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    // GET: api/Reference/5
    [HttpGet("{id}")]
    public async Task<ActionResult<T>> Get(long id)
    {
        var item = await _context.Set<T>().FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return item;
    }

    // POST: api/Reference
    [HttpPost]
    public async Task<ActionResult<T>> Post(T item)
    {
        _context.Set<T>().Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction("Get", new { id = ((dynamic)item).Id }, item);
    }

    // PATCH: api/Reference/5
    [HttpPatch("{Id}")]
    public async  Task<IActionResult> PatchProduct(long Id, [FromBody] JsonPatchDocument<UnitModel> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest();
        }

        var entity = await _context.Set<UnitModel>().FindAsync(Id);

        if (entity == null)
        {
            return NotFound();
        }

        Console.WriteLine($"Unit before: {entity.Unit}");
        patchDoc.ApplyTo(entity, ModelState);
        Console.WriteLine($"Unit after: {entity.Unit}");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            Console.WriteLine($"State before save: {_context.Entry(entity).State}");
            _context.Update(entity);
            Console.WriteLine($"State before save1: {_context.Entry(entity).State}");
            await _context.SaveChangesAsync();
        }
        catch (DataMisalignedException)
        {
            return StatusCode(500, "Internal server error");
        }

        return NoContent();
    }


    // DELETE: api/Reference/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var item = await _context.Set<T>().FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        _context.Set<T>().Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool ReferenceExists(long id)
    {
        return _context.Set<T>().Any(e => e.Id == id);
    }
}
