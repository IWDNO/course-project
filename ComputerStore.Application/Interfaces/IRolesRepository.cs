using ComputerStore.DataAccess.Entities;

namespace ComputerStore.Application.Interfaces
{
    public interface IRolesRepository
    {
        Task<IEnumerable<RoleEntity>> GetAllRolesAsync(bool includeRelated = false);
        Task<RoleEntity?> GetRoleByIdAsync(Guid id, bool includeRelated = false);
        Task AddRoleAsync(RoleEntity role);
        Task UpdateRoleAsync(RoleEntity role);
        Task DeleteRoleAsync(Guid id);
    }
}