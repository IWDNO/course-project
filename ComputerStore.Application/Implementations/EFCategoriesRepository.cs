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
}
