using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;
using Catalog.LIB.Entities;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Data
{
	public class CatalogDbContext : DbContext
	{
		public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
			: base(options)
		{
		}

		public DbSet<Category> Categories { get; set; } = default!;
		public DbSet<Product> Products { get; set; } = default!;
		public DbSet<Image> Images { get; set; } = default!;
		public DbSet<ProductSize> ProductSizes { get; set; } = default!;
		public DbSet<ProductColor> ProductColors { get; set; } = default!;
		public DbSet<Size> Sizes { get; set; } = default!;
		public DbSet<Color> Colors { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			builder.Entity<Category>()
				.HasMany(e => e.SubCategories)
				.WithOne(e => e.ParentCategory)
				.HasForeignKey(e => e.ParentCategoryId);

			builder.Entity<Category>()
				.HasOne(e => e.Image)
				.WithMany()
				.HasForeignKey(e => e.ImageId);

			builder.Entity<ProductColor>()
				.HasKey(pc => new { pc.ProductId, pc.ColorId });

			builder.Entity<ProductColor>()
				.HasOne(pc => pc.Product)
				.WithMany(p => p.ProductColors)
				.HasForeignKey(pc => pc.ProductId);

			builder.Entity<ProductColor>()
				.HasOne(pc => pc.Color)
				.WithMany()
				.HasForeignKey(pc => pc.ColorId);

			builder.Entity<ProductSize>()
				.HasKey(pst => new { pst.ProductId, pst.SizeId });

			builder.Entity<ProductSize>()
				.HasOne(pst => pst.Product)
				.WithMany(p => p.ProductSizes)
				.HasForeignKey(pst => pst.ProductId);

			builder.Entity<ProductSize>()
				.HasOne(pst => pst.Size)
				.WithMany()
				.HasForeignKey(pst => pst.SizeId);
		}
	}
}