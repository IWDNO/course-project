using ComputerStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerStore.Data
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUserEntity>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserEntity> builder)
        {
            //builder.HasKey(x => x.Id);

            //builder.HasMany(u => u.Sales).WithOne(s => s.Seller).HasForeignKey(s => s.SellerId);
        }
    }
}
