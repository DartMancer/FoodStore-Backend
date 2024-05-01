using System.ComponentModel.DataAnnotations.Schema;
using FoodStoreAPI.Models.ParentsTables;
using FoodStoreAPI.Models.ReferencesTables;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FoodStoreAPI.Models.ChildTables
{
    [Table(name:"products_information")]
    public class Product
    {
        [Column(name:"id")]
        public long Id { get; set; }
        [Column(name:"product_name")]
        public string? ProductName { get; set; }
        [Column(name:"manufacturer_id")]
        public long ManufacturerId { get; set; }
        [Column(name:"unit_id")]
        public long UnitId { get; set; }
        public Manufacturer? Manufacturer { get; set; }
        public UnitModel? Unit { get; set; }
    }

    public class ProductDto
    {
        public long Id { get; set; }
        public string? ProductName { get; set; }
        public ManufacturerDto? Manufacturer { get; set; }
        public string? Unit { get; set; }
    }

}
