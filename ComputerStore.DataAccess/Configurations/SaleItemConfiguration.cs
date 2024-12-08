using ComputerStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerStore.DataAccess.Configurations
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItemEntity>
    {
        public void Configure(EntityTypeBuilder<SaleItemEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(s => s.Product).WithMany(p => p.SaleItems).HasForeignKey(s => s.ProductId);

            builder.HasOne(s => s.Sale).WithMany(s => s.SaleItems).HasForeignKey(s => s.SaleId);
        }
    }
}
