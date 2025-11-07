public class AppDbContext : DbContext
{
    public DbSet<LoadDetail> LoadDetails { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Bakery> Bakeries { get; set; }
    public DbSet<Carrier> Carriers { get; set; }
    public DbSet<SealsUsed> SealsUsed { get; set; }
    public DbSet<TruckInspectionConfirmation> TruckInspectionConfirmations { get; set; }
    public DbSet<Loadout> Loadouts { get; set; }
    public DbSet<ProductCode> ProductCodes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}