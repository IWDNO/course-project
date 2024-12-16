using Microsoft.AspNetCore.Identity;

namespace ComputerStore.Models.ViewModels
{
    public class SalesReportViewModel
    {
        public string SellerEmail { get; set; } // Email продавца
        public DateTime StartDate { get; set; } // Начальная дата
        public DateTime EndDate { get; set; } // Конечная дата
        public List<SaleEntity> Sales { get; set; } = new List<SaleEntity>(); // Список продаж
        public decimal TotalRevenue { get; set; } // Общий оборот
        public List<IdentityUser> Sellers { get; set; } = new List<IdentityUser>();
    }
}
