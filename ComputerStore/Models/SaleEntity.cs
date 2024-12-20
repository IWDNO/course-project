using Microsoft.AspNetCore.Identity;

namespace ComputerStore.Models
{
    public class SaleEntity
    {
        public Guid Id { get; set; }
        public DateTime SaleDate { get; set; }

        public List<SaleItemEntity> SaleItems { get; set; } = [];

        public string? SellerId { get; set; } 
        public IdentityUser? Seller { get; set; } 

        public string? CustomerId { get; set; } 
        public IdentityUser? Customer { get; set; }

        public decimal TotalPrice { get; set; }

        public Guid StatusId { get; set; }
        public SaleStatusEntity? Status { get; set; }
    }
}

