using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.EF;
using DAL.Entities;

namespace CarServiceRepairsMVC.Controllers
{
    public class AutosController : Controller
    {
        private readonly CarServiceDbContext _context;

        public AutosController(CarServiceDbContext context)
        {
            _context = context;
        }

        // GET: Autos
        public async Task<IActionResult> Index()
        {
            var carServiceDbContext = await _context.Autos.Include(a => a.Owner).ToListAsync();
            var aee = carServiceDbContext.First();
            return View(carServiceDbContext);
        }

        // GET: Autos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Autos == null)
            {
                return NotFound();
            }

            var auto = await _context.Autos
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(m => m.AutoId == id);
            if (auto == null)
            {
                return NotFound();
            }

            return View(auto);
        }

        // GET: Autos/Create
        public IActionResult Create()
        {
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "Email");
            return View();
        }

        // POST: Autos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AutoId,Model,VinCode,TechPass,OwnerId")] Auto auto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "Email", auto.OwnerId);
            return View(auto);
        }

        // GET: Autos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Autos == null)
            {
                return NotFound();
            }

            var auto = await _context.Autos.FindAsync(id);
            if (auto == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "Email", auto.OwnerId);
            return View(auto);
        }

        // POST: Autos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AutoId,Model,VinCode,TechPass,OwnerId")] Auto auto)
        {
            if (id != auto.AutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoExists(auto.AutoId))
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
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "Email", auto.OwnerId);
            return View(auto);
        }

        // GET: Autos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Autos == null)
            {
                return NotFound();
            }

            var auto = await _context.Autos
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(m => m.AutoId == id);
            if (auto == null)
            {
                return NotFound();
            }

            return View(auto);
        }

        // POST: Autos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Autos == null)
            {
                return Problem("Entity set 'CarServiceDbContext.Autos'  is null.");
            }
            var auto = await _context.Autos.FindAsync(id);
            if (auto != null)
            {
                _context.Autos.Remove(auto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutoExists(int id)
        {
          return (_context.Autos?.Any(e => e.AutoId == id)).GetValueOrDefault();
        }
    }
}
