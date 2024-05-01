using FoodStoreAPI.Models.ParentsTables;
using FoodStoreAPI.Models.ChildTables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ProductsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAllProducts()
        {
           var products = _context.Products
            .Include(p => p.Manufacturer)
            .Include(p => p.Unit)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Manufacturer = new ManufacturerDto
                {
                    ManufacturerName = p.Manufacturer!.ManufacturerName,
                    Address = p.Manufacturer.Address,
                    IsActive = p.Manufacturer.IsActive
                },
                Unit = p.Unit!.Unit
            })
            .ToList();
            return Ok(products);
        }
    }
}
