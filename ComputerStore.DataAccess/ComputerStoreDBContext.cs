using ComputerStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.DataAccess
{
    public class ComputerStoreDBContext : DbContext
    {
        public ComputerStoreDBContext(DbContextOptions<ComputerStoreDBContext> options)
            : base(options)
        {
        }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<SaleEntity> Sales { get; set; }
        public DbSet<SaleItemEntity> SaleItems { get; set; }
        public DbSet<SupplierEntity> Suppliers { get; set; }
        public DbSet<UserEntity> Users { get; set; }

    }
}
