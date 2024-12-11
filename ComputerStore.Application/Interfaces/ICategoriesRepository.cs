using ComputerStore.DataAccess.Entities;

namespace ComputerStore.Application.Interfaces
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<CategoryEntity>> GetAllCategoriesAsync(bool includeProducts = false);
        Task<CategoryEntity?> GetCategoryByIdAsync(Guid id, bool includeProducts = false);
        Task AddCategoryAsync(CategoryEntity category);
        Task UpdateCategoryAsync(CategoryEntity category);
        Task DeleteCategoryAsync(Guid id);
    }

    public interface ISuppliersRepository
    {
        Task<IEnumerable<SupplierEntity>> GetAllSuppliersAsync(bool includeProducts = false);
        Task<SupplierEntity?> GetSupplierByIdAsync(Guid id, bool includeProducts = false);
        Task AddSupplierAsync(SupplierEntity supplier);
        Task UpdateSupplierAsync(SupplierEntity supplier);
        Task DeleteSupplierAsync(Guid id);
    }

    public interface IProductsRepository
    {
        Task<IEnumerable<ProductEntity>> GetAllProductsAsync(bool includeRelated = false);
        Task<ProductEntity?> GetProductByIdAsync(Guid id, bool includeRelated = false);
        Task AddProductAsync(ProductEntity product);
        Task UpdateProductAsync(ProductEntity product);
        Task DeleteProductAsync(Guid id);
        Task<IEnumerable<ProductEntity>> GetProductsByPageAsync(int page, int pageSize);
    }

    public interface IRolesRepository
    {
        Task<IEnumerable<RoleEntity>> GetAllRolesAsync(bool includeRelated = false);
        Task<RoleEntity?> GetRoleByIdAsync(Guid id, bool includeRelated = false);
        Task AddRoleAsync(RoleEntity role);
        Task UpdateRoleAsync(RoleEntity role);
        Task DeleteRoleAsync(Guid id);
    }

    public interface ISalesRepository
    {
        Task<IEnumerable<SaleEntity>> GetAllSalesAsync(bool includeRelated = false);
        Task<SaleEntity?> GetSaleByIdAsync(Guid id, bool includeRelated = false);
        Task AddSaleAsync(SaleEntity sale);
        Task UpdateSaleAsync(SaleEntity sale);
        Task DeleteSaleAsync(Guid id);
        Task<IEnumerable<SaleEntity>> GetSalesByPageAsync(int page, int pageSize);
    }

    public interface ISaleItemsRepository
    {
        Task AddSaleAsync(SaleEntity sale);
        Task UpdateSaleAsync(SaleEntity sale);
        Task DeleteSaleAsync(Guid id);
    }

    public interface IUsersRepository
    {
        Task<IEnumerable<UserEntity>> GetAllUsersAsync(bool includeRelated = false);
        Task<UserEntity?> GetUserByIdAsync(Guid id, bool includeRelated = false);
        Task AddUserAsync(UserEntity user);
        Task UpdateUserAsync(UserEntity user);
        Task DeleteUserAsync(Guid id);
        Task<IEnumerable<UserEntity>> GetUsersByPageAsync(int page, int pageSize);
    }
}