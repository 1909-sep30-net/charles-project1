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
    public class LineItemsController : Controller
    {
        private readonly caproj0Context _context;

        public LineItemsController(caproj0Context context)
        {
            _context = context;
        }

        // GET: LineItems
        public async Task<IActionResult> Index()
        {
            var caproj0Context = _context.LineItem.Include(l => l.Order).Include(l => l.Product);
            return View(await caproj0Context.ToListAsync());
        }

        // GET: LineItems/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItem = await _context.LineItem
                .Include(l => l.Order)
                .Include(l => l.Product)
                .FirstOrDefaultAsync(m => m.LineItemId == id);
            if (lineItem == null)
            {
                return NotFound();
            }

            return View(lineItem);
        }

        // GET: LineItems/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.CustOrder, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "SalesName");
            return View();
        }

        // POST: LineItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LineItemId,OrderId,ProductId,Quantity")] LineItem lineItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lineItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.CustOrder, "OrderId", "OrderId", lineItem.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Pname", lineItem.ProductId);
            return View(lineItem);
        }

        // GET: LineItems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItem = await _context.LineItem.FindAsync(id);
            if (lineItem == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.CustOrder, "OrderId", "OrderId", lineItem.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Pname", lineItem.ProductId);
            return View(lineItem);
        }

        // POST: LineItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("LineItemId,OrderId,ProductId,Quantity")] LineItem lineItem)
        {
            if (id != lineItem.LineItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lineItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LineItemExists(lineItem.LineItemId))
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
            ViewData["OrderId"] = new SelectList(_context.CustOrder, "OrderId", "OrderId", lineItem.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Pname", lineItem.ProductId);
            return View(lineItem);
        }

        // GET: LineItems/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItem = await _context.LineItem
                .Include(l => l.Order)
                .Include(l => l.Product)
                .FirstOrDefaultAsync(m => m.LineItemId == id);
            if (lineItem == null)
            {
                return NotFound();
            }

            return View(lineItem);
        }

        // POST: LineItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var lineItem = await _context.LineItem.FindAsync(id);
            _context.LineItem.Remove(lineItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LineItemExists(long id)
        {
            return _context.LineItem.Any(e => e.LineItemId == id);
        }
    }
}
