using System;
using System.Collections.Generic;
using System.Text;

//an order mean individual orders by individual customers : a receipt and it's data

namespace business_logic
{
    public class Order : IOrder
    {
        //unique id
        private string orderID;
        public string OrderID
        {
            get
            {
                return this.orderID;
            }
            set
            {
                this.orderID = value;
            }
        }
        
        //who is buying
        private ICustomer cust;

        public ICustomer Cust
        {
            get
            {
                return this.cust;
            }
            private set
            {
                this.cust = value;
            }
        }
        
        //total cost of order
        private double totalCost;

        //whats being ordered and how much
        private List< Tuple< IProduct, int > > itemsOrdered;
        public  List< Tuple< IProduct, int > > ItemsOrdered
        {
            get
            {
                return this.itemsOrdered;

            }
            set
            {
                this.itemsOrdered = value;
            }
        }
        
        //total number of items on order
        private int qtyAllItems;

        //when was it ordered
        private string order_timeStamp;
        public string Order_TimeStamp
        {
            get
            {
                return this.order_timeStamp;
            }
        }

        //lock the order in before check-out
        private bool orderIsLocked;
        private bool orderFulfulled;

        // blank constructor
        public Order()
        {
            this.orderID = genHexID();//always the first one.
            this.cust = new Customer("n/a", "n/a", "n/a", "nope");//dummy entry
            this.totalCost = 0.0;
            this.order_timeStamp = "14920101"; //yyyymmdd, apparently columbus ordered this via his cell phone on the way from Spain.
            this.orderFulfulled = true;
            this.orderIsLocked = true;
            this.itemsOrdered = new List< Tuple <IProduct , int > >();
            this.qtyAllItems = 0;
        }

        //a real order
        public Order(ICustomer cust) // blank constructor
        {
            this.orderID = genHexID(); 
            this.totalCost = 0.0;
            this.order_timeStamp = DateTime.Now.ToString(); 
            this.orderFulfulled = false;
            this.orderIsLocked = false;
            this.itemsOrdered = new List<Tuple<IProduct, int>>();
            this.qtyAllItems = 0;
            this.Cust = cust;
        }

        public void UpdateTotal( Tuple<IProduct, int> goods)
        {
            //Item sale price * number of items being ordered.
            this.totalCost += goods.Item1.SalePrice * goods.Item2;
        }

        public double GetTotal()
        {
            return this.totalCost;
        }

        public void GetTodaysDate()
        {
            //time is irrelevant... for now;
        }

        public void AddItemToOrder(  IProduct item , int qty )
        {
            //make the new tuple
            Tuple<IProduct, int> itemVol = new Tuple<IProduct, int>(item, qty);
            
            //add the ordered pair to the order.
            itemsOrdered.Add( itemVol );
            
            //update totals
            this.qtyAllItems += qty;
            UpdateTotal( itemVol );
        }

        public void RemoveItemFromOrder()
        {
            //you touched it, you baught it!;
        }

        public int ReturnTotalItems()
        {
            return this.itemsOrdered.Count;
        }

        //tool for generating a random id
        private string genHexID()
        {
            //random number generator
            int mills = Math.Abs((int)((DateTimeOffset.Now.ToUnixTimeMilliseconds() % 256) * Math.PI));
            Random rand = new Random(mills);
            //holds the random number.
            int random;

            //what to return
            string stringID = "";

            //set up the hex converter
            IDictionary<int, string> dict = new Dictionary<int, string>();
            dict.Add(10, "A");
            dict.Add(11, "B");
            dict.Add(12, "C");
            dict.Add(13, "D");
            dict.Add(14, "E");
            dict.Add(15, "F");

            //build the string
            for (int i = 0; i <= 15; i++)
            {
                random = rand.Next(0, 15);
                if (random <= 9)
                {
                    stringID += random.ToString();
                }
                else
                {
                    stringID += dict[random];
                }

            }

            //return the hex-id
            return stringID;

        }
    }
}
