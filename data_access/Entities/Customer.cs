using System;
using System.Collections.Generic;

namespace data_access.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            CustOrder = new HashSet<CustOrder>();
        }

        public int CustomerId { get; set; }
        public string Phone { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string CustomerPw { get; set; }

        public virtual ICollection<CustOrder> CustOrder { get; set; }
    }
}
