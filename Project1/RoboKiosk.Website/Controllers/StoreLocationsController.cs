using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using data_access.Entities;

namespace RoboKiosk.Website.Controllers
{
    public class StoreLocationsController : Controller
    {
        private readonly caproj0Context _context;

        public StoreLocationsController(caproj0Context context)
        {
            _context = context;
        }

        // GET: StoreLocations
        public async Task<IActionResult> Index()
        {
            var caproj0Context = _context.StoreLocation.Include(s => s.ManagerNavigation);
            return View(await caproj0Context.ToListAsync());
        }

        // GET: StoreLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeLocation = await _context.StoreLocation
                .Include(s => s.ManagerNavigation)
                .FirstOrDefaultAsync(m => m.LocationId == id);
            if (storeLocation == null)
            {
                return NotFound();
            }

            return View(storeLocation);
        }

        // GET: StoreLocations/Create
        public IActionResult Create()
        {
            ViewData["Manager"] = new SelectList(_context.Manager, "ManagerId", "ManagerPw");
            return View();
        }

        // POST: StoreLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationId,StoreName,Phone,Manager")] StoreLocation storeLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storeLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Manager"] = new SelectList(_context.Manager, "ManagerId", "ManagerPw", storeLocation.Manager);
            return View(storeLocation);
        }

        // GET: StoreLocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeLocation = await _context.StoreLocation.FindAsync(id);
            if (storeLocation == null)
            {
                return NotFound();
            }
            ViewData["Manager"] = new SelectList(_context.Manager, "ManagerId", "ManagerPw", storeLocation.Manager);
            return View(storeLocation);
        }

        // POST: StoreLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocationId,StoreName,Phone,Manager")] StoreLocation storeLocation)
        {
            if (id != storeLocation.LocationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storeLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreLocationExists(storeLocation.LocationId))
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
            ViewData["Manager"] = new SelectList(_context.Manager, "ManagerId", "ManagerPw", storeLocation.Manager);
            return View(storeLocation);
        }

        // GET: StoreLocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeLocation = await _context.StoreLocation
                .Include(s => s.ManagerNavigation)
                .FirstOrDefaultAsync(m => m.LocationId == id);
            if (storeLocation == null)
            {
                return NotFound();
            }

            return View(storeLocation);
        }

        // POST: StoreLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var storeLocation = await _context.StoreLocation.FindAsync(id);
            _context.StoreLocation.Remove(storeLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreLocationExists(int id)
        {
            return _context.StoreLocation.Any(e => e.LocationId == id);
        }
    }
}
