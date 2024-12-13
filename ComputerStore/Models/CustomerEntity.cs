using Microsoft.AspNetCore.Identity;

namespace ComputerStore.Models
{
    public class CustomerEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string IdentityUserId { get; set; } 

        public IdentityUser? IdentityUser { get; set; }

        public List<SaleEntity> Sales { get; set; } = [];
    }
}

