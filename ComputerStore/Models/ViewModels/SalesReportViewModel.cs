using Microsoft.AspNetCore.Identity;

namespace ComputerStore.Models.ViewModels
{
    public class SalesReportViewModel
    {
        public string SellerEmail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<SaleEntity> Sales { get; set; } = new List<SaleEntity>();
        public decimal TotalRevenue { get; set; }
        public List<IdentityUser> Sellers { get; set; } = new List<IdentityUser>();
    }
}
