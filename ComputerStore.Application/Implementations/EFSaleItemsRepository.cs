using ComputerStore.Application.Interfaces;
using ComputerStore.DataAccess;
using ComputerStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Application.Implementations
{
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
}
