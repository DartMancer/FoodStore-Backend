using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodStoreAPI.Models.ChildTables;
using Microsoft.AspNetCore.JsonPatch;

[ApiController]
[Route("[controller]")]
public class StoresOrderController : ControllerBase
{
    private readonly ApplicationContext _context;

    public StoresOrderController(ApplicationContext context)
    {
        _context = context;
    }

    // GET: storesorder
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StoresOrder>>> GetStoresOrders()
    {
        return await _context.StoresOrders.ToListAsync();
    }

    // GET: storesorder/id
    [HttpGet("{id}")]
    public async Task<ActionResult<StoresOrder>> GetStoresOrder(long id)
    {
        var storesOrder = await _context.StoresOrders.FindAsync(id);

        if (storesOrder == null)
        {
            return NotFound();
        }

        return storesOrder;
    }

    // POST: storesorder
    [HttpPost]
    public async Task<ActionResult<StoresOrder>> PostStoresOrder(StoresOrder storesOrder)
    {
        _context.StoresOrders.Add(storesOrder);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStoresOrder), new { id = storesOrder.Id }, storesOrder);
    }

    // PATCH: storesorder/id
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchStoresOrder(long id, [FromBody] JsonPatchDocument<StoresOrder> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest();
        }

        var storesOrder = await _context.StoresOrders.FindAsync(id);
        if (storesOrder == null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(storesOrder, error =>
        {
            ModelState.AddModelError(error.Operation.path, error.ErrorMessage);
        });

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StoresOrderExists(id))
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

    // DELETE: storesorder/id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStoresOrder(long id)
    {
        var storesOrder = await _context.StoresOrders.FindAsync(id);
        if (storesOrder == null)
        {
            return NotFound();
        }

        _context.StoresOrders.Remove(storesOrder);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool StoresOrderExists(long id)
    {
        return _context.StoresOrders.Any(e => e.Id == id);
    }
}
