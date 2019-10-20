using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using business_logic;
using System.Linq;

namespace data_access
{
    class Repository
    {
        //Verify This////////////////////
        //
        //Decare the local reference to the database, our "context,"
        //for dependancy injection.
        private readonly Entities.caproj0Context _context;

        //do the injection.  "Repository" referes to the database.
        public Repository(Entities.caproj0Context context)
        {
            _context = context;
        }

        //get from the table

        business_logic.Order ord= new Order();

        /// <summary>
        /// Get all customers asynch
        /// threaded version of the get-all-customers
        /// may need to be threaded due to multiple-requests by multiple clients.
        /// 
        /// called in Controllers.CustomerController to get the list of customers when using the
        /// "constructor injection pattern"
        /// </summary>
        /// <returns></returns>

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            //gets the Customer entries for the Customer table, and writes them to a list, asynchronously.
            List<Entities.Customer> entities = await _context.Customer.ToListAsync();


            //re-build the customer object from database-data.
            return entities.Select(e => new business_logic.Customer//(e.Fname, e.Lname, e.Phone, e.CustomerId.ToString() ) );
            {
                FName = e.Fname,
                LName = e.Lname,
                PhoneNum = e.Phone,

                CustID = e.CustomerId.ToString(),


            }) ;
            
        }


    }
}
