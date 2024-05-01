using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStoreAPI.Models.ParentsTables
{
    [Table(name:"manufacturers_information")]
    public class Manufacturer
    {   
        [Column(name:"id")]
        public long Id { get; set; }
        [Column(name:"manufacturer_name")]
        public string? ManufacturerName { get; set; }
        [Column(name:"address")]
        public string? Address { get; set; }
        [Column(name:"is_active")]
        public bool IsActive { get; set; }
    } 

    public class ManufacturerDto
    {
        public long Id { get; set; }
        public string? ManufacturerName { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
    }
}
