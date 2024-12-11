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
}