using ClientTele.Assessment.Data.Customer.Interface;
using ClientTele.Assessment.Data.Customer.Model;

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

        public async Task AddCustomerAsync(Model.Customer customer)
        {
            await _customerRepository.AddAsync(customer);
            await _customerRepository.SaveAsync();
        }

        public async Task<IEnumerable<Model.Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Model.Customer> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.FindByIdAsync(id);
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            try
            {
                var results  = await _customerRepository.DeleteAsync(id);
                await _customerRepository.SaveAsync();
                return results;
            }
            catch 
            {

                throw;
            }
             

        }

        public async Task<bool> DeleteCustomerAsync(Model.Customer customer)
        {
            try
            {
               var result  =  await _customerRepository.DeleteAsync(customer);
                await _customerRepository.SaveAsync();
                return result;
            }
            catch
            {

                throw;
            }
            
            
        }

        public async Task<Model.Customer> UpdateCustomerAsync(Model.Customer customer)
        {
            try
            {
                var results  = await _customerRepository.UpdateAsync(customer);
                await _customerRepository.SaveAsync();
                return results;

            }
            catch 
            {

                throw;
            }
            
        }

        public async Task<Model.Customer?> FindCustomerByEmailAsync(string email)
        {
            return await _customerRepository.FindByConditionAsync(x => x.Email == email);
        }
        public async Task<Model.Customer?> FindCustomerByAsync(string name)
        {
            return await _customerRepository.FindByConditionAsync(x => x.Name == name);
        }
    }
}
