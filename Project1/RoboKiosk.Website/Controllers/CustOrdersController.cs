using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using data_access.Entities;
using business_logic;
using RoboKiosk.Website.Models;

namespace RoboKiosk.Website.Controllers
{
    public class CustOrdersController : Controller
    {


        //change from caproj0Context _context to IRepository _repository;
        private readonly caproj0Context _context;
        private readonly IRepository _repository;


        public CustOrdersController(IRepository repository)
        {
            _repository = repository;

            
        }


        //private readonly caproj0Context _context;

        //public CustOrdersController(caproj0Context context)
        //{
          //  _context = context;
        //}

        // GET: CustOrders
        public async Task<IActionResult> Index()
        {
            IEnumerable<business_logic.Order> orders = await _repository.GetAllCustOrdersAsync();

            //non di method
            //var caproj0Context = _context.CustOrder.Include(c => c.Customer).Include(c => c.Location);

            var viewModels = orders.Select(o => new Models.CustOrdersViewModel
            {

                OrderID = o.OrderID,
                CustID = o.Cust.PhoneNum,
                LocationId = o.OrderLoc,
                OrderDate_Timestamp = o.Order_TimeStamp

            });


            //non di
            //return View(await caproj0Context.ToListAsync());

            //with di
            return View(viewModels);

        }
        /*
        // GET: CustOrders/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var custOrder = await data_access.Entities.CustOrder
                .Include(c => c.Customer)
                .Include(c => c.Location)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (custOrder == null)
            {
                return NotFound();
            }

            return View(custOrder);
        }


        */        
        // GET: CustOrders/Create
        public IActionResult Create()
        {
            //used by non di
            //returns the select list items to display

            var viewModel = new CustOrdersViewModel();

            //Set up a list of expected inputs
            
            viewModel.ProdTrippleList = _repository.GetID_short_longName();
            
            //declare the list
            viewModel.TupProdQtyObjList = new List<TempSet>();

            //add a list to store the form-field's name and get the quanity.
            for(int i = 0; i < viewModel.ProdTrippleList.Count; i++)
            {
                //sets an ordered pair, product-id and quantity.
                TempSet thisSet = new TempSet(viewModel.ProdTrippleList[i].Item2, viewModel.ProdTrippleList[i].Item1, 0, viewModel.ProdTrippleList[i].Item3);

                //dump the labels into temp-data and assign each 0 (for a quantity)
                TempData[viewModel.ProdTrippleList[i].Item2] = 0;

                //adds it to the view-model.
                viewModel.TupProdQtyObjList.Add(thisSet);
            }

            

            //place the reference
            //TempData["TupProdQtyObjList"] = viewModel.TupProdQtyObjList;

            //viewModel.ProdCodec = _repository.ProductDictionary();

            return View(viewModel);

        }

        
        // POST: CustOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustOrdersViewModel viewModel)
        {
            //recreate the reference.
            viewModel.ProdTrippleList = _repository.GetID_short_longName();
            //var whatIsThis = ViewBag["test"];

            //get the data
            //viewModel.TupProdQtyObjList = (List<TempSet>)TempData["TupProdQtyObjList"];

            //non-destructive method
            //viewModel.TupProdQtyObjList = (List<TempSet>)TempData.Peek("TupProdQtyObjList");



            /*
            if (ModelState.IsValid)
            {
                _context.Add(custOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            */


            ///////////build the list of line items///////////////////////
            List <Tuple<int, int>> lineitemList = new List<Tuple<int, int>>();

            for (int i = 0; i < viewModel.ProdTrippleList.Count; i++)
            {
                string shortN = viewModel.ProdTrippleList[i].Item2;
                
                int pid = viewModel.ProdTrippleList[i].Item1;
                
                int qty = int.Parse(TempData[shortN].ToString());
                
                Tuple<int, int> LineItem = Tuple.Create(pid, qty);
                
                lineitemList.Add(LineItem);
            }
            ///////////////////////////////////////////////////////////////



            //////////////////// just to see if the back-end is even working//////////////////
            //dummy objects : they do work

            List<Tuple<int, int>> dummyList = new List<Tuple<int, int>>();

            //dummy tuple
            //note, products on the server really start at 2, as 1 = none.
            Tuple<int, int> dummyLineItem = Tuple.Create(2, 1);

            //self explanitory
            dummyList.Add(dummyLineItem);

            //don't do anything till other code works!!!!!!!!!!!!
            await _repository.AddCustOrderAsync(dummyList, "5551234567", 1);

            ////////////////////////////////////////////////////////////////////////////////




            //ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerPw", custOrder.CustomerId);
            //ViewData["LocationId"] = new SelectList(_context.StoreLocation, "LocationId", "Phone", custOrder.LocationId);

            return View(viewModel);
        }

        /*
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
        */
        private bool CustOrderExists(long id)
        {
            return _context.CustOrder.Any(e => e.OrderId == id);
        }

    
    }
}
