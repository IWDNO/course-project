using ComputerStore.Data;
using ComputerStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ComputerStore.Controllers
{
    [Authorize(Roles = "Seller")]
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var sellerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Получаем все продажи, совершенные текущим продавцом
            var sales = _context.Sales
                .Where(s => s.SellerId == sellerId)
                .Include(s => s.Status)
                .Include(s => s.Customer)
                .OrderByDescending(s => s.SaleDate)
                .ToList();

            return View(sales);
        }

        [HttpGet]
        public async Task<IActionResult> GetSaleDetails(Guid saleId)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleItems)
                .ThenInclude(si => si.Product)
                .Include(s => s.Status)
                .FirstOrDefaultAsync(s => s.Id == saleId);

            if (sale == null)
            {
                return NotFound();
            }

            return PartialView("_SaleDetailsPartial", sale);
        }

        [HttpGet]
        public IActionResult Create(Guid? productId)
        {
            var viewModel = new CreateSaleViewModel
            {
                AvailableCategories = _context.Categories.ToList(),
                AvailableProducts = _context.Products.ToList(),
                AvailableCustomers = _context.Users.ToList()
            };

            if (productId.HasValue)
            {
                var product = _context.Products.Find(productId.Value);
                if (product != null)
                {
                    viewModel.SaleItems.Add(new SaleItemEntity
                    {
                        ProductId = product.Id,
                        Quantity = 1,
                        Price = product.Price
                    });
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetProductsByCategory(Guid categoryId)
        {
            var products = _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToList();

            return Json(products);
        }

        [HttpGet]
        public IActionResult GetProductStock(Guid productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                return NotFound();
            }

            return Json(new { stockQuantity = product.StockQuantity });
        }

        [HttpPost]
        public IActionResult Create(CreateSaleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var validSaleItems = viewModel.SaleItems
                    .Where(item => item.Quantity > 0)
                    .ToList();

                foreach (var item in validSaleItems)
                {
                    var product = _context.Products.Find(item.ProductId);
                    if (product == null || product.StockQuantity < item.Quantity)
                    {
                        ModelState.AddModelError(string.Empty, $"Not enough stock for product {product?.Name}");
                        viewModel.AvailableProducts = _context.Products.ToList();
                        viewModel.AvailableCustomers = _context.Users.ToList();
                        return View(viewModel);
                    }
                }

                var sale = new SaleEntity
                {
                    Id = Guid.NewGuid(),
                    SaleDate = DateTime.UtcNow,
                    SaleItems = new List<SaleItemEntity>(),
                    SellerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    CustomerId = viewModel.CustomerId == "-- Select Customer --" ? null : viewModel.CustomerId, 
                    TotalPrice = 0,
                    StatusId = _context.SaleStatuses.First(s => s.Name == "Completed").Id
                };

                foreach (var item in validSaleItems)
                {
                    var product = _context.Products.Find(item.ProductId);
                    if (product != null)
                    {
                        item.Price = product.Price * item.Quantity;

                        product.StockQuantity -= item.Quantity;

                        sale.SaleItems.Add(item);

                        sale.TotalPrice += item.Price;
                    }
                }

                _context.Sales.Add(sale);
                _context.SaveChanges();

                return RedirectToAction("Create");
            }

            viewModel.AvailableProducts = _context.Products.ToList();
            viewModel.AvailableCustomers = _context.Users.ToList();
            return View(viewModel);
        }
    }
}