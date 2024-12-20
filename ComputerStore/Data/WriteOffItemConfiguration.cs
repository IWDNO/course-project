using ComputerStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerStore.Data
{
    public class WriteOffItemConfiguration : IEntityTypeConfiguration<WriteOffItemEntity>
    {
        public void Configure(EntityTypeBuilder<WriteOffItemEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(w => w.Product).WithMany(p => p.WriteOffItems).HasForeignKey(w => w.ProductId);

            builder.HasOne(w => w.WriteOff).WithMany(w => w.WriteOffItems).HasForeignKey(w => w.WriteOffId);

        }
    }
}
