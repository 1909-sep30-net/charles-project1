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
        


        // GET: Customers for Index display
        public async Task<IActionResult> Index()
        {
            IEnumerable<business_logic.Customer> customer = await _repository.GetAllCustomersAsync();


            //customer view model object acquires the pointer to the locally mirrored customer-objects.
            var viewModels = customer.Select(p => new Models.CustomerViewModel
            {
                
                CustNum = p.CustNum,
                FName = p.FName,
                LName = p.LName,
                PhoneNum = p.PhoneNum,
                //password, may want to delete this or build another model for a Location view.
                CustId = p.CustID,
            }) ;


            return View(viewModels);
        }

        //Pages with Forms come in a pair of actions: GET one to get the form to begin with,
        //                                            POST one to post the filled out form.
        
        // GET: Customer/Create : Gets the form to create a new customer.
        public ActionResult Create()
        {
            //var types = await _repository.GetAllTypesAsync();

            var viewModel = new CustomerViewModel();
            /*{
                Types = types.Select(t => t.Name).ToList()
            };
            */
            return View(viewModel);
        }

        /// <summary>
        /// Controller to Create the customer
        /// Viewmodel is processed from form data, matched up, then data is written to server.
        /// </summary>
        /// <returns></returns>

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerViewModel viewModel)
        {
            
            try
            {
                //instructor notes/////////////////
                // ModelState works with model binding to give us automatic 
                // server-side validation of any of those attributes on the model class.
                // (e.g. Required, Range, RegularExpression)


                //design notes/////////
                //this was specific to Mr. Nick's database, where the 
                //type of pokemon-creature (elemental type) was a separate table
                //may want to set up another type of validation IE phone-already-exists

                //input validation: checks for a class-of-something
                                /*if (!ModelState.IsValid)
                                {
                                    var types = await _repository.GetAllTypesAsync();
                                    viewModel.Types = types.Select(t => t.Name).ToList();

                                    // server-side validation failure, return a new form to the
                                    // user, but for convenience, fill in his previous (wrong) data

                                    //returns the fields pre-filled, allowing the user to correct invalid data.
                                    return View(viewModel);

                                    // also, if ModelState contains errors when we render that form,
                                    // the validation tag helpers will get filled in with error messages
                                }*/

                                

                    //Create a new bus_log.Customer representation and get the form-data.
                    var customer = new business_logic.Customer
                    {

                        FName = viewModel.FName,
                        LName = viewModel.LName,
                        PhoneNum = viewModel.PhoneNum,
                        CustID = viewModel.CustId //set the password
                    };

                    //get it's type and add it.
                    //add the validated data: Mr. Nick needed to make sure that the pokemon's types were valid
                    //before the new entry could be placed on the server.  We don't need the exact same thing here..
                    /*
                    foreach (var s in viewModel.Types)
                    {
                        pokemon.Types.Add(await _repository.GetTypeByNameAsync(s));
                    }
                    */


                    //Call the method to add the customer to the server.
                    await _repository.AddCustomerAsync(customer);

                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    // when we run our own logic for server-side validation
                    // we can add our own errors to that modelstate just like the validationattributes do.
                    // why? because they will be put onto the form when we return it.
                    // first parameter: which field has the problem
                    //       ("" to put the error in the "summary" at the top)
                    ModelState.AddModelError("Customer:", ex.Message);

                    //var types = await _repository.GetAllTypesAsync();
                    //viewModel.Types = types.Select(t => t.Name).ToList();

                    return View(viewModel);
                }

            
                //return View();
            }




        /*
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

        /*
         // old version (stub)
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
        //return View();
        //}
        //////////end old version

        // GET: Customers/Edit/5

        //edit out for now?

            /*
        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            
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
            

            //return View(customer);
            return View();
        }
        */

        /*
        public async Task<IActionResult> Edit(int? id)
        {
            
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
            

            //temporary till I can figure this out.
            return View();
        }
        */

        /*
        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, /*[Bind("CustomerId,Phone,Fname,Lname,CustomerPw")] Customer customer
        IFormCollection collection)
        {
            
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
            
            
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            
            return View();

        }
        */
        
        /*
        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            
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
        */

        /*
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
            
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }
            
        */
        /*
        private bool CustomerExists(int id)
        {
            //temp fix
            return true;
            //return _context.Customer.Any(e => e.CustomerId == id);
        }
        */
    }
}
