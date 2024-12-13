using ComputerStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerStore.Data
{
    public class WorkerEntityConfiguration : IEntityTypeConfiguration<WorkerEntity>
    {
        public void Configure(EntityTypeBuilder<WorkerEntity> builder)
        {
            builder.HasKey(w => w.Id);

            builder.HasOne(w => w.IdentityUser)
                   .WithMany()
                   .HasForeignKey(w => w.IdentityUserId);

            builder.HasMany(c => c.Sales).WithOne(s => s.Seller).HasForeignKey(s => s.SellerId);
        }
    }
}
