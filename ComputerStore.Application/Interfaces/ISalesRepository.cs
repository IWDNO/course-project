using ComputerStore.DataAccess.Entities;

namespace ComputerStore.Application.Interfaces
{
    public interface ISalesRepository
    {
        Task<IEnumerable<SaleEntity>> GetAllSalesAsync(bool includeRelated = false);
        Task<SaleEntity?> GetSaleByIdAsync(Guid id, bool includeRelated = false);
        Task AddSaleAsync(SaleEntity sale);
        Task UpdateSaleAsync(SaleEntity sale);
        Task DeleteSaleAsync(Guid id);
        Task<IEnumerable<SaleEntity>> GetSalesByPageAsync(int page, int pageSize);
    }
}