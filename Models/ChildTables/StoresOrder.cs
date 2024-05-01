using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStoreAPI.Models.ChildTables
{
    [Table(name:"stores_orders")]
    public class StoresOrder
    {
        [Column(name:"id")]
        public long Id { get; set; }

        [Column(name:"selling_id")]
        public long SellingId { get; set; }

        [Column(name:"stores_product_id")]
        public long StoresProductId { get; set; }

        [Column(name:"quantity_sold")]
        public decimal QuantitySold { get; set; }

        [Column(name:"sold_by_price")]
        public decimal SoldByPrice { get; set; }
        public StoreSelling? StoreSelling { get; set; }
    }
}
