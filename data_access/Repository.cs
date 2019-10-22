using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using business_logic;
using System.Linq;



namespace data_access
{
    /// <summary>
    /// Access from and writes to the respository
    /// IRepository acts as a juncture to negotiate with the business_logic classes.
    /// </summary>
    public class Repository : IRepository
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

        //business_logic.Order ord= new Order();

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
                
                //cust num is it's index on the table in the database
                CustNum = e.CustomerId,
                FName = e.Fname,
                LName = e.Lname,
                PhoneNum = e.Phone,

                //customer's password passed to CustID
                CustID = e.CustomerPw,
                

            }) ;
        }



        /// <summary>
        /// Add a new customer, asynch-method.  Customers must have a unique phone number and a password (basic security).
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        
        public async Task AddCustomerAsync(business_logic.Customer customer)
        {
            var entity = new data_access.Entities.Customer
            {
                Fname = customer.FName,
                Lname = customer.LName,
                Phone = customer.PhoneNum,
                

                //custID is the password-string
                CustomerPw = customer.CustID
            };

            //check to see if customer is already listed in the database
            //customers must have a unique phone number
            if (await _context.Customer.AnyAsync(c => c.Phone == entity.Phone))
            {
                throw new InvalidOperationException("Customer already exists, please use a different cell-phone number");
            }

            _context.Add(entity);
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// For Dependancy Injection
        /// Get all customer orders
        /// </summary>
        /// <returns></returns>
       public async Task<IEnumerable<Order>> GetAllCustOrdersAsync()
        {
            List<Entities.CustOrder> orders = await _context.CustOrder.ToListAsync();
            
            return orders.Select(o => new business_logic.Order
            {
                //cust num is it's index on the table in the database
                OrderID = o.OrderId.ToString(),
                Cust = GetCustRecord(_context, o.Customer.Phone),
                Order_TimeStamp = o.OrderDate.ToString(),
                OrderLoc = o.Location.Phone,

            });
        }

        /// <summary>
        /// Get a customer by phone number
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
         Customer GetCustRecord(Entities.caproj0Context _context, string phone)
        {

            var customer = _context.Customer.FirstOrDefault(cust => cust.Phone == phone);

            if (customer == null)
            {
                Console.WriteLine("Customer not found, please try again or make a new login.");
                return null;
            }
            
            //                                                            (string first, string last, string phone, string id)
            business_logic.Customer thisCust = new business_logic.Customer(customer.Fname, customer.Lname, customer.Phone, customer.CustomerPw);
            thisCust.CustNum = customer.CustomerId;
            return thisCust;

        }

        public async Task AddCustOrderAsync(business_logic.Order order)
        {
            throw new NotImplementedException();
        }
    }
}
