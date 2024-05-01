using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStoreAPI.Models.ChildTables
{
    [Table(name:"stores_products_price")]
    public class StoresProductPrice
    {
        [Column(name:"id")]
        public long Id { get; set; }
        
        [Column(name:"stores_product_id")]
        public long StoresProductId { get; set; }

        [Column(name:"product_price")]
        public decimal ProductPrice { get; set; }

        [Column(name:"price_markup_id")]
        public long PriceMarkupId { get; set; }

        [Column(name:"dt_start")]
        public DateTime DtStart { get; set; }

        [Column(name:"dt_end")]
        public DateTime DtEnd { get; set; }
    }
}
