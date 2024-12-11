using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputerStore.DataAccess;
using ComputerStore.DataAccess.Entities;

namespace ComputerStore.MVC.Controllers
{
    public class CategoryEntitiesController : Controller
    {
        private readonly ComputerStoreDBContext _context;

        public CategoryEntitiesController(ComputerStoreDBContext context)
        {
            _context = context;
        }

        // GET: CategoryEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: CategoryEntities/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryEntity = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryEntity == null)
            {
                return NotFound();
            }

            return View(categoryEntity);
        }

        // GET: CategoryEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] CategoryEntity categoryEntity)
        {
            if (ModelState.IsValid)
            {
                categoryEntity.Id = Guid.NewGuid();
                _context.Add(categoryEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryEntity);
        }

        // GET: CategoryEntities/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryEntity = await _context.Categories.FindAsync(id);
            if (categoryEntity == null)
            {
                return NotFound();
            }
            return View(categoryEntity);
        }

        // POST: CategoryEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description")] CategoryEntity categoryEntity)
        {
            if (id != categoryEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryEntityExists(categoryEntity.Id))
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
            return View(categoryEntity);
        }

        // GET: CategoryEntities/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryEntity = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryEntity == null)
            {
                return NotFound();
            }

            return View(categoryEntity);
        }

        // POST: CategoryEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var categoryEntity = await _context.Categories.FindAsync(id);
            if (categoryEntity != null)
            {
                _context.Categories.Remove(categoryEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryEntityExists(Guid id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
