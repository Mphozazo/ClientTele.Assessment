using ClientTele.Assessment.Data.Application;
using ClientTele.Assessment.Data.Customer.Model;
using ClientTele.Assessment.Data.Customer.Repository;
using Microsoft.EntityFrameworkCore;
namespace ClientTele.Assessment.Test.Repository
{
    public class CustomerRepositoryTests
    {
        private async Task<ApplicationDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            var dbContext = new ApplicationDbContext();
            await dbContext.Database.EnsureCreatedAsync();

            // Seed Test Data
            dbContext.Customers.Add(new CustomerEntity { Name = "Walter Heisi", Email = "walterh@example.com", PhoneNumber = "1234567890" });
            await dbContext.SaveChangesAsync();

            return dbContext;
        }

        [Fact]
        public async Task GetCustomerByEmailAsync_ShouldReturn_Customer_WhenExists()
        {
            var dbContext = await GetDatabaseContext();
            var repository = new CustomerRepositoryAsync(dbContext);

            var customer = await repository.GetCustomerByEmailAsync("walterh@example.com");

            Assert.NotNull(customer);
            Assert.Equal("John Doe", customer.Name);
        }

        [Fact]
        public async Task GetCustomerByEmailAsync_ShouldReturn_Null_WhenNotExists()
        {
            var dbContext = await GetDatabaseContext();
            var repository = new CustomerRepositoryAsync(dbContext);

            var customer = await repository.GetCustomerByEmailAsync("notfound@example.com");

            Assert.Null(customer);
        }

        [Fact]
        public async Task AddAsync_ShouldAdd_Customer()
        {
            var dbContext = await GetDatabaseContext();
            var repository = new CustomerRepositoryAsync(dbContext);

            var newCustomer = new CustomerEntity { Name = "Walter Heisi", Email = "walterh@example.com", PhoneNumber = "9876543210" };
            await repository.AddAsync(newCustomer);
            await repository.SaveAsync();

            var customer = await dbContext.Customers.FirstOrDefaultAsync(x => x.Email == "walterh@example.com");

            Assert.NotNull(customer);
            Assert.Equal("Walter Heisi", customer.Name);
        }
    }

}
