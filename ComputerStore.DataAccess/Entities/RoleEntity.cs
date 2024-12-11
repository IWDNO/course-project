using Microsoft.AspNetCore.Identity;

namespace ComputerStore.DataAccess.Entities
{
    public class RoleEntity : IdentityRole<Guid>
    {
        public string Description { get; set; } = string.Empty;
        public List<UserEntity> Users { get; set; } = new List<UserEntity>();
    }
}
