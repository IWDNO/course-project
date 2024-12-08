using ComputerStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerStore.DataAccess.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.
                HasOne(p => p.Category).
                WithMany(c => c.Products).
                HasForeignKey(p => p.CategoryId);

            builder.
                HasOne(p => p.Supplier).
                WithMany(s => s.Products).
                HasForeignKey(p => p.SupplierId);

            builder.
                HasMany(p => p.SaleItems).
                WithOne(s => s.Product).
                HasForeignKey(s =>  s.ProductId);
        }
    }
}
