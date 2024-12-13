using System.Data;

namespace ComputerStore.Models
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public Guid RoleId { get; set; }
        
        public RoleEntity? Role { get; set; }

        public List<SaleEntity> Sales { get; set; } = [];
    }
}

