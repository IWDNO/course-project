using ComputerStore.Data;
using ComputerStore.Models;
using ComputerStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ComputerStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WriteOffController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WriteOffController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var writeOffs = await _context.WriteOffs
                .Include(w => w.WriteOffItems)
                .ThenInclude(wi => wi.Product)
                .Include(w => w.Manager)
                .OrderByDescending(w => w.WriteOffDate)
                .ToListAsync();

            return View(writeOffs);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new WriteOffViewModel
            {
                Products = await _context.Products.ToListAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WriteOffViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in viewModel.WriteOffItems)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product == null || product.StockQuantity < item.Quantity)
                    {
                        ModelState.AddModelError(string.Empty, $"Недостаточно товаров на складе для {product?.Name}");
                        viewModel.Products = await _context.Products.ToListAsync();
                        return View(viewModel);
                    }
                }

                var writeOff = new WriteOffEntity
                {
                    Id = Guid.NewGuid(),
                    Reason = viewModel.Reason,
                    WriteOffDate = DateTime.UtcNow,
                    ManagerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    WriteOffItems = viewModel.WriteOffItems
                };

                foreach (var item in viewModel.WriteOffItems)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product != null)
                    {
                        product.StockQuantity -= item.Quantity;
                    }
                }

                _context.WriteOffs.Add(writeOff);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            viewModel.Products = await _context.Products.ToListAsync();
            return View(viewModel);
        }
        [HttpGet]

        public async Task<IActionResult> GetProductStockQuantity(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            return Json(new { stockQuantity = product.StockQuantity });
        }
    }
}