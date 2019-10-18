using System;
using System.Collections.Generic;
using System.Text;

namespace business_logic
{
    public interface IOrder
    {

        public List<Tuple<IProduct, int>> ItemsOrdered { get; set; }

        public string OrderID { get; set; }
        public string Order_TimeStamp { get; }
        public void UpdateTotal(Tuple<IProduct, int> goods);
        public ICustomer Cust { get; }
        public double GetTotal();
        public void GetTodaysDate();
        public void AddItemToOrder(IProduct item , int qty);
        public void RemoveItemFromOrder();
        public int ReturnTotalItems();

    }
}
