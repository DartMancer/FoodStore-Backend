using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStoreAPI.Models.ReferencesTables
{
    [Table(name:"units_guide")]
    public class UnitModel : IEntityWithId
    {
        [Column(name:"id")]
        public long Id { get; set; }
        [Column(name:"unit")]
        public string? Unit { get; set; }
    }
}
