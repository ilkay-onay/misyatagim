using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;

namespace ProductService.Infrastructure.Data;

public class ProductDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Price property'si için precision ve scale belirtin
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2); // 18 toplam basamak, 2 ondalık basamak

         modelBuilder.Entity<Product>()
            .HasIndex(p=>p.Slug)
            .IsUnique(); // Slug'in unique olmasını sağlar
            
        modelBuilder.Entity<Product>()
            .Property(p => p.ImageBase64s)
            .HasConversion(
                v => string.Join(";", v),
                v => v.Split(";", StringSplitOptions.RemoveEmptyEntries)
            );

         // Örnek olarak, string alanların maksimum uzunluğunu belirleyelim.
        modelBuilder.Entity<Product>()
        .Property(p => p.Name)
        .HasMaxLength(255);

        modelBuilder.Entity<Product>()
            .Property(p => p.Description)
            .HasMaxLength(2000);
        
       modelBuilder.Entity<Product>()
          .Property(p => p.Size)
            .HasMaxLength(50);

        modelBuilder.Entity<Product>()
          .Property(p => p.Material)
          .HasMaxLength(50);
        
        modelBuilder.Entity<Product>()
          .Property(p => p.Color)
            .HasMaxLength(50);
        
        modelBuilder.Entity<Product>()
          .Property(p => p.Firmness)
            .HasMaxLength(50);
    }
}