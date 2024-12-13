using Microsoft.AspNetCore.Identity;
using System.Data;

namespace ComputerStore.Models
{
    public class ApplicationUserEntity 
    {
        public Guid RoleId { get; set; }

        public List<SaleEntity> Sales { get; set; } = [];
    }
}

