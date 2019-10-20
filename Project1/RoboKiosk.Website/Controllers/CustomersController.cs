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
using Microsoft.AspNetCore.Http;

namespace RoboKiosk.Website.Controllers
{
    public class CustomersController : Controller
    {
        //change from caproj0Context _context to IRepository _repository;
        //private readonly caproj0Context _context;
        private readonly IRepository _repository;


        public CustomersController(IRepository repository)
        {
            _repository = repository;
        }
        


        // GET: Customers
        public async Task<IActionResult> Index()
        {
            IEnumerable<business_logic.Customer> customer = await _repository.GetAllCustomersAsync();



            var viewModels = customer.Select(p => new Models.CustomerViewModel
            {
                CustId = Int32.Parse(p.CustID),
                FName = p.FName,
                LName = p.LName,
                PhoneNum = p.PhoneNum
            });

            return View(viewModels);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            /*
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            */

            //return View(customer);
            return View();
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Adds a new customer via asynch, based off PokeMon DB by Mr. Nick
        /// ToDo: probably add a password, 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.CustomerViewModel viewModel)
        {/*
            if (ModelState.IsValid)
            {
                var customers = await _repository.GetAllCustomersAsync();
                viewModel.Customers = customers.Select(fn => fn.FName).ToList();

                // server-side validation failure, return a new form to the
                // user, but for convenience, fill in his previous (wrong) data
                return View(viewModel);
                //                _repository.Add(customer);
                //                await _context.SaveChangesAsync();
                //                return RedirectToAction(nameof(Index));
            }

            var customer = new business_logic.Customer
            {
                CustID = viewModel.CustId.ToString(),
                FName = viewModel.FName,
                LName = viewModel.LName,
                PhoneNum = viewModel.PhoneNum
            };

            await _repository.AddCustomerAsync(customer);

            */
            //comment out till working
            //return View(customer);
            return View();
        }

        // GET: Customers/Edit/5
        
        //edit out for now?

        public async Task<IActionResult> Edit(int? id)
        {
            /*
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
            */

            //temporary till I can figure this out.
            return View();
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, /*[Bind("CustomerId,Phone,Fname,Lname,CustomerPw")] Customer customer*/ IFormCollection collection)
        {
            /*
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            return View(customer);
            */
            /*
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            */
            return View();

        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            /*
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
            */
            IActionResult dummy = await Delete(null);

            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add update logic here
                IActionResult x = await DeleteConfirmed(0);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            /*
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            */
        }

        private bool CustomerExists(int id)
        {
            //temp fix
            return true;
            //return _context.Customer.Any(e => e.CustomerId == id);
        }
    }
}
