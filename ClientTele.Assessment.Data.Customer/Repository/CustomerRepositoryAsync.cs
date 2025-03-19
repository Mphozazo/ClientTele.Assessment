using ClientTele.Assessment.Data.Application;
using ClientTele.Assessment.Data.Customer.Interface;
using ClientTele.Assessment.Data.Customer.Model;
using Microsoft.EntityFrameworkCore;

namespace ClientTele.Assessment.Data.Customer.Repository
{
    /// <summary>
    ///  Customer Repository for async CRUD operations from Generic Repository
    /// </summary>
    public class CustomerRepositoryAsync : GenericRepositoryAsync<Model.Customer>, ICustomerEntityAsync
    {
        public CustomerRepositoryAsync(ApplicationDbContext context) : base(context) { }

        public async Task<Model.Customer?> GetCustomerByEmailAsync(string email)
        {
           return await FindByConditionAsync(x => x.Email == email);
        }

        public async Task<Model.Customer?> GetCustomerByNameAsync(string name)
        {
            return await FindByConditionAsync(x => x.Name == name);
        }
    }
}
