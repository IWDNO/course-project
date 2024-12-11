using ComputerStore.Application.Interfaces;
using ComputerStore.DataAccess;
using ComputerStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Application.Implementations
{
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

        public async Task<IEnumerable<ProductEntity>> GetProductsByCategoryIdAsync(Guid categoryId, int page, int pageSize)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .Where(p => p.CategoryId == categoryId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
