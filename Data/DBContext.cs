namespace ExportStatistics.Data;

using Microsoft.EntityFrameworkCore;
using ExportStatistics.Data;
public class EfDbContext : DbContext
{
    public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
    {

    }
    public virtual DbSet<Entities.ExportProduct> ExportProducts { get; set; }
    public virtual DbSet<Entities.Category> Categories { get; set; }
    public virtual DbSet<Entities.Country> Countries { get; set; }
    public virtual DbSet<Entities.ExportStatistic> ExportStatistics { get; set; }
    public virtual DbSet<Entities.MarketInsight> MarketInsights { get; set; }
    public virtual DbSet<Entities.ErrorLog> ErrorLogs { get; set; }
    protected override void OnModelCreating(
    ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entities.Category>()
            .HasIndex(x => x.Name)
            .IsUnique();
        modelBuilder.Entity<Entities.ExportProduct>()
            .HasIndex(x => x.HsCode)
            .IsUnique();
    }

}