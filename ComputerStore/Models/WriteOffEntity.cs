using Microsoft.AspNetCore.Identity;

namespace ComputerStore.Models
{
    public class WriteOffEntity
    {
        public Guid Id { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime WriteOffDate { get; set; }

        public string ManagerId { get; set; } = string.Empty; 
        public IdentityUser? Manager { get; set; }

        public List<WriteOffItemEntity> WriteOffItems { get; set; } = [];
    }
}

