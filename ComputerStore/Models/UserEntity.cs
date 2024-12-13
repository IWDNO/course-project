using Microsoft.AspNetCore.Identity;
using System.Data;

namespace ComputerStore.Models
{
    public class UserEntity : IdentityUser
    {
        public Guid RoleId { get; set; }
        
        public RoleEntity? Role { get; set; }

        public List<SaleEntity> Sales { get; set; } = [];
    }
}

