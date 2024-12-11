using ComputerStore.Application.Interfaces;
using ComputerStore.DataAccess;
using ComputerStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Application.Implementations
{
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
}
