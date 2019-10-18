using System;
using System.Collections.Generic;
using System.Text;

namespace business_logic
{
    public interface ILocation
    {
        public int LocID { get; set; }
        public string Phone { get; set; }
        public void AddProduct(IProduct addMe);
        public void PrintInfo();
        public void RemProduct(IProduct removeMe);
        public void AdjustInv(string prod, int qty);

        public void AddClient(ICustomer addMe);
        public List<ICustomer> CustList { get; }

        public bool comparePW(string input);

        public ICustomer GetMgr();

        public string LocMenuStr();
        public string InvToStr();

        public string RecieptsToStr();

        public string ClientsToStr();

        public List<IProduct> Inventory { get; set; }
        public Dictionary<int, IProduct> ProdIndex { get; set; }

        public List<IProduct> GetInventory();
        public List<IOrder> Reciepts { get; set; }

        public IOrder CreateOrder(ICustomer customer);

        public string BuildMenuChoices();

    }
}
