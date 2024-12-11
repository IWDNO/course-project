using ComputerStore.Application;
using ComputerStore.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ComputerStore.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataManager _dataManager;

        public HomeController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _dataManager.CategoriesRepository.GetAllCategoriesAsync();
            return View(categories);
        }

        public async Task<IActionResult> ProductsByCategory(Guid categoryId, int page = 1)
        {
            int pageSize = 10;
            var products = await _dataManager.ProductsRepository.GetProductsByCategoryIdAsync(categoryId, page, pageSize);
            ViewBag.CategoryId = categoryId;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            return View(products);
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
