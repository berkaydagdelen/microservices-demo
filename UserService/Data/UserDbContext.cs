
using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Data
{
    /// <summary>
    /// UserService için veritabaný context'i
    /// Code First yaklaþýmý ile veritabaný þemasýný yönetir
    /// </summary>
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        // Users tablosunu temsil eden DbSet
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Model yapýlandýrmasý (isteðe baðlý)
        /// Tablo adlarý, kolon özellikleri vb. burada ayarlanabilir
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User entity yapýlandýrmasý
            modelBuilder.Entity<User>(entity =>
            {
                // Primary Key
                entity.HasKey(e => e.Id);

                // Tablo adý
                entity.ToTable("Users");

                // Kolon özellikleri
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                // Index (Email unique olabilir)
                entity.HasIndex(e => e.Email)
                    .IsUnique();
            });
        }
    }
}

