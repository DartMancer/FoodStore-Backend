using FoodStoreAPI.Models.ParentsTables;
using FoodStoreAPI.Models.ChildTables;
using FoodStoreAPI.Models.ReferencesTables;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {

    }
    //ParentTables
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<SellerInfo> SellerInfo { get; set; }
    public DbSet<SupplierInfo> SupplierInfo { get; set; }


    //ChildTables
    public DbSet<Product> Products { get; set; }
    public DbSet<StoresOrder> StoresOrders { get; set; }
    public DbSet<StoresProduct> StoresProducts { get; set; }
    public DbSet<StoresProductPrice> StoresProductPrices { get; set; }
    public DbSet<StoresProductStock> StoresProductStocks { get; set; }
    public DbSet<StoreSelling> StoresSellings { get; set; }

    
    //ReferenceTables
    public DbSet<UnitModel> Units { get; set; }
    public DbSet<PriceMarkupModel> PriceMarkups { get; set; }
    public DbSet<SellingStatus> SellingStatuses { get; set; }
    
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Manufacturer)  
            .WithMany()                  
            .HasForeignKey(p => p.ManufacturerId);
    
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Unit)          
            .WithMany()                  
            .HasForeignKey(p => p.UnitId);

        modelBuilder.Entity<Store>()
            .HasMany(s => s.StoresProducts)
            .WithOne() 
            .HasForeignKey(sp => sp.StoreId);

        modelBuilder.Entity<StoresProduct>()
            .HasOne(sp => sp.Store)
            .WithMany(s => s.StoresProducts)
            .HasForeignKey(sp => sp.StoreId);

        modelBuilder.Entity<StoresOrder>()
            .HasOne(s => s.StoreSelling) // Используйте правильное название свойства
            .WithMany()
            .HasForeignKey(s => s.SellingId);

        modelBuilder.Entity<StoreSelling>(entity =>
        {
            entity.Property(e => e.SaleDate).HasColumnType("timestamp with time zone");
        });

    }
}
