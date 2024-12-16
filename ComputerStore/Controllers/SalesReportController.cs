using ComputerStore.Data;
using ComputerStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SalesReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new SalesReportViewModel
            {
                StartDate = DateTime.Now.AddDays(-7),
                EndDate = DateTime.Now,
                Sellers = await _context.Users.ToListAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetReport(string sellerEmail, DateTime startDate, DateTime endDate)
        {
            if (startDate.Kind != DateTimeKind.Utc)
                startDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);

            if (endDate.Kind != DateTimeKind.Utc)
                endDate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

            endDate = endDate.AddDays(1).AddMilliseconds(-1);

            Console.WriteLine(startDate);
            Console.WriteLine(endDate);

            var sales = await _context.Sales
                .Include(s => s.SaleItems)
                .Include(s => s.Seller)
                .Include(s => s.Customer)
                .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
                .OrderBy(s => s.SaleDate)
                .ToListAsync();

            if (!string.IsNullOrEmpty(sellerEmail))
            {
                sales = sales.Where(s => s.Seller != null && s.Seller.Email == sellerEmail).ToList();
            }

            var totalRevenue = sales.Sum(s => s.TotalPrice);

            var viewModel = new SalesReportViewModel
            {
                SellerEmail = sellerEmail,
                StartDate = startDate,
                EndDate = endDate,
                Sales = sales,
                TotalRevenue = totalRevenue
            };

            return PartialView("_SalesReportPartial", viewModel);
        }
    }
}