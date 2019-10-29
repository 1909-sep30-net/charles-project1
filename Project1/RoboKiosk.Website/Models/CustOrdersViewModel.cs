using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoboKiosk.Website.Models
{
    public class CustOrdersViewModel
    {

        //[DisplayName("ID")] // this one is associated with the Html.DisplayNameFor helper

        //[DisplayFormat(] // there are attributes like Display and DisplayFormat
        // to modify here in this one place how values in this property will be displayed in some view
        // (when we use the Html.DisplayFor HTML helper!)
        [Required] // can't be null or empty string
        [DisplayName("Order Number")]

        public string OrderID { get; set; }

        [Required]
        [DisplayName("Customer Mobile Number")]
        public string CustID { get; set; }

        [Required]
        [DisplayName("Location Number")]

        public string LocationId { get; set; }

        [Required]
        [DisplayName("Order Date")]

        public string OrderDate_Timestamp { get; set; }

        public DateTime OrderDate { get; set; }

        public int ProdID { get; set; }


        public List<TempSet> TupProdQtyObjList { get; set; }

        public Dictionary<int, int> TypeQtyDict { get; set; }

        //for Sales Description display purposes only... may be pointless
        public List<string> Products { get; set; }

        //for labels on form-data, long string name of a product
        public Dictionary<int, string> ProdCodec { get; set; }

        //product IDs.
        public List<int> ProdIDs { get; set; }

        //a tuple to get the product's id, short, and then long name.
        public List<Tuple<int, string, string> > ProdTrippleList {get;set;}

    }

    public class TempSet
    {
        public int prodID, qty;
        public string prodLabel;

        
        public string prodSales;

        
        public string ProdSales { get; set; }

        public TempSet(string l, int p, int q, string s)
        {
            this.prodLabel = l;
            this.prodID = p;
            this.qty = q;
            this.prodSales = s;
        }
    }
}


