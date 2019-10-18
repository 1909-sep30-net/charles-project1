using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace business_logic
{
    public class Customer: ICustomer
    {
        //fields
        private string custID;

        public string CustID
        {
            get
            {
                return this.custID;
            }
            set
            {
                this.custID = value;
            }
        }
        private string fName;
        public string FName
        {
            get
            {
                return this.fName;
            }
            set
            {
                this.fName = value;
            }
        }
        private string lName;

        public string LName
        {
            get
            {
                return this.lName;
            }
            set
            {
                this.lName = value;
            }
        }
        

        private string phoneNum;
        public string PhoneNum
        {
            get
            {
                return this.phoneNum;
            }
            set
            {
                this.phoneNum = value;
            }
        }

        private List<IOrder> custOrders;
        public List<IOrder> CustOrders
        {
            get
            {
                return this.custOrders;
            }
            set
            {
                this.custOrders = value;
            }
        }
        //simple constructor
        public Customer(string first, string last, string phone, string id)
        {
            //is the password
            this.custID = id;

            //get basic info
            this.fName = first;
            this.lName = last;
            this.phoneNum = phone;

            custOrders = new List<IOrder>();

        }


        public string MakeString()
        {
            //Fix this
            //quick and dirty, fix
            return this.ToString();
        }

        public void SetFavorite()
        {
            //Fix this.
            throw new NotImplementedException();
        }

        //returns a string that describes the recent orders
        public string RecieptsToStr()
        {
            string retThis = "";

            retThis += ("Recent Orders\n");

            //build the output
            //iterate through each order on the list of reciepts
            for (int i = 0; i < this.CustOrders.Count; i++)
            {

                retThis += ($"Order No. { this.CustOrders[i].OrderID } Time: {this.CustOrders[i].Order_TimeStamp}\n");
                //holds the line item
                Tuple<IProduct, int> lineItem;

                //iterate through each item on each order on the reciepts
                for (int q = 0; q < this.CustOrders[i].ItemsOrdered.Count; q++)
                {
                    lineItem = this.CustOrders[i].ItemsOrdered[q];
                    retThis += ($"\tItem: { lineItem.Item1.ProductDesc } Quantity: { lineItem.Item2 } \n");
                }

                
            }

            return retThis;
        }


    }
}
