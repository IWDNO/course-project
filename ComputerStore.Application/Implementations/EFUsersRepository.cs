using ComputerStore.Application.Interfaces;
using ComputerStore.DataAccess;
using ComputerStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Application.Implementations
{
    public class EFUsersRepository : IUsersRepository
    {
        private readonly ComputerStoreDBContext _dbContext;

        public EFUsersRepository(ComputerStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UserEntity>> GetAllUsersAsync(bool includeRelated = false)
        {
            return await _dbContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<UserEntity?> GetUserByIdAsync(Guid id, bool includeRelated = false)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddUserAsync(UserEntity user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserEntity user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _dbContext.Users.Where(u => u.Id == id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<UserEntity>> GetUsersByPageAsync(int page, int pageSize)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
