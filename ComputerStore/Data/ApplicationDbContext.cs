using Microsoft.EntityFrameworkCore;
using ComputerStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ComputerStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<SaleEntity> Sales { get; set; }
        public DbSet<SaleItemEntity> SaleItems { get; set; }
        public DbSet<SupplierEntity> Suppliers { get; set; }
        public DbSet<SaleStatusEntity> SaleStatuses { get; set; }
        public DbSet<WriteOffEntity> WriteOffs { get; set; }
        public DbSet<WriteOffItemEntity> WriteOffItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new SaleConfiguration());
            modelBuilder.ApplyConfiguration(new SaleItemConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new SaleStatusConfiguration());
            modelBuilder.ApplyConfiguration(new WriteOffConfiguration());
            modelBuilder.ApplyConfiguration(new WriteOffItemConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
