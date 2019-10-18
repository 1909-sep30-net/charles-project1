using System;
using System.Collections.Generic;
using System.Text;

namespace business_logic
{
    public class Product : IProduct
    {
        private int prodID;

        public int ProdID
        {
            get
            {
                return this.prodID;
            }
            set
            {
                this.prodID = value;
            }
        }
        private string productDesc;
        public string ProductDesc
        {
            get
            {
                return this.productDesc;
            }
            private set
            {
                this.productDesc = value;
            }
        }

        private string salesBlurb;
        public string SalesBlurb
        {
            get
            {
                return this.salesBlurb;
            }
            set
            {
                this.salesBlurb = value;
            }
        }

        private double cost;
        public double SalePrice { get; }
        private double saleValue;
        private int quantityOnHand;
        public int QuantityOnHand 
        { 
                get
                {
                    return this.quantityOnHand;
                }

                private set
                {
                    quantityOnHand = value;
                }
         }

        public Product(string desc, string sellwords, double cost)
        {
            this.prodID = 0;


            this.productDesc = desc;
            this.salesBlurb = sellwords;
            this.cost = cost;
            this.SalePrice = cost * 2.0;
            this.saleValue = SalePrice - cost;
            this.quantityOnHand = 0;
        }

        //initialize with a quantity.
        public Product(string desc, string sellwords, double cost, int qty, int id)
        {
            this.prodID = id;
            this.productDesc = desc;
            this.salesBlurb = sellwords;
            this.cost = cost;

            //markup is automatic (can be changed per business requirements)
            this.SalePrice = cost * 1.5; 
            
            this.saleValue = SalePrice - cost;
            this.quantityOnHand = qty;
        }

        public double Profitability()
        {
            return this.SalePrice - this.cost;
        }

        public void AdjustQty(int adjustment)
        {
            this.quantityOnHand += adjustment;
        }

        private void SetQuantityOnHand( int adustment)
        {
            this.QuantityOnHand += adustment;
        }

    }
}
