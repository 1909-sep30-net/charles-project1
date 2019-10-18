using System;
using System.Collections.Generic;

namespace data_access.Entities
{
    public partial class Manager
    {
        public Manager()
        {
            StoreLocation = new HashSet<StoreLocation>();
        }

        public int ManagerId { get; set; }
        public string ManagerPw { get; set; }

        public virtual ICollection<StoreLocation> StoreLocation { get; set; }
    }
}
