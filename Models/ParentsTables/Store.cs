using System.ComponentModel.DataAnnotations.Schema;
using FoodStoreAPI.Models.ChildTables;

namespace FoodStoreAPI.Models.ParentsTables
{
    [Table(name:"stores_information")]
    public class Store
    {
        [Column(name:"id")]
        public long Id { get; set; }
        [Column(name:"store_name")]
        public required string StoreName { get; set; }
        [Column(name:"store_location")]
        public required string Location { get; set; }
        [Column(name:"opening_time_start")]
        public TimeSpan OpeningTime { get; set; }
        [Column(name:"opening_time_end")]
        public TimeSpan ClosingTime { get; set; }
        public List<StoresProduct> StoresProducts { get; set; } = new List<StoresProduct>();
    }

    public class StoreDto
    {
        public long Id { get; set; }
        public string? StoreName { get; set; }
        public string? Location { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
    }    
}
