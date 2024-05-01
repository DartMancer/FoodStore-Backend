using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStoreAPI.Models.ChildTables
{
    [Table(name:"stores_selling")]
    public class StoreSelling
    {
        [Column(name:"id")]
        public long Id { get; set; }

        [Column(name:"seller_id")]
        public long SellerId { get; set; }

        [Column(name:"status_id")]
        public long StatusId { get; set; }

        [Column(name:"end_price")]
        public decimal EndPrice { get; set; }

        [Column(name:"dt_upd")]
        public DateTime DtUpd { get; set; } 
        
        [Column(name:"sale_date")]
        public DateTime SaleDate { get; set; }
    }
}
