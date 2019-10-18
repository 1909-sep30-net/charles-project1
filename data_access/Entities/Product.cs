using System;
using System.Collections.Generic;

namespace data_access.Entities
{
    public partial class Product
    {
        public Product()
        {
            Inventory = new HashSet<Inventory>();
            LineItem = new HashSet<LineItem>();
        }

        public int ProductId { get; set; }
        public string Pname { get; set; }
        public string SalesName { get; set; }
        public decimal? Cost { get; set; }
        public decimal? SalesPrice { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<LineItem> LineItem { get; set; }
    }
}
