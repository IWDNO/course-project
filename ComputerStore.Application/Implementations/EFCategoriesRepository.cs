using ComputerStore.Application.Interfaces;
using ComputerStore.DataAccess;
using ComputerStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Application.Implementations
{
    public class EFCategoriesRepository : ICategoriesRepository
    {
        private readonly ComputerStoreDBContext _dbContext;

        public EFCategoriesRepository(ComputerStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCategoryAsync(CategoryEntity category)
        {
            await _dbContext.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            await _dbContext.Categories.Where(c => c.Id == id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<CategoryEntity>> GetAllCategoriesAsync(bool includeProducts = false)
        {
            return await _dbContext.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<CategoryEntity?> GetCategoryByIdAsync(Guid id, bool includeProducts = false)
        {
            return await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task UpdateCategoryAsync(CategoryEntity category)
        {
            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();

        }
    }

    public class EFSuppliersRepository : ISuppliersRepository
    {
        private readonly ComputerStoreDBContext _dbContext;

        public EFSuppliersRepository(ComputerStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<SupplierEntity>> GetAllSuppliersAsync(bool includeProducts = false)
        {
            return await _dbContext.Suppliers.AsNoTracking().ToListAsync();
        }

        public async Task<SupplierEntity?> GetSupplierByIdAsync(Guid id, bool includeProducts = false)
        {
            return await _dbContext.Suppliers.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddSupplierAsync(SupplierEntity supplier)
        {
            await _dbContext.Suppliers.AddAsync(supplier);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateSupplierAsync(SupplierEntity supplier)
        {
            _dbContext.Suppliers.Update(supplier);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSupplierAsync(Guid id)
        {
            await _dbContext.Suppliers.Where(s => s.Id == id).ExecuteDeleteAsync();
        }
    }

    public class EFProductsRepository : IProductsRepository
    {
        private readonly ComputerStoreDBContext _dbContext;

        public EFProductsRepository(ComputerStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductEntity>> GetAllProductsAsync(bool includeRelated = false)
        {
            return await _dbContext.Products.AsNoTracking().ToListAsync();
        }

        public async Task<ProductEntity?> GetProductByIdAsync(Guid id, bool includeRelated = false)
        {
            return await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProductAsync(ProductEntity product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(ProductEntity product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await _dbContext.Products.Where(p => p.Id == id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<ProductEntity>> GetProductsByPageAsync(int page, int pageSize)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }

    public class EFRolesRepository : IRolesRepository
    {
        private readonly ComputerStoreDBContext _dbContext;

        public EFRolesRepository(ComputerStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<RoleEntity>> GetAllRolesAsync(bool includeRelated = false)
        {
            return await _dbContext.Roles.AsNoTracking().ToListAsync();
        }

        public async Task<RoleEntity?> GetRoleByIdAsync(Guid id, bool includeRelated = false)
        {
            return await _dbContext.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddRoleAsync(RoleEntity role)
        {
            await _dbContext.Roles.AddAsync(role);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(RoleEntity role)
        {
            _dbContext.Roles.Update(role);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(Guid id)
        {
            await _dbContext.Roles.Where(r => r.Id == id).ExecuteDeleteAsync();
        }
    }

    public class EFSalesRepository : ISalesRepository
    {
        private readonly ComputerStoreDBContext _dbContext;

        public EFSalesRepository(ComputerStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<SaleEntity>> GetAllSalesAsync(bool includeRelated = false)
        {
            return await _dbContext.Sales.AsNoTracking().ToListAsync();
        }

        public async Task<SaleEntity?> GetSaleByIdAsync(Guid id, bool includeRelated = false)
        {
            return await _dbContext.Sales.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddSaleAsync(SaleEntity sale)
        {
            await _dbContext.Sales.AddAsync(sale);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateSaleAsync(SaleEntity sale)
        {
            _dbContext.Sales.Update(sale);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSaleAsync(Guid id)
        {
            await _dbContext.Sales.Where(s => s.Id == id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<SaleEntity>> GetSalesByPageAsync(int page, int pageSize)
        {
            return await _dbContext.Sales
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }

    public class EFSaleItemsRepository : ISaleItemsRepository
    {
        private readonly ComputerStoreDBContext _dbContext;

        public EFSaleItemsRepository(ComputerStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddSaleAsync(SaleEntity sale)
        {
            await _dbContext.Sales.AddAsync(sale);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateSaleAsync(SaleEntity sale)
        {
            _dbContext.Sales.Update(sale);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSaleAsync(Guid id)
        {
            await _dbContext.Sales.Where(s => s.Id == id).ExecuteDeleteAsync();
        }
    }

    public class EFUsersRepository : IUsersRepository
    {
        private readonly ComputerStoreDBContext _dbContext;

        public EFUsersRepository(ComputerStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UserEntity>> GetAllUsersAsync(bool includeRelated = false)
        {
            return await _dbContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<UserEntity?> GetUserByIdAsync(Guid id, bool includeRelated = false)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddUserAsync(UserEntity user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserEntity user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _dbContext.Users.Where(u => u.Id == id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<UserEntity>> GetUsersByPageAsync(int page, int pageSize)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
