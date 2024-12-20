using ComputerStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerStore.Data
{
    public class WriteOffConfiguration : IEntityTypeConfiguration<WriteOffEntity>
    {
        public void Configure(EntityTypeBuilder<WriteOffEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(w => w.Manager).WithMany().HasForeignKey(w => w.ManagerId);

            builder.HasMany(w => w.WriteOffItems).WithOne(w => w.WriteOff).HasForeignKey(w => w.WriteOffId);
                
        }
    }
}
