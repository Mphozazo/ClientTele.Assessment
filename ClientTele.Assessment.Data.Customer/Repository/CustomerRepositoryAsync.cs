using ClientTele.Assessment.Data.Application;
using ClientTele.Assessment.Data.Customer.Interface;
using ClientTele.Assessment.Data.Customer.Model;
using Microsoft.EntityFrameworkCore;

namespace ClientTele.Assessment.Data.Customer.Repository
{
    public class CustomerRepositoryAsync : GenericRepositoryAsync<CustomerEntity>, ICustomerEntityAsync
    {
        public CustomerRepositoryAsync(ApplicationDbContext context) : base(context) { }

        public async Task<CustomerEntity?> GetCustomerByEmailAsync(string email)
        {
           return await FindByConditionAsync(x => x.Email == email);
        }

        public async Task<CustomerEntity?> GetCustomerByNameAsync(string name)
        {
            return await FindByConditionAsync(x => x.Name == name);
        }
    }
}
