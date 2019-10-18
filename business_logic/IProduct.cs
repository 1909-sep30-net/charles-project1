using System;
using System.Collections.Generic;
using System.Text;

namespace business_logic
{
    public interface IProduct
    {
        //have to put this here as the properties block needs to get through the interface.
        public string ProductDesc { get; }

        public int ProdID { get; set; }
        public string SalesBlurb { get; set; }

        public double SalePrice { get; }
        public int QuantityOnHand { get; }

        public double Profitability();

        public void AdjustQty(int adjustment);

    }
    
    
    
}
