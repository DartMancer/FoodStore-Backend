using System.ComponentModel.DataAnnotations.Schema;
using FoodStoreAPI.Models.ParentsTables;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FoodStoreAPI.Models.ChildTables
{
    [Table(name:"stores_products")]
    public class StoresProduct
    {
        [Column(name:"id")]
        public long Id { get; set; }
        
        [Column(name:"product_id")]
        public long ProductId { get; set; }

        [Column(name:"store_id")]
        public long StoreId { get; set; }

        [Column(name:"dt_create")]
        public DateTime DtCreate { get; set; }
        public Product? Product { get; set; }
        public Store? Store { get; set; }
    }

        public class StoresProductDto
    {
           public long Id { get; set; }
            public string? StoreName { get; set; }
            public string? Location { get; set; }
            public TimeSpan OpeningTime { get; set; }
            public TimeSpan ClosingTime { get; set; }
            public List<ProductDto>? Products { get; set; }
    }
}


