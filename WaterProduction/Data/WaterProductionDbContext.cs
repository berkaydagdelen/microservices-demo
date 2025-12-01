using Microsoft.EntityFrameworkCore;
using WaterProduction.Models;

namespace WaterProduction.Data
{
    /// <summary>
    /// WaterProduction servisi için veritabaný context'i
    /// Code First yaklaþýmý ile veritabaný þemasýný yönetir
    /// </summary>
    public class WaterProductionDbContext : DbContext
    {
        public WaterProductionDbContext(DbContextOptions<WaterProductionDbContext> options) : base(options)
        {
        }

        // WaterProductionData tablosunu temsil eden DbSet
        public DbSet<WaterProductionData> WaterProductionData { get; set; }

        /// <summary>
        /// Model yapýlandýrmasý
        /// Tablo adlarý, kolon özellikleri vb. burada ayarlanýr
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // WaterProductionData entity yapýlandýrmasý
            modelBuilder.Entity<WaterProductionData>(entity =>
            {
                // Primary Key
                entity.HasKey(e => e.Id);

                // Tablo adý
                entity.ToTable("WaterProductionData");

                // Kolon özellikleri
                entity.Property(e => e.FacilityName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ProductionAmount)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.ProductionDate)
                    .IsRequired();

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValue("ÝZBB API");

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

                // Index (Tesis adý ve tarih kombinasyonu için)
                entity.HasIndex(e => new { e.FacilityName, e.ProductionDate })
                    .IsUnique();

                // Index (Tarih için - sorgu performansý)
                entity.HasIndex(e => e.ProductionDate);
            });
        }
    }
}

