using ComputerStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerStore.DataAccess.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<SupplierEntity>
    {
        public void Configure(EntityTypeBuilder<SupplierEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(s => s.Products).WithOne(p => p.Supplier).HasForeignKey(p => p.SupplierId);
        }
    }
}
