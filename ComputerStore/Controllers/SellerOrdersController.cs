using ComputerStore.Data;
using ComputerStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ComputerStore.Controllers
{
    [Authorize(Roles = "Seller")]
    public class SellerOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SellerOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Отображение всех отправленных заказов
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Sales
                .Include(s => s.SaleItems)
                .ThenInclude(si => si.Product)
                .Include(s => s.Status)
                .Where(s => s.Status.Name == "Ordered")
                .ToListAsync();

            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(Guid saleId)
        {
            // Загружаем заказ вместе с его статусом
            var sale = await _context.Sales
                .Include(s => s.Status)
                .FirstOrDefaultAsync(s => s.Id == saleId);

            if (sale != null && sale.Status.Name == "Ordered")
            {
                sale.StatusId = _context.SaleStatuses.First(s => s.Name == "Completed").Id;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CancelOrder(Guid saleId)
        {
            // Загружаем заказ вместе с его статусом и товарами
            var sale = await _context.Sales
                .Include(s => s.Status)
                .Include(s => s.SaleItems)
                .ThenInclude(si => si.Product)
                .FirstOrDefaultAsync(s => s.Id == saleId);

            if (sale != null && sale.Status.Name == "Ordered")
            {
                // Возвращаем товары на склад
                foreach (var item in sale.SaleItems)
                {
                    if (item.Product != null)
                    {
                        item.Product.StockQuantity += item.Quantity;
                    }
                }

                // Меняем статус заказа на "Cancelled"
                sale.StatusId = _context.SaleStatuses.First(s => s.Name == "Cancelled").Id;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}