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
    public class CustOrdersController : Controller
    {
        private readonly caproj0Context _context;

        public CustOrdersController(caproj0Context context)
        {
            _context = context;
        }

        // GET: CustOrders
        public async Task<IActionResult> Index()
        {
            var caproj0Context = _context.CustOrder.Include(c => c.Customer).Include(c => c.Location);

            return View(await caproj0Context.ToListAsync());



        }

        // GET: CustOrders/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var custOrder = await _context.CustOrder
                .Include(c => c.Customer)
                .Include(c => c.Location)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (custOrder == null)
            {
                return NotFound();
            }

            return View(custOrder);
        }

        // GET: CustOrders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerPw");
            ViewData["LocationId"] = new SelectList(_context.StoreLocation, "LocationId", "Phone");
            return View();
        }

        // POST: CustOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CustomerId,LocationId,OrderDate")] CustOrder custOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(custOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerPw", custOrder.CustomerId);
            ViewData["LocationId"] = new SelectList(_context.StoreLocation, "LocationId", "Phone", custOrder.LocationId);
            return View(custOrder);
        }

        // GET: CustOrders/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var custOrder = await _context.CustOrder.FindAsync(id);
            if (custOrder == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerPw", custOrder.CustomerId);
            ViewData["LocationId"] = new SelectList(_context.StoreLocation, "LocationId", "Phone", custOrder.LocationId);
            return View(custOrder);
        }

        // POST: CustOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("OrderId,CustomerId,LocationId,OrderDate")] CustOrder custOrder)
        {
            if (id != custOrder.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(custOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustOrderExists(custOrder.OrderId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerPw", custOrder.CustomerId);
            ViewData["LocationId"] = new SelectList(_context.StoreLocation, "LocationId", "Phone", custOrder.LocationId);
            return View(custOrder);
        }

        // GET: CustOrders/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var custOrder = await _context.CustOrder
                .Include(c => c.Customer)
                .Include(c => c.Location)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (custOrder == null)
            {
                return NotFound();
            }

            return View(custOrder);
        }

        // POST: CustOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var custOrder = await _context.CustOrder.FindAsync(id);
            _context.CustOrder.Remove(custOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustOrderExists(long id)
        {
            return _context.CustOrder.Any(e => e.OrderId == id);
        }
    }
}
