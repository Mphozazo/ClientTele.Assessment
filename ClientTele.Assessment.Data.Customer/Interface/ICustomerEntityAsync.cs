using ClientTele.Assessment.Data.Customer.Model;

namespace ClientTele.Assessment.Data.Customer.Interface
{
    /// <summary>
    /// Customer repository interface for async operations with extra operations
    /// </summary>
    public interface ICustomerEntityAsync : IGenericRepositoryAsync<Model.Customer>
    {
        Task<Model.Customer?> GetCustomerByNameAsync(string name);
        Task<Model.Customer?> GetCustomerByEmailAsync(string email);
    }
}
