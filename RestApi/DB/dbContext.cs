using Microsoft.EntityFrameworkCore;
using RestApi.Models;

namespace RestApi.DB;

public class dbContext: DbContext
{

    public dbContext(DbContextOptions<dbContext> options) : base(options)
    {
    }
    
    public DbSet<DrillBlock> DrillBlocks { get; set; }
    
    public DbSet<Hole> Holes { get; set; }
    
    public DbSet<DrillBlockPoint> DrillBlockPoints { get; set; }
    
    public DbSet<HolePoint> HolePoints { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DrillBlock>()
            .HasMany(d => d.DrillBlockPoints)
            .WithOne(p => p.DrillBlock)
            .HasForeignKey(p => p.DrillBlockId);

        modelBuilder.Entity<Hole>()
            .HasMany(h => h.HolePoints)
            .WithOne(p => p.Hole)
            .HasForeignKey(p => p.HoleId);
    }

}