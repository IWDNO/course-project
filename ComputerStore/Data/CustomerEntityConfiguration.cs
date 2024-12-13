using ComputerStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerStore.Data
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.IdentityUser)
                   .WithMany()
                   .HasForeignKey(c => c.IdentityUserId);

            builder.HasMany(c => c.Sales).WithOne(s => s.Customer).HasForeignKey(s => s.CustomerId);
        }
    }
}
