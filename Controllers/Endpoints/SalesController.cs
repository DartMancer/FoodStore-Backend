using System.Globalization;
using FoodStoreAPI.Models.ParentsTables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SalesController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("daily-sales-summary")]
        public async Task<IActionResult> GetDailySales()
        {
            var today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0, DateTimeKind.Utc);
            var yesterday = today.AddDays(-1);
            var lastWeek = today.AddDays(-7);

            var dailySummary = await GetDailySalesTotal(today);
            var todaySales = await GetHourlySales(today);
            var yesterdaySales = await GetHourlySales(yesterday);
            var lastWeekSales = await GetHourlySales(lastWeek);

            var hourlySales = Enumerable.Range(0, 24).Select(hour => new HourlySalesData
            {
                Hour = hour,
                TodaySales = todaySales.FirstOrDefault(h => h.Hour == hour)?.TodaySales ?? 0,
                YesterdaySales = yesterdaySales.FirstOrDefault(h => h.Hour == hour)?.TodaySales ?? 0,
                LastWeekSales = lastWeekSales.FirstOrDefault(h => h.Hour == hour)?.TodaySales ?? 0
            }).ToList();

            var response = new DailySalesResponse
            {
                DailySummary = dailySummary,
                HourlySales = hourlySales
            };

            return Ok(response);
        }

        private async Task<DailySalesSummary> GetDailySalesTotal(DateTime date)
        {
            var endDate = date.AddDays(1);
            return await _context.StoresOrders
                .Where(o => o.StoreSelling!.SaleDate >= date && o.StoreSelling!.SaleDate < endDate)
                .GroupBy(o => 1)
                .Select(g => new DailySalesSummary
                {
                    TotalSales = g.Sum(x => x.SoldByPrice),
                    TotalUnits = g.Sum(x => x.QuantitySold)
                })
                .FirstOrDefaultAsync() ?? new DailySalesSummary();
        }

        private async Task<List<HourlySalesData>> GetHourlySales(DateTime date)
        {
            var endDate = date.AddDays(1);
            var sales = await _context.StoresOrders
                .Where(o => o.StoreSelling!.SaleDate >= date && o.StoreSelling!.SaleDate < endDate)
                .GroupBy(o => o.StoreSelling!.SaleDate.Hour)
                .Select(g => new HourlySalesData
                {
                    Hour = g.Key,
                    TodaySales = g.Sum(x => x.SoldByPrice * x.QuantitySold)
                })
                .ToListAsync();

            return sales;
        }



        [HttpGet("weekly-sales-summary")]
        public async Task<IActionResult> GetWeeklySales()
        {
            var today = DateTime.UtcNow.Date;
            var startOfCurrentWeek = today.AddDays(-6);
            var startOfLastWeek = today.AddDays(-13);
            var endOfLastWeek = today.AddDays(-7);

            var weeklySummary = await GetWeeklySalesTotal(startOfCurrentWeek, today);
            var dailySalesThisWeek = await GetDailySales(startOfCurrentWeek, today);
            var dailySalesLastWeek = await GetDailySales(startOfLastWeek, endOfLastWeek);

            Console.WriteLine(today);
            Console.WriteLine(startOfCurrentWeek);
            Console.WriteLine(startOfLastWeek);
            Console.WriteLine(endOfLastWeek);

            var orderedDays = GetOrderedWeekDays();

            var response = new WeeklySalesResponse
            {
                WeeklySummary = new WeeklySalesSummary
                {
                    TotalSales = weeklySummary.TotalSales,
                    TotalUnits = weeklySummary.TotalUnits
                },
                DailySales = orderedDays.Select(day => new DailySalesData
                {
                    Day = day,
                    ThisWeek = dailySalesThisWeek.FirstOrDefault(d => d.Day == day)?.ThisWeek ?? 0,
                    LastWeek = dailySalesLastWeek.FirstOrDefault(d => d.Day == day)?.LastWeek ?? 0
                }).ToList()
            };

            return Ok(response);
        }


        private async Task<WeeklySalesSummary> GetWeeklySalesTotal(DateTime startDate, DateTime endDate)
        {
            var summary = await _context.StoresOrders
                .Where(o => o.StoreSelling!.SaleDate >= startDate && o.StoreSelling!.SaleDate < endDate)
                .GroupBy(o => 1)
                .Select(g => new WeeklySalesSummary
                {
                    TotalSales = g.Sum(x => x.SoldByPrice),
                    TotalUnits = g.Sum(x => x.QuantitySold)
                })
                .FirstOrDefaultAsync() ?? new WeeklySalesSummary();

            return summary;
        }

        private async Task<List<DailySalesData>> GetDailySales(DateTime startDate, DateTime endDate)
        {
            var cultureInfo = new CultureInfo("ru-RU");

            var sales = await _context.StoresOrders
                .Include(o => o.StoreSelling)
                .Where(o => o.StoreSelling!.SaleDate >= startDate && o.StoreSelling.SaleDate < endDate)
                .ToListAsync();

            var dailySales = sales.GroupBy(o => o.StoreSelling!.SaleDate.DayOfWeek)
                .Select(g => new DailySalesData
                {
                    Day = cultureInfo.DateTimeFormat.GetAbbreviatedDayName(g.Key),
                    ThisWeek = g.Sum(x => x.SoldByPrice),
                    LastWeek = g.Sum(x => x.SoldByPrice)
                })
                .ToList();

            return dailySales;
        }


        private List<string> GetOrderedWeekDays()
        {
            var culture = new CultureInfo("ru-RU");
            var today = DateTime.UtcNow.DayOfWeek;
            var days = Enumerable.Range(0, 7)
                .Select(i => (DayOfWeek)(((int)today + i + 1) % 7))
                .Select(dayOfWeek => culture.DateTimeFormat.GetAbbreviatedDayName(dayOfWeek))
                .ToList();

            return days;
        }






    //     [HttpGet("weekly-sales")]
    //     public IActionResult GetWeeklySales()
    //     {
    //         var orderedDays = GetOrderedWeekDays();
    //         var startOfWeek = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek + 1);
    //         var endOfWeek = startOfWeek.AddDays(7);
    //         var startLastWeek = startOfWeek.AddDays(-7);
    //         var endLastWeek = startOfWeek;

    //         var thisWeekSales = GetDailySales(startOfWeek, endOfWeek);
    //         var lastWeekSales = GetDailySales(startLastWeek, endLastWeek);

    //         var result = orderedDays.Select(dayName => new
    //         {
    //             Day = dayName,
    //             ThisWeek = thisWeekSales.FirstOrDefault(d => d.Day == dayName)?.TotalSales ?? 0,
    //             LastWeek = lastWeekSales.FirstOrDefault(d => d.Day == dayName)?.TotalSales ?? 0
    //         }).ToList();

    //         return Ok(result);
    //     }

    //     private List<DailySales> GetDailySales(DateTime startDate, DateTime endDate)
    //     {
    //         var culture = new CultureInfo("ru-RU");
    //         return _context.StoresOrders
    //             .Where(o => o.StoreSelling!.SaleDate >= startDate && o.StoreSelling!.SaleDate < endDate)
    //             .GroupBy(o => o.StoreSelling!.SaleDate.DayOfWeek)
    //             .Select(g => new DailySales
    //             {
    //                 Day = culture.DateTimeFormat.GetAbbreviatedDayName(g.Key),
    //                 TotalSales = g.Sum(o => o.SoldByPrice * o.QuantitySold)
    //             })
    //             .ToList();
    //     }



    //     private List<string> GetOrderedWeekDays()
    //     {
    //         var culture = new CultureInfo("ru-RU");
    //         var days = Enumerable.Range(1, 7)
    //             .Select(offset => DateTime.UtcNow.AddDays(offset).DayOfWeek)
    //             .Select(dayOfWeek => culture.DateTimeFormat.GetAbbreviatedDayName(dayOfWeek))
    //             .ToList();
        
    //         var todayDayName = culture.DateTimeFormat.GetAbbreviatedDayName(DateTime.UtcNow.DayOfWeek);
    //         int todayIndex = days.IndexOf(todayDayName);
    //         var orderedDays = days.Skip(todayIndex + 1).Concat(days.Take(todayIndex + 1)).ToList();
        
    //         return orderedDays;
    //     }

    }
}
