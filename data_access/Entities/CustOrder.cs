using System;
using System.Collections.Generic;

namespace data_access.Entities
{
    public partial class CustOrder
    {
        public CustOrder()
        {
            LineItem = new HashSet<LineItem>();
        }

        public long OrderId { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual StoreLocation Location { get; set; }
        public virtual ICollection<LineItem> LineItem { get; set; }
    }
}
