using ComputerStore.DataAccess.Entities;

namespace ComputerStore.Application.Interfaces
{
    public interface IProductsRepository
    {
        Task<IEnumerable<ProductEntity>> GetAllProductsAsync(bool includeRelated = false);
        Task<ProductEntity?> GetProductByIdAsync(Guid id, bool includeRelated = false);
        Task AddProductAsync(ProductEntity product);
        Task UpdateProductAsync(ProductEntity product);
        Task DeleteProductAsync(Guid id);
        Task<IEnumerable<ProductEntity>> GetProductsByPageAsync(int page, int pageSize);
        Task<IEnumerable<ProductEntity>> GetProductsByCategoryIdAsync(Guid categoryId, int page, int pageSize);
    }
}