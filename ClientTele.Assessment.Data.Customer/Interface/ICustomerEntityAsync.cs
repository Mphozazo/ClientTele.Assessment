using ClientTele.Assessment.Data.Customer.Model;

namespace ClientTele.Assessment.Data.Customer.Interface
{
    /// <summary>
    /// Customer repository interface for async operations with extra operations
    /// </summary>
    public interface ICustomerEntityAsync : IGenericRepositoryAsync<CustomerEntity>
    {
        Task<CustomerEntity?> GetCustomerByNameAsync(string name);
        Task<CustomerEntity?> GetCustomerByEmailAsync(string email);
    }
}
