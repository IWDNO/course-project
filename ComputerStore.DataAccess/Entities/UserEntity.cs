using Microsoft.AspNetCore.Identity;
using System.Data;

namespace ComputerStore.DataAccess.Entities
{
    public class UserEntity : IdentityUser<Guid>
    {

        public Guid RoleId { get; set; }
        public RoleEntity? Role { get; set; }
        public List<SaleEntity> Sales { get; set; } = new List<SaleEntity>();
    }
}

