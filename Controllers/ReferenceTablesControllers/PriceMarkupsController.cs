using FoodStoreAPI.Models.ReferencesTables;

public class PriceMarkupsController : ReferenceController<PriceMarkupModel>
{
    public PriceMarkupsController(ApplicationContext context) : base(context) { }
}