using ComputerStore.Data;
using ComputerStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            return View();
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
                // Фильтруем товары с ненулевым количеством
                var validSaleItems = viewModel.SaleItems
                    .Where(item => item.Quantity > 0)
                    .ToList();

                // Проверяем, что количество товаров в наличии достаточно
                foreach (var item in validSaleItems)
                {
                    var product = _context.Products.Find(item.ProductId);
                    if (product == null || product.StockQuantity < item.Quantity)
                    {
                        ModelState.AddModelError(string.Empty, $"Not enough stock for product {product?.Name}");
                        viewModel.AvailableProducts = _context.Products.ToList(); // Обновляем список товаров
                        viewModel.AvailableCustomers = _context.Users.ToList(); // Обновляем список покупателей
                        return View(viewModel);
                    }
                }

                // Создаем продажу
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

            // Если валидация не прошла, возвращаем модель с ошибками
            viewModel.AvailableProducts = _context.Products.ToList();
            viewModel.AvailableCustomers = _context.Users.ToList(); // Обновляем список покупателей
            return View(viewModel);
        }
    }
}