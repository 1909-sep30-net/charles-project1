using System;
using System.Collections.Generic;

namespace data_access.Entities
{
    public partial class LineItem
    {
        public long LineItemId { get; set; }
        public long OrderId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual CustOrder Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
