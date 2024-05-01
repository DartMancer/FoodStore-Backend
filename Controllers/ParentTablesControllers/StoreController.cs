using FoodStoreAPI.Models.ParentsTables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoresController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public StoresController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
         public async Task<ActionResult<IEnumerable<Store>>> GetAllStores()
        {
            var stores = await _context.Stores.ToListAsync();
            return Ok(stores);
        }
    }
}
