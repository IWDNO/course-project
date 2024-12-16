using ComputerStore.Data;
using ComputerStore.Models;
using ComputerStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace ComputerStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Products(Guid id, int page = 1, int pageSize = 10)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .OrderBy(c => c.Name)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            // Пагинация товаров
            var products = category.Products
                .OrderBy(p => p.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalProducts = category.Products.Count;
            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);


            // Получаем текущий Sale со статусом "In Process" для текущего пользователя
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentSale = await _context.Sales
                .Include(s => s.SaleItems)
                .FirstOrDefaultAsync(s => s.CustomerId == userId && s.Status.Name == "In Process");

            var viewModel = new ProductsViewModel
            {
                Category = category,
                Products = products,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                CurrentSale = currentSale, // Передаем текущий Sale в модель
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddToCart(Guid productId, int quantity)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Проверяем, есть ли у пользователя Sale со статусом "In Process"
            var currentSale = await _context.Sales
                .Include(s => s.SaleItems)
                .FirstOrDefaultAsync(s => s.CustomerId == userId && s.Status.Name == "In Process");

            // Если Sale не существует, создаем новый
            if (currentSale == null)
            {
                currentSale = new SaleEntity
                {
                    Id = Guid.NewGuid(),
                    SaleDate = DateTime.UtcNow,
                    CustomerId = userId,
                    StatusId = _context.SaleStatuses.First(s => s.Name == "In Process").Id,
                    SaleItems = new List<SaleItemEntity>()
                };

                _context.Sales.Add(currentSale);
            }

            var product = await _context.Products.FindAsync(productId);
            // Проверяем, есть ли уже такой товар в Sale
            var existingItem = currentSale.SaleItems.FirstOrDefault(item => item.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                existingItem.Price += quantity * product.Price;
                currentSale.TotalPrice += quantity * product.Price;
            }
            else
            {
                // Добавляем новый товар в Sale
                if (product != null)
                {
                    var saleItem = new SaleItemEntity
                    {
                        SaleId = currentSale.Id,
                        ProductId = productId,
                        Quantity = quantity,
                        Price = product.Price * quantity
                    };
                    currentSale.SaleItems.Add(saleItem);
                    currentSale.TotalPrice += saleItem.Price;
                }
            }
            product.StockQuantity -= quantity;

            await _context.SaveChangesAsync();

            return RedirectToAction("Products", new { id = _context.Products.Find(productId).CategoryId });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
