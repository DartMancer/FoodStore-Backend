using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStoreAPI.Models.ReferencesTables
{
    [Table(name:"price_markups")]
    public class PriceMarkupModel : IEntityWithId
    {
        [Column(name:"id")]
        public long Id { get; set; }
        [Column(name:"price_markup_percent")]
        public int? PriceMarkup { get; set; }
    }
}
