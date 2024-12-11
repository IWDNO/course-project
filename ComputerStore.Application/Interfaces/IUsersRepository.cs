using ComputerStore.DataAccess.Entities;

namespace ComputerStore.Application.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<UserEntity>> GetAllUsersAsync(bool includeRelated = false);
        Task<UserEntity?> GetUserByIdAsync(Guid id, bool includeRelated = false);
        Task AddUserAsync(UserEntity user);
        Task UpdateUserAsync(UserEntity user);
        Task DeleteUserAsync(Guid id);
        Task<IEnumerable<UserEntity>> GetUsersByPageAsync(int page, int pageSize);
    }
}