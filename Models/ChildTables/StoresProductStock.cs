using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStoreAPI.Models.ChildTables
{
    [Table(name:"stores_products_stocks")]
    public class StoresProductStock
    {
        [Column(name:"id")]
        public long Id { get; set; }

        [Column(name:"stores_product_id")]
        public long StoresProductId { get; set; }

        [Column(name:"supplier_id")]
        public long SupplierId { get; set; }

        [Column(name:"quantity")]
        public decimal Quantity { get; set; }

        [Column(name:"last_dt_upd")]
        public DateTime LastDtUpd { get; set; }
    }
}
