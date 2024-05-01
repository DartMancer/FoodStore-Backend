using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodStoreAPI.Models.ChildTables;
using Microsoft.AspNetCore.JsonPatch;
using FoodStoreAPI.Models.ParentsTables;

[Route("[controller]")]
[ApiController]
public class StoresProductController : ControllerBase
{
    private readonly ApplicationContext _context;

    public StoresProductController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<StoresProductDto>>> GetAllStoresProducts()
    {
        var storesProducts = await _context.Stores
        .Select(s => new StoresProductDto
        {
            Id = s.Id,
            StoreName = s.StoreName,
            Location = s.Location,
            OpeningTime = s.OpeningTime,
            ClosingTime = s.ClosingTime,
            Products = s.StoresProducts.Select(sp => new ProductDto
            {
                Id = sp.Product!.Id,
                ProductName = sp.Product.ProductName,
                Manufacturer = new ManufacturerDto
                {
                    Id = sp.Product.Manufacturer!.Id,
                    ManufacturerName = sp.Product.Manufacturer.ManufacturerName,
                    Address = sp.Product.Manufacturer.Address,
                    IsActive = sp.Product.Manufacturer.IsActive
                },
                Unit = sp.Product.Unit!.Unit
            }).ToList()
        })
        .ToListAsync();

        return storesProducts;
    }

    // GET: api/StoresProduct/5
    [HttpGet("{id}")]
    public async Task<ActionResult<StoresProductDto>> GetStoresProduct(long id)
    {
        var storesProducts = await _context.Stores
        .Where(s => s.Id == id)
        .Select(s => new StoresProductDto
        {
            Id = s.Id,
            StoreName = s.StoreName,
            Location = s.Location,
            OpeningTime = s.OpeningTime,
            ClosingTime = s.ClosingTime,
            Products = s.StoresProducts.Select(sp => new ProductDto
            {
                Id = sp.Product!.Id,
                ProductName = sp.Product.ProductName,
                Manufacturer = new ManufacturerDto
                {
                    Id = sp.Product.Manufacturer!.Id,
                    ManufacturerName = sp.Product.Manufacturer.ManufacturerName,
                    Address = sp.Product.Manufacturer.Address,
                    IsActive = sp.Product.Manufacturer.IsActive
                },
                Unit = sp.Product.Unit!.Unit
            }).ToList()
        })
        .FirstOrDefaultAsync();

        if (storesProducts == null)
        {
            return NotFound();
        }

        return storesProducts;
    }

    // POST: api/StoresProduct
    [HttpPost]
    public async Task<ActionResult<StoresProduct>> PostStoresProduct(StoresProduct storesProduct)
    {
        _context.StoresProducts.Add(storesProduct);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetStoresProduct", new { id = storesProduct.Id }, storesProduct);
    }

    // PATCH: api/StoresProduct/5
    [HttpPatch("{Id}")]
    public async Task<IActionResult> PatchProduct(long Id, [FromBody] JsonPatchDocument<StoresProduct> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest();
        }

        var storesProduct = await _context.StoresProducts.FindAsync(Id);

        if (storesProduct == null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(storesProduct, ModelState);

        if (!TryValidateModel(storesProduct))
        {
            return BadRequest(ModelState);
        }

        _context.Entry(storesProduct).State = EntityState.Modified;
        var result = await _context.SaveChangesAsync();
        if (result > 0) {
            return NoContent();
        } else {
            return Ok("No changes detected.");
        }
    }

    // DELETE: api/StoresProduct/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStoresProduct(long id)
    {
        var storesProduct = await _context.StoresProducts.FindAsync(id);
        if (storesProduct == null)
        {
            return NotFound();
        }

        _context.StoresProducts.Remove(storesProduct);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool StoresProductExists(long id)
    {
        return _context.StoresProducts.Any(e => e.Id == id);
    }
}
