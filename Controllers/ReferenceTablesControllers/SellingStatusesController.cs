using FoodStoreAPI.Models.ReferencesTables;

public class SellingStatusesController : ReferenceController<SellingStatus>
{
    public SellingStatusesController(ApplicationContext context) : base(context) { }
}