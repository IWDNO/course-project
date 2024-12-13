using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComputerStore.Data;
using ComputerStore.Models;

namespace ComputerStore.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuppliersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Suppliers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Suppliers.ToListAsync());
        }

        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplierEntity = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplierEntity == null)
            {
                return NotFound();
            }

            return View(supplierEntity);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ContactInfo")] SupplierEntity supplierEntity)
        {
            if (ModelState.IsValid)
            {
                supplierEntity.Id = Guid.NewGuid();
                _context.Add(supplierEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplierEntity);
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplierEntity = await _context.Suppliers.FindAsync(id);
            if (supplierEntity == null)
            {
                return NotFound();
            }
            return View(supplierEntity);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,ContactInfo")] SupplierEntity supplierEntity)
        {
            if (id != supplierEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplierEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierEntityExists(supplierEntity.Id))
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
            return View(supplierEntity);
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplierEntity = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplierEntity == null)
            {
                return NotFound();
            }

            return View(supplierEntity);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supplierEntity = await _context.Suppliers.FindAsync(id);
            if (supplierEntity != null)
            {
                _context.Suppliers.Remove(supplierEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierEntityExists(Guid id)
        {
            return _context.Suppliers.Any(e => e.Id == id);
        }
    }
}
