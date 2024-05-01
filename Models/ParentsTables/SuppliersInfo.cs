using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStoreAPI.Models.ParentsTables
{
    [Table(name:"suppliers_information")]
    public class SupplierInfo 
    {
        [Column(name:"id")]
        public long Id { get; set; }
        [Column(name:"supplier_name")]
        public string? SupplierName { get; set; }
    }    
}
