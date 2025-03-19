using ClientTele.Assessment.Data.Customer.Interface;
using ClientTele.Assessment.Data.Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTele.Assessment.Data.Customer.Service
{
    /// <summary>
    /// Customer management service of all operations related to customer
    /// </summary>
    /// <remarks>Exposed to the UI</remarks>
    public class CustomerManagementServiceAsync
    {
        private readonly ICustomerEntityAsync _customerRepository;

        public CustomerManagementServiceAsync(ICustomerEntityAsync customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task AddCustomerAsync(CustomerEntity customer)
        {
            await _customerRepository.AddAsync(customer);
        }

        public async Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<CustomerEntity> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.FindByIdAsync(id);
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            return await _customerRepository.DeleteAsync(id);
        }

        public async Task<bool> DeleteCustomerAsync(CustomerEntity customer)
        {
            return await _customerRepository.DeleteAsync(customer);
            
        }

        /// <summary>
        ///  Find customer by condition
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<CustomerEntity?> FindCustomerByConditionAsync(Func<CustomerEntity, bool> expression)
        {
            return await _customerRepository.FindByConditionAsync(x => expression(x));
        }

        public async Task<CustomerEntity> UpdateCustomerAsync(CustomerEntity customer)
        {
            return await _customerRepository.UpdateAsync(customer);
        }



    }
}
