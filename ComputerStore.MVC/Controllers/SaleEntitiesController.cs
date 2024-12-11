using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComputerStore.DataAccess;
using ComputerStore.DataAccess.Entities;
using ComputerStore.Application;
using ComputerStore.MVC.ViewModels;

namespace ComputerStore.MVC.Controllers
{
    public class SaleEntitiesController : Controller
    {
        private readonly ComputerStoreDBContext _context;
        private readonly DataManager _dataManager;

        public SaleEntitiesController(ComputerStoreDBContext context, DataManager dataManager)
        {
            _context = context;
            _dataManager = dataManager;
        }

        // GET: SaleEntities
        public async Task<IActionResult> Index()
        {
            var computerStoreDBContext = _context.Sales.Include(s => s.Seller);
            return View(await computerStoreDBContext.ToListAsync());
        }

        // GET: SaleEntities/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleEntity = await _context.Sales
                .Include(s => s.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleEntity == null)
            {
                return NotFound();
            }

            return View(saleEntity);
        }

        // GET: SaleEntities/Create
        public async Task<IActionResult> Create()
        {
            var products = await _dataManager.ProductsRepository.GetAllProductsAsync();
            var sellers = await _dataManager.UsersRepository.GetAllUsersAsync();

            var saleViewModel = new SaleViewModel
            {
                SaleItems = products.Select(p => new SaleItemViewModel
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    Price = p.Price
                }).ToList(),
                Sellers = sellers.ToList()
            };

            return View(saleViewModel);
        }

        // POST: SaleEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sale = new SaleEntity
                {
                    Id = Guid.NewGuid(),
                    SaleDate = DateTime.UtcNow,
                    SellerId = model.SellerId,
                    SaleItems = model.SaleItems.Select(item => new SaleItemEntity
                    {
                        Id = Guid.NewGuid(),
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Price
                    }).ToList()
                };

                await _dataManager.SalesRepository.AddSaleAsync(sale);
                return RedirectToAction("Index", "Home");
            }

            var products = await _dataManager.ProductsRepository.GetAllProductsAsync();
            var sellers = await _dataManager.UsersRepository.GetAllUsersAsync();

            model.SaleItems = products.Select(p => new SaleItemViewModel
            {
                ProductId = p.Id,
                ProductName = p.Name,
                Price = p.Price
            }).ToList();

            model.Sellers = sellers.ToList();

            return View(model);
        }

        // GET: SaleEntities/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleEntity = await _context.Sales.FindAsync(id);
            if (saleEntity == null)
            {
                return NotFound();
            }
            ViewData["SellerId"] = new SelectList(_context.Users, "Id", "Email", saleEntity.SellerId);
            return View(saleEntity);
        }

        // POST: SaleEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,SaleDate,SellerId")] SaleEntity saleEntity)
        {
            if (id != saleEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleEntityExists(saleEntity.Id))
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
            ViewData["SellerId"] = new SelectList(_context.Users, "Id", "Email", saleEntity.SellerId);
            return View(saleEntity);
        }

        // GET: SaleEntities/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleEntity = await _context.Sales
                .Include(s => s.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleEntity == null)
            {
                return NotFound();
            }

            return View(saleEntity);
        }

        // POST: SaleEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var saleEntity = await _context.Sales.FindAsync(id);
            if (saleEntity != null)
            {
                _context.Sales.Remove(saleEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleEntityExists(Guid id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }
    }
}
