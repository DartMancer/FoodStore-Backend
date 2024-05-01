using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStoreAPI.Models.ReferencesTables
{
    [Table(name:"selling_statuses")]
    public class SellingStatus : IEntityWithId
    {
        [Column(name:"id")]
        public long Id { get; set; }
        [Column(name:"selling_status")]
        public string? Status { get; set; }
    }
}
