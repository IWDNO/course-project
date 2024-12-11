using ComputerStore.Application.Interfaces;
using ComputerStore.DataAccess;
using ComputerStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Application.Implementations
{
    public class EFRolesRepository : IRolesRepository
    {
        private readonly ComputerStoreDBContext _dbContext;

        public EFRolesRepository(ComputerStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<RoleEntity>> GetAllRolesAsync(bool includeRelated = false)
        {
            return await _dbContext.Roles.AsNoTracking().ToListAsync();
        }

        public async Task<RoleEntity?> GetRoleByIdAsync(Guid id, bool includeRelated = false)
        {
            return await _dbContext.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddRoleAsync(RoleEntity role)
        {
            await _dbContext.Roles.AddAsync(role);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(RoleEntity role)
        {
            _dbContext.Roles.Update(role);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(Guid id)
        {
            await _dbContext.Roles.Where(r => r.Id == id).ExecuteDeleteAsync();
        }
    }
}
