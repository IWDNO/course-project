using ComputerStore.Data;
using ComputerStore.Models;
using ComputerStore.Models.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ComputerStore.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Отображение всех заказов покупателя
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Получаем все заказы покупателя
            var sales = await _context.Sales
                .Include(s => s.SaleItems)
                .ThenInclude(si => si.Product)
                .Include(s => s.Status)
                .Where(s => s.CustomerId == userId)
                .ToListAsync();

            // Группируем заказы по статусам
            var viewModel = new CustomerOrdersViewModel
            {
                InProcessSales = sales.Where(s => s.Status.Name == "In Process").ToList(),
                OrderedSales = sales.Where(s => s.Status.Name == "Ordered").ToList(),
                CompletedSales = sales.Where(s => s.Status.Name == "Completed").ToList(),
                CancelledSales = sales.Where(s => s.Status.Name == "Cancelled").ToList()
            };

            return View(viewModel);
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
        public async Task<IActionResult> OrderSale(Guid saleId)
        {
            var sale = await _context.Sales
                .Include(s => s.Status) // Загружаем связанный статус
                .FirstOrDefaultAsync(s => s.Id == saleId);

            if (sale != null && sale.Status.Name == "In Process")
            {
                // Меняем статус заказа на "Ordered"
                sale.StatusId = _context.SaleStatuses.First(s => s.Name == "Ordered").Id;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CancelSale(Guid saleId)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleItems) // Загружаем товары в заказе
                    .ThenInclude(si => si.Product) // Загружаем связанные товары
                .Include(s => s.Status) // Загружаем статус заказа
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

        [HttpPost]
        public async Task<IActionResult> DeleteSale(Guid saleId)
        {
            var sale = await _context.Sales
                .Include(s => s.Status)
                .Include(s => s.SaleItems)
                .ThenInclude(si => si.Product)
                .FirstOrDefaultAsync(s => s.Id == saleId);

            if (sale != null && sale.Status.Name == "In Process")
            {
                // Возвращаем товары на склад
                foreach (var item in sale.SaleItems)
                {
                    if (item.Product != null)
                    {
                        item.Product.StockQuantity += item.Quantity;
                    }
                }

                // Удаляем заказ
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditSale(Guid saleId)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleItems)
                .ThenInclude(si => si.Product)
                .FirstOrDefaultAsync(s => s.Id == saleId && s.Status.Name == "In Process");

            if (sale == null)
            {
                return NotFound();
            }

            var viewModel = new CreateSaleViewModel
            {
                SaleItems = sale.SaleItems.ToList(),
                AvailableCategories = await _context.Categories.ToListAsync(),
                AvailableProducts = await _context.Products.ToListAsync(),
                AvailableCustomers = await _context.Users.ToListAsync(),
                CustomerId = sale.CustomerId,
                TotalPrice = sale.TotalPrice
            };

            return View("EditSale", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditSale(CreateSaleViewModel viewModel, Guid saleId)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleItems)
                .ThenInclude(si => si.Product)
                .FirstOrDefaultAsync(s => s.Id == saleId && s.Status.Name == "In Process");

            if (sale == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Фильтруем товары с ненулевым количеством
                var validSaleItems = viewModel.SaleItems
                    .Where(item => item.Quantity > 0)
                    .ToList();

                // Проверяем, что количество товаров в наличии достаточно
                foreach (var item in validSaleItems)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product == null || product.StockQuantity < item.Quantity)
                    {
                        ModelState.AddModelError(string.Empty, $"Not enough stock for product {product?.Name}");
                        viewModel.AvailableProducts = await _context.Products.ToListAsync();
                        viewModel.AvailableCustomers = await _context.Users.ToListAsync();
                        return View(viewModel);
                    }
                }

                // Сравниваем текущие и новые данные заказа
                var existingItems = sale.SaleItems.ToDictionary(item => item.ProductId);
                var updatedItems = validSaleItems.ToDictionary(item => item.ProductId);

                // Возвращаем товары на склад для удаленных позиций
                foreach (var existingItem in existingItems.Values)
                {
                    if (!updatedItems.ContainsKey(existingItem.ProductId))
                    {
                        var product = await _context.Products.FindAsync(existingItem.ProductId);
                        if (product != null)
                        {
                            product.StockQuantity += existingItem.Quantity;
                        }
                    }
                }

                // Обновляем количество товаров на складе для измененных позиций
                foreach (var updatedItem in updatedItems.Values)
                {
                    var product = await _context.Products.FindAsync(updatedItem.ProductId);
                    if (product != null)
                    {
                        if (existingItems.TryGetValue(updatedItem.ProductId, out var existingItem))
                        {
                            // Если товар уже был в заказе, корректируем количество
                            var quantityDifference = updatedItem.Quantity - existingItem.Quantity;
                            product.StockQuantity -= quantityDifference;
                        }
                        else
                        {
                            // Если товар добавлен в заказ, уменьшаем количество на складе
                            product.StockQuantity -= updatedItem.Quantity;
                        }
                    }
                }

                // Обновляем заказ
                sale.SaleItems.Clear();
                foreach (var item in validSaleItems)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product != null)
                    {
                        item.Price = product.Price * item.Quantity;
                        sale.SaleItems.Add(item);
                    }
                }

                sale.TotalPrice = validSaleItems.Sum(item => item.Price);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            // Если валидация не прошла, возвращаем модель с ошибками
            viewModel.AvailableProducts = await _context.Products.ToListAsync();
            viewModel.AvailableCustomers = await _context.Users.ToListAsync();
            return View(viewModel);
        }
    }
}