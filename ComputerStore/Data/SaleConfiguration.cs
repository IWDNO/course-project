using ComputerStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerStore.Data
{
    public class SaleConfiguration : IEntityTypeConfiguration<SaleEntity>
    {
        public void Configure(EntityTypeBuilder<SaleEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(s => s.SaleItems).WithOne(s => s.Sale).HasForeignKey(s => s.SaleId);

            builder.HasOne(s => s.Seller).WithMany().HasForeignKey(s => s.SellerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(s => s.Customer).WithMany().HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(s => s.Status).WithMany(s => s.Sales).HasForeignKey(s => s.StatusId);
        }
    }

    public class SaleStatusConfiguration : IEntityTypeConfiguration<SaleStatusEntity>
    {
        public void Configure(EntityTypeBuilder<SaleStatusEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(s => s.Sales).WithOne(s => s.Status).HasForeignKey(s => s.StatusId);
        }
    }
}
