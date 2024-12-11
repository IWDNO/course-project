using ComputerStore.DataAccess.Entities;

namespace ComputerStore.Application.Interfaces
{
    public interface ISaleItemsRepository
    {
        Task AddSaleAsync(SaleEntity sale);
        Task UpdateSaleAsync(SaleEntity sale);
        Task DeleteSaleAsync(Guid id);
    }
}