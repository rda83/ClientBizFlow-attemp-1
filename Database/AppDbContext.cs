using ClientBizFlow_attemp_1.Database.Entities.BizFlow;
using ClientBizFlow_attemp_1.Database.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ClientBizFlow_attemp_1.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<PipelineItem> PipelineItems { get; set; }
        public DbSet<Pipeline> Pipelines { get; set; }
        public DbSet<BizFlowJournalRecord> BizFlowJournalRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(p => p.Name).IsUnique();
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
            });

            modelBuilder
                .Entity<BizFlowJournalRecord>()
                .Property(e => e.TypeAction)
                .HasConversion<string>()
                .HasColumnType("text");
        }
    }
}
