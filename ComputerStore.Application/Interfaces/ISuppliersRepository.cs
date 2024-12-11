using ComputerStore.DataAccess.Entities;

namespace ComputerStore.Application.Interfaces
{
    public interface ISuppliersRepository
    {
        Task<IEnumerable<SupplierEntity>> GetAllSuppliersAsync(bool includeProducts = false);
        Task<SupplierEntity?> GetSupplierByIdAsync(Guid id, bool includeProducts = false);
        Task AddSupplierAsync(SupplierEntity supplier);
        Task UpdateSupplierAsync(SupplierEntity supplier);
        Task DeleteSupplierAsync(Guid id);
    }
}