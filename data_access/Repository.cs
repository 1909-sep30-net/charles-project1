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


            });
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
        /// Returns minimal data to a Controller for a 
        /// </summary>
        /// <returns></returns>
        public List<Tuple<int, string, string>> GetID_short_longName()
        {
            var prod = _context.Product.ToList();

            List<Tuple<int, string, string> > productlist = new List<Tuple<int, string, string>>();

            for (int i = 0; i < prod.Count; i++)
            {
                int thisProdID = prod[i].ProductId;
                string thisProdS = prod[i].Pname;
                string thisProdL = prod[i].SalesName;

                Tuple<int, string, string> theTuple = new Tuple<int, string, string>(thisProdID, thisProdS, thisProdL);

                productlist.Add(theTuple);
            }

            return productlist;

        }

        public List<string> ProductList()
        {
            var prod = _context.Product.ToList();

            List<string> productlist = new List<string>();

            for (int i = 0; i < prod.Count; i++)
            {
                string thisProd = prod[i].SalesName;
                productlist.Add(thisProd);
            }

            return productlist;
        }

        public List<int> GetProdIDList()
        {
            var prod = _context.Product.ToList();

            List<int> prodIDs = new List<int>();

            for(int i = 0; i < prod.Count; i++)
            {
                int thisProdID = prod[i].ProductId;
                prodIDs.Add(thisProdID);
            }

            return prodIDs;
        }

        public Dictionary<int,string> ProductDictionary()
        {
            Dictionary<int, string> prods = new Dictionary<int, string>();

            var prod = _context.Product.ToList();

            for (int i = 0; i < prod.Count; i++)
            {
                string thisProd = prod[i].Pname;
                int thisProdID = prod[i].ProductId;

                prods.Add(thisProdID, thisProd);
            }

            return prods;
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
                //Console.WriteLine("Customer not found, please try again or make a new login.");
                return new Customer("null", "null", "null", "9999");
               
            }
            
            //                                                            (string first, string last, string phone, string id)
            business_logic.Customer thisCust = new business_logic.Customer(customer.Fname, customer.Lname, customer.Phone, customer.CustomerPw);
            thisCust.CustNum = customer.CustomerId;
            return thisCust;

        }

        //////////////////////////////////////////////////////////////
        //customer orders
        //

        /// <summary>
        /// Get all customer orders from SQL Server
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Order>> GetAllCustOrdersAsync()
        {
            List<Entities.CustOrder> orders = await _context.CustOrder.ToListAsync();

            //create a list
            List <Order> orderlist = new List<Order>();

            //add to the list
            for (int i = 1; i < orders.Count; i++)
            {
                //get the right person.
                int dbcustID = orders[i].CustomerId;
                var dbcust = await _context.Customer.FirstOrDefaultAsync(cust => cust.CustomerId == dbcustID);

                //get the right location
                int dblocID = orders[i].LocationId;
                var dbloc = await _context.StoreLocation.FirstOrDefaultAsync(loc =>  loc.LocationId == dblocID);



                //assign them to and create the order
                ICustomer thisCust = new Customer(dbcust.Fname, dbcust.Lname, dbcust.Phone, dbcust.CustomerPw);

                Order indOrder = new Order(orders[i].OrderId.ToString(), thisCust, orders[i].OrderDate.ToString() , dbloc.Phone);

                orderlist.Add(indOrder);
            }

            //return the list of orders
            return orderlist;

        }

        /// <summary>
        /// Create and build a customer's order 
        /// ToDo: Customer will need to build their list of items ordered: proj0 used to use a loop.
        ///                                                                proj1 needs to create a list from form-line-items entered-
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cust"></param>
        /// <param name="store"></param>
        /// <returns></returns>
        public async Task AddCustOrderAsync(List<Tuple<int, int>> lineItems, string custPh, int storeId)
        {
            

            //create a store object and get it's inventory from the database.
            ILocation store = InitStoreLocation(storeId);

            //create a customer object
            ICustomer cust = CreateCustObj(custPh);

            //establish the order based on Customer object.
            IOrder theOrder = new Order(cust);

            //for timestamps
            DateTime now = DateTime.Now;

            //create an order on the DB
            await addOrderToDB(_context, cust.PhoneNum, store.Phone, now);

            //get the order's number set by the database
            var dbOrdNo = GetCustoOrdNoFromDB(_context, cust.PhoneNum, store.Phone, now);

                      
            
            //need to create the line items, or reference them.//////////////////-------------------------------------
            // based on a list of tuples.
            // Order.AddItemToOrder(IProduct, quantity) will work.in a loop from a list of tuples.
            // So the order must be built directly from a list passed in.
            //      to simplify, pass in a tuple directly.

            // additionally, will need to get a store-reference passed in by a phone number?
            // can get the customer by their phone number?

            //build the list of items on the order
            for(int i = 0; i < lineItems.Count; i++)
            {
                //get the product in question from the database?
                var product = _context.Product.FirstOrDefault(p => p.ProductId == lineItems[i].Item1);
                business_logic.IProduct lineitm = new Product(product.Pname, product.SalesName, (double)product.SalesPrice);

                

                theOrder.AddItemToOrder(lineitm, lineItems[i].Item2);
            }



            //add the line items previously created directly from theOrder object.
            for (int i = 0; i < theOrder.ItemsOrdered.Count; i++)
            {

                //get the product in question
                var product = _context.Product.FirstOrDefault(p => p.Pname == theOrder.ItemsOrdered[i].Item1.ProductDesc);

                //create the line-item in the database.
                addLineItemToDB(_context, dbOrdNo, product.ProductId, theOrder.ItemsOrdered[i].Item2);

                //adjust the store's inventory
                //store can go into negative (will have to buy more stock to make up for the negative)... yes.
                // never say no!
                store.AdjustInv(theOrder.ItemsOrdered[i].Item1.ProductDesc, (-1*theOrder.ItemsOrdered[i].Item2));
            }

            

            //update the inventory for a given store on the database.
            UpdateLocInvOnDB(_context, store);


            //throw new NotImplementedException();
        }


        /// <summary>
        /// add an order to the database
        /// </summary>
        /// <param name="context"></param>
        /// <param name="custPhone"></param>
        /// <param name="locPhone"></param>
        /// <param name="now"></param>
        /// <returns></returns>

        //add an order to the Database.
        public async Task addOrderToDB(Entities.caproj0Context context, string custPhone, string locPhone, DateTime now)
        {
            //datetime format
            //2019-10-14 00:00:00.000
            //string now = SQLTimeStamp();

            //get the customer id
            var customer = await context.Customer.FirstOrDefaultAsync(cust => cust.Phone == custPhone);

            //get the store id.
            var location = await context.StoreLocation.FirstOrDefaultAsync(loc => loc.Phone == locPhone);

            //check if input is valid.
            if (customer == null)
            {
                //Console.WriteLine("Unable to create order, The customer's record does not exist.");
                return;
            }
            else if (location == null)
            {
                //Console.WriteLine("Unable to create order: The location's record does not exist.");
                return;
            }

            //passes all checks, so proceed

            var order = new data_access.Entities.CustOrder //create object
            {
                CustomerId = customer.CustomerId,
                LocationId = location.LocationId,
                OrderDate = now //don't need to build this, already done.
            };

            //add to the table
            await context.CustOrder.AddAsync(order);

            //save changes.
            await context.SaveChangesAsync();
        }



        /// <summary>
        /// add a line item to the database
        /// </summary>
        /// <param name="context"></param>
        /// <param name="order"></param>
        /// <param name="prodN"></param>
        /// <param name="qty"></param>


        static void addLineItemToDB(Entities.caproj0Context context, long order, int prodN, int qty)
        {
            //check if they exist
            var theOrder = context.CustOrder.FirstOrDefault(ord => ord.OrderId == order);

            var theProduct = context.Product.FirstOrDefault(prod => prod.ProductId == prodN);

            //check if order and products exist
            if (theOrder == null)
            {
                //Console.WriteLine("Invalid order, canceling");
                return;
            }
            else if (theProduct == null)
            {
                //Console.WriteLine("Invalid item, canceling");
                return;
            }

            var lineItem = new data_access.Entities.LineItem //create object
            {
                OrderId = order,
                ProductId = prodN,
                Quantity = qty
            };

            context.LineItem.Add(lineItem);

            context.SaveChanges();

        }

        /// <summary>
        /// Mirror's stores inventory to database, then updates it.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="store"></param>
        static void UpdateLocInvOnDB(Entities.caproj0Context context, ILocation store)
        {
            //get the location
            var loc = context.StoreLocation.FirstOrDefault(l => l.Phone == store.Phone);

            //get the store's inventory representation from the server
            var invQuery = from inv in context.Inventory
                           where (inv.LocationId == store.LocID)
                           select inv;

            //do we need to ?  Why not just write directly? Synchronicity.

            //put it into a list for simplicity's sake
            var invList = invQuery.ToList();

            int count = 0;
            //iterate through the query results.
            foreach (var p in invList)
            {
                count++;
                //Console.WriteLine($"Remote Name: {p.Product.Pname} Qty{p.Quantity} Store: {p.LocationId}:" );
                //Console.WriteLine($"Local Name: {store.Inventory[count].ProductDesc}" );

                p.Quantity = store.Inventory[count].QuantityOnHand;

                //Console.WriteLine("Check Update:");
                //Console.WriteLine($"Remote Name: {p.Product.Pname} Qty{p.Quantity} Store: {p.LocationId}:");


            }

            context.SaveChanges();

            //Console.WriteLine("Update complete, press Enter to Continue");
            //Console.ReadLine();
            /*            
                        //for each item in the inventory on the server and on the client
                        for(int i = 0; i < invList.Count; i++)
                        {

                            //from the store's inventory entries on the server
                            //get the row we want                                      based on local product id equals server product-id
                            var invRow = invQuery.FirstOrDefault(p => p.Product.ProductId == store.Inventory[i].ProdID);

                            //temp
            //                string a = invRow.ProductID.ToString();

              //              Console.WriteLine($"Updated Remote Inventory -- inRow Product ID : {invRow.ProductId} <-> {invRow.Product.Pname} : ||  local equivalant: { store.Inventory[i].ProdID } <-> {store.Inventory[i].ProductDesc}");

                            //update that entry on the server
                            invRow.Quantity = store.Inventory[i].QuantityOnHand;

                            context.SaveChanges();

                        }
                        */



        }


        long GetCustoOrdNoFromDB(Entities.caproj0Context context, string ph, string locph, DateTime time)
        {
            var custOrdsAsync = context.CustOrder.Select(o => o.Customer.Phone == ph);

            var custOrds = from order in context.CustOrder
                           where (order.Customer.Phone == ph) && (order.Location.Phone == locph) && (order.OrderDate == time)
                           select order;

            var orderList = custOrds.ToList();

            return (long)orderList[0].OrderId;
        }


        ///////////////////setup local store instances
        public ILocation InitStoreLocation(int storeNum)
        {
            ILocation store;

            //check if they exist
            var loc = _context.StoreLocation.FirstOrDefault(l => l.LocationId == storeNum);
            int locID = loc.LocationId;
            var mgr = _context.Manager.FirstOrDefault(m => m.ManagerId == loc.Manager);

            if (loc == null)
            {
                //Console.WriteLine("Invalid Store");
                return null;
            }
            else if (mgr == null)
            {
                //Console.WriteLine("Nobody manages this location, unable to proceed");
            }

            //initialize the business location object
            // all have region 1 for now.
            store = new Location(loc.StoreName, 1, mgr.ManagerId, mgr.ManagerPw, loc.LocationId, loc.Phone);


            InitStoreInventory(store);

            return store;
        }

        public ICustomer CreateCustObj(string custPh)
        {
            ICustomer cust;

            //check if they exist
            var customer = _context.Customer.FirstOrDefault(c => c.Phone == custPh);
            int custID = customer.CustomerId;
            

            if (customer == null)
            {
                //Console.WriteLine("Invalid Customer");
                return null;
            }

            //initialize the business customer object
            // all have region 1 for now.
            cust = new Customer(customer.Fname, customer.Lname, customer.Phone, customer.CustomerPw);

            return cust;
        }


        //initialize store inventory from database-information.
        public void InitStoreInventory(ILocation store)
        {
            var loc = _context.StoreLocation.FirstOrDefault(l => l.LocationId == store.LocID);

            //linq query to get the orders by id for location
            var invLine = from inv in _context.Inventory
                          where (inv.LocationId == store.LocID)
                          select inv;

            //convert to list
            var stInv = invLine.ToList();

            //make the inventory
            //store.AddProduct(new business_logic.Product("circuits", "Sal's fine Circuits", 12.5, 300));

            for (int i = 0; i < stInv.Count; i++)
            {
                var invItem = _context.Product.FirstOrDefault(q => q.ProductId == stInv[i].ProductId);

                //Console.WriteLine($"Adding {stInv[i].Quantity}x{invItem.Pname}");

                store.AddProduct(new business_logic.Product(invItem.Pname, invItem.SalesName, (double)invItem.Cost, stInv[i].Quantity, stInv[i].ProductId));
            }
        }

    }

}
