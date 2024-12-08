using ComputerStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerStore.DataAccess.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<SaleEntity>
    {
        public void Configure(EntityTypeBuilder<SaleEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.
                HasMany(s => s.SaleItems).
                WithOne(s => s.Sale).
                HasForeignKey(s => s.SaleId);

            builder.
                HasOne(s => s.Seller).
                WithMany(u => u.Sales).
                HasForeignKey(s => s.SellerId);
        }
    }
}
