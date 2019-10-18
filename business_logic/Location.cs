
using System;
using System.Collections.Generic;
using System.Text;
//business location class
// inventory simply uses a list of tuples with a product number and the quantity on hand.

namespace business_logic
{
    public class Location : ILocation
    {
        //unique id
        private int locID;

        public int LocID

        {
            get
            {
                return this.locID;
            }
            set
            {
                this.locID = value;
            }
        }

        private string phone;

        public string Phone
        {
            get
            {
                return this.phone;
            }
            set
            {
                this.phone = value;
            }
        }

        private string locName;
        private int region;

        //product id, quantity
        private List<IProduct> inventory;

        private Dictionary<int, IProduct> prodIndex;
        public Dictionary<int, IProduct> ProdIndex
        {
            get
            {
                return this.prodIndex;
            }
            set
            {
                this.prodIndex = value;
            }
        }

        public List<IProduct> Inventory
        {
            get
            {
                return this.inventory;
            }
            set
            {
                this.inventory = value;
            }
        }

        private List<ICustomer> custList;

        public List<ICustomer> CustList
        {
            get
            {
                return this.custList;
            }
            private set
            {
                //can't set this, initialized in constructor.;
            }
            
        }


        //list of reciepts
        private List<IOrder> receipts;
        public List<IOrder> Reciepts
        {
            get
            {
                return this.receipts;
            }
            set
            {
                this.receipts = value;
            }
        }

        //who runs the place?

        private ICustomer mgr;
        private long mgrID;
        private string mgrpwd;

        //is it on fire?
        public bool isOnFire = false;

        //constructor
        public Location(string name, int region, long ID, string log, int locID, string phone)
        {
            //basics
            this.locName = name;
            this.region = region;
            this.mgrID = ID;
            this.mgrpwd = log;
            this.phone = phone;
            
            //The all important location ID.
            this.locID = locID;

            //set up the inventory list
            this.inventory = new List< IProduct >();

            //list of clientelle list
            this.custList = new List< ICustomer >();

            //set up the list of reciepts
            this.receipts = new List< IOrder > () ;

            //add the first product (which is nothing) for first product.
            // and empty store has nothing.
            IProduct blank = new Product("blank", "n/a", 0.0);
            this.inventory.Add(blank);

            //manager is the first client
            this.mgr = new Customer(mgrID.ToString(), mgrID.ToString(), "n/a", "thisIsAnID");
            this.custList.Add(mgr);//not a dummy to hold the first spot.

            //////Add a new order, but make it blank
            //
            
            //holds the first spot
            IOrder blankOrder = new Order();
            //add an order
            
            //add a new blank order
            this.receipts.Add( blankOrder );
            
            //add the dummy item to the dummy order 
            this.receipts[0].AddItemToOrder(new Product("nothing", "nada", 0.0) , 0) ;

            //
            /////////////////////////////////////

            this.prodIndex = new Dictionary<int, IProduct>();
        }

        //REMOVE or change...unsecure
        public void PrintInfo()
        {
            Console.WriteLine( $"loc {this.locID} region {this.region} mgrpwd {this.mgrpwd}" );
        }

        //add a product to inventory
        public void AddProduct( IProduct addMe )
        {
            this.inventory.Add( addMe );

            //update index.
            int ind = this.inventory.Count - 1;

            IProduct someThing = addMe;


            this.ProdIndex.Add( ind, someThing);
        }



        //remove an item from inventory
        public void RemProduct( IProduct remMe)
        {
            this.inventory.Remove( remMe );
        }

        //getter, manually written

        public List<IProduct> GetInventory()
        {
            return this.inventory;
        }

        //adjust the quantity of an item in inventory.
        public void AdjustInv(string prod, int qty)
        {
            IProduct element = inventory.Find(e => e.ProductDesc == prod );

            element.AdjustQty(qty);

        }

        //add  a customer to the client list
        public void AddClient( ICustomer addMe)
        {
            this.custList.Add(addMe);
        }

        public void RemClient(ICustomer remMe)
        {
            this.custList.Remove( remMe );
        }

        public bool comparePW(string input)
        {
            if(input == this.mgrpwd)
            {
                return true;
            }
            return false;
        }

        public ICustomer GetMgr()
        {
            return this.mgr;
        }

        ////////////////////////////////////////////////////////////
        //   Manage the Location
        //   TODO:
        //       Add a Set Location
        //       Add or Remove Inventory
        //       Adjust Inventory Directly
        //      
   
        //returns a LocationMenu
        public string LocMenuStr()
        {
            return ("\nWelcome Manager"
            + "\nPlease Choose One Of The Following"
            + "\n\n1. Store Inventory" //need submenu
            + "\n2. Order History"     //need submenu
            + "\n3. Client List"       //
            + "\n4. Sales Reporting"   
            + "\n5. Exit Management Menu"
             );


        }
        
        //return a string that describes the inventory on hand.
        public string InvToStr()
        {
            string retThis = "";

            retThis += "Inventory On Hand\n";

            //build the output
            for (int i = 0; i < this.inventory.Count; i++)
            {
                retThis+= ($"Item: {this.inventory[i].ProductDesc } Quantity: { this.inventory[i].QuantityOnHand } \n");
            }

            return retThis;
        }

        //returns a string that describes the recent orders
        public string RecieptsToStr()
        {
            string retThis = "";
            
            retThis += ("Recent Orders\n");
            
            //build the output
            //iterate through each order on the list of reciepts
            for (int i = 0; i < this.receipts.Count; i++)
            {

                retThis += ($"Order { this.Reciepts[i].OrderID }: " 
                          + $"Customer: { this.Reciepts[i].Cust.PhoneNum }\n");
                //holds the line item
                Tuple<IProduct, int> lineItem;

                //iterate through each item on each order on the reciepts
                for (int q = 0; q < this.Reciepts[i].ItemsOrdered.Count; q++)
                {
                    lineItem = this.Reciepts[i].ItemsOrdered[q];
                    retThis += ($"\tItem: { lineItem.Item1.ProductDesc } Quantity: { lineItem.Item2 } \n");
                }

                //retThis += ($"Order {i}: Customer: { this.receipts[i].Cust.PhoneNum } Qty Items: {this.receipts[i].ReturnTotalItems() } Sale: { this.receipts[i].GetTotal() } \n");
            }

            return retThis;
        }

        //customer list
        public string ClientsToStr()
        {
            string retThis = "";

            retThis += ($"Clients: \nQty: { this.custList.Count } \n");

            //build the output
            for (int i = 0; i < this.custList.Count; i++)
            {

                retThis += ($"ID: Customer: { i } : Customer: { this.custList[i].LName }, { this.custList[i].FName }  Phone: { this.custList[i].PhoneNum }\n");
            }

            return retThis;
        }

        public string BuildMenuChoices()
        {
            string retThis = "";

            retThis += "Please enter a number " +
                "to chose one of the following\n\n";

            //build the output
            for (int i = 1; i < this.inventory.Count; i++)
            {
                
                retThis += ($"{i} Item: {this.Inventory[i].SalesBlurb } \n");
                
            }


            return retThis;
        }

        //create an order and return it.
        public IOrder CreateOrder(ICustomer customer)
        {
            IOrder order = new Order(customer);

            //add it to the lists of those who track it
            customer.CustOrders.Add(order);
            this.Reciepts.Add(order);
            //customer and store now tracking said order.

            return order;
        }
    }
}
