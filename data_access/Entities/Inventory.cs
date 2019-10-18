using System;
using System.Collections.Generic;

namespace data_access.Entities
{
    public partial class Inventory
    {
        public int LocationId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual StoreLocation Location { get; set; }
        public virtual Product Product { get; set; }
    }
}
