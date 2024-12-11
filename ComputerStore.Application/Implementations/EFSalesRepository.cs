using ComputerStore.Application.Interfaces;
using ComputerStore.DataAccess;
using ComputerStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Application.Implementations
{
    public class EFSalesRepository : ISalesRepository
    {
        private readonly ComputerStoreDBContext _dbContext;

        public EFSalesRepository(ComputerStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<SaleEntity>> GetAllSalesAsync(bool includeRelated = false)
        {
            return await _dbContext.Sales.AsNoTracking().ToListAsync();
        }

        public async Task<SaleEntity?> GetSaleByIdAsync(Guid id, bool includeRelated = false)
        {
            return await _dbContext.Sales.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
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

        public async Task<IEnumerable<SaleEntity>> GetSalesByPageAsync(int page, int pageSize)
        {
            return await _dbContext.Sales
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
