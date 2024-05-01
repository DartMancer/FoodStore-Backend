using FoodStoreAPI.Models.ReferencesTables;

public class UnitsController : ReferenceController<UnitModel>
{
    public UnitsController(ApplicationContext context) : base(context) { }
}