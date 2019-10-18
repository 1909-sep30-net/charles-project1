using System;
using System.Collections.Generic;

namespace data_access.Entities
{
    public partial class StoreLocation
    {
        public StoreLocation()
        {
            CustOrder = new HashSet<CustOrder>();
            Inventory = new HashSet<Inventory>();
        }

        public int LocationId { get; set; }
        public string StoreName { get; set; }
        public string Phone { get; set; }
        public int Manager { get; set; }

        public virtual Manager ManagerNavigation { get; set; }
        public virtual ICollection<CustOrder> CustOrder { get; set; }
        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
