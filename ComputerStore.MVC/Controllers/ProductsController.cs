using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComputerStore.DataAccess;
using ComputerStore.DataAccess.Entities;
using ComputerStore.Application;

namespace ComputerStore.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataManager _dataManager;

        public ProductsController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }


        // GET: Products
        public async Task<IActionResult> Index(Guid? categoryId, int page = 1)
        {
            int pageSize = 10;

            IEnumerable<ProductEntity> products;

            if (categoryId.HasValue)
            {
                products = await _dataManager.ProductsRepository.GetProductsByCategoryIdAsync(categoryId.Value, page, pageSize);
            }
            else
            {
                products = await _dataManager.ProductsRepository.GetProductsByPageAsync(page, pageSize);
            }

            ViewBag.CategoryId = categoryId;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productEntity = await _dataManager.ProductsRepository.GetProductByIdAsync(id.Value, includeRelated: true);
            if (productEntity == null)
            {
                return NotFound();
            }

            return View(productEntity);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _dataManager.CategoriesRepository.GetAllCategoriesAsync();
            var suppliers = await _dataManager.SuppliersRepository.GetAllSuppliersAsync();

            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            ViewData["SupplierId"] = new SelectList(suppliers, "Id", "Name");

            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,StockQuantity,CategoryId,SupplierId")] ProductEntity productEntity)
        {
            if (ModelState.IsValid)
            {
                productEntity.Id = Guid.NewGuid();
                await _dataManager.ProductsRepository.AddProductAsync(productEntity);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _dataManager.CategoriesRepository.GetAllCategoriesAsync();
            var suppliers = await _dataManager.SuppliersRepository.GetAllSuppliersAsync();

            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name", productEntity.CategoryId);
            ViewData["SupplierId"] = new SelectList(suppliers, "Id", "Name", productEntity.SupplierId);

            return View(productEntity);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productEntity = await _dataManager.ProductsRepository.GetProductByIdAsync(id.Value);
            if (productEntity == null)
            {
                return NotFound();
            }

            var categories = await _dataManager.CategoriesRepository.GetAllCategoriesAsync();
            var suppliers = await _dataManager.SuppliersRepository.GetAllSuppliersAsync();

            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name", productEntity.CategoryId);
            ViewData["SupplierId"] = new SelectList(suppliers, "Id", "Name", productEntity.SupplierId);

            return View(productEntity);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Price,StockQuantity,CategoryId,SupplierId")] ProductEntity productEntity)
        {
            if (id != productEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _dataManager.ProductsRepository.UpdateProductAsync(productEntity);
                }
                catch (Exception)
                {
                    if (!await ProductEntityExistsAsync(productEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var categories = await _dataManager.CategoriesRepository.GetAllCategoriesAsync();
            var suppliers = await _dataManager.SuppliersRepository.GetAllSuppliersAsync();

            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name", productEntity.CategoryId);
            ViewData["SupplierId"] = new SelectList(suppliers, "Id", "Name", productEntity.SupplierId);

            return View(productEntity);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productEntity = await _dataManager.ProductsRepository.GetProductByIdAsync(id.Value, includeRelated: true);
            if (productEntity == null)
            {
                return NotFound();
            }

            return View(productEntity);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _dataManager.ProductsRepository.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductEntityExistsAsync(Guid id)
        {
            var product = await _dataManager.ProductsRepository.GetProductByIdAsync(id);
            return product != null;
        }
    }
}
