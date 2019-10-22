using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace business_logic
{
    //I believe this is the point of intereaction from the data-access to the business logic
    public interface IRepository
    {

        /// <summary>
        /// Interface to Get a list of customers required for the Repository class in data_access to
        /// quickly get information about the business_logic.Customer class and re-create Customer(s) from
        /// raw data, which are then displayed via the appropriate View.
        /// </summary>
        /// <returns></returns>
        //threaded version
        Task<IEnumerable<Customer>> GetAllCustomersAsync();

        //for adding customers.
        Task AddCustomerAsync(Customer customer);

        Task<IEnumerable<Order>> GetAllCustOrdersAsync();

        Task AddCustOrderAsync(Order order);





    }
}
