using ComputerStore.Data;
using ComputerStore.Models;
using ComputerStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[Authorize(Roles = "Seller")] // Доступ только для продавцов
public class SalesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public SalesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // Отображение формы для оформления продажи
    public async Task<IActionResult> Create()
    {
        var users = _userManager.Users.AsQueryable();
        var customerIds = await _userManager.GetUsersInRoleAsync("Customer");
        users = users.Where(u => customerIds.Contains(u));

        var model = new SaleViewModel
        {
            Customers = await _context.Customers.ToListAsync(),
            Products = await _context.Products.ToListAsync()
        };

        return View(model);
    }

    // Обработка данных из формы
    [HttpPost]
    public async Task<IActionResult> Create(SaleViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Customers = await _context.Customers.ToListAsync();
            model.Products = await _context.Products.ToListAsync();
            return View(model);
        }

        // Получаем текущего продавца (авторизованного пользователя)
        var seller = await _context.Workers.FirstOrDefaultAsync(w => w.IdentityUserId == _userManager.GetUserId(User));
        if (seller == null)
        {
            ModelState.AddModelError(string.Empty, "Seller not found.");
            return View(model);
        }

        // Создаем продажу
        var sale = new SaleEntity
        {
            Id = Guid.NewGuid(),
            SaleDate = DateTime.UtcNow,
            SellerId = seller.Id,
            CustomerId = model.CustomerId,
            SaleItems = model.SaleItems.Select(item => new SaleItemEntity
            {
                Id = Guid.NewGuid(),
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList()
        };

        // Рассчитываем общую стоимость
        sale.TotalPrice = sale.SaleItems.Sum(item => item.Price * item.Quantity);

        // Проверяем наличие товаров на складе
        foreach (var item in sale.SaleItems)
        {
            var product = await _context.Products.FindAsync(item.ProductId);
            if (product == null || product.StockQuantity < item.Quantity)
            {
                ModelState.AddModelError(string.Empty, $"Not enough stock for product {product?.Name}.");
                model.Customers = await _context.Customers.ToListAsync();
                model.Products = await _context.Products.ToListAsync();
                return View(model);
            }

            // Уменьшаем количество товара на складе
            product.StockQuantity -= item.Quantity;
        }

        // Сохраняем продажу в базе данных
        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }
}