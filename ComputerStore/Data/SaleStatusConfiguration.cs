using ComputerStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerStore.Data
{
    public class SaleStatusConfiguration : IEntityTypeConfiguration<SaleStatusEntity>
    {
        public void Configure(EntityTypeBuilder<SaleStatusEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(s => s.Sales).WithOne(s => s.Status).HasForeignKey(s => s.StatusId);
        }
    }
}
