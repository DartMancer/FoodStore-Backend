public class DailySalesSummary
{
    public decimal TotalSales { get; set; }
    public decimal TotalUnits { get; set; }
}

public class HourlySalesData
{
    public int Hour { get; set; }
    public decimal TodaySales { get; set; }
    public decimal YesterdaySales { get; set; }
    public decimal LastWeekSales { get; set; }
}

public class DailySalesResponse
{
    public required DailySalesSummary DailySummary { get; set; }
    public required List<HourlySalesData> HourlySales { get; set; }
}


public class WeeklySalesSummary
{
    public decimal TotalSales { get; set; }
    public decimal TotalUnits { get; set; }
}

public class DailySalesData
{
    public string? Day { get; set; }
    public decimal ThisWeek { get; set; }
    public decimal LastWeek { get; set; }
}

public class WeeklySalesResponse
{
    public required WeeklySalesSummary WeeklySummary { get; set; }
    public required List<DailySalesData> DailySales { get; set; }
}
