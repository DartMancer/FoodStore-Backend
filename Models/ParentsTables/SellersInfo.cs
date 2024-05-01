using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStoreAPI.Models.ParentsTables
{
    [Table(name:"sellers_information")]
    public class SellerInfo
    {
        [Column(name:"id")]
        public long Id { get; set; }
        [Column(name:"seller_name")]
        public string? SellerName { get; set; }
    }    
}
