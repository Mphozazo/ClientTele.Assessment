
using ClientTele.Assessment.Data.Application;
using ClientTele.Assessment.Data.Customer.Interface;
using ClientTele.Assessment.Data.Customer.Model;
using ClientTele.Assessment.Data.Customer.Repository;
using ClientTele.Assessment.Data.Customer.Service;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ClientTele.Assassment.Console.CustomerManagement
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Setup Dependency Injection
            var serviceProvider = new ServiceCollection()
                .AddDbContext<ApplicationDbContext>() 
                .AddSingleton<ICustomerEntityAsync, CustomerRepositoryAsync>() // Provide your implementation for ICustomerEntityAsync
                .AddSingleton<CustomerManagementServiceAsync>()
                .BuildServiceProvider();

            // Create a scope to resolve services
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.CreateDatabaseIfNotExists(); // Ensure the database is created if not exists

                // Get the service
                var customerService = serviceProvider.GetService<CustomerManagementServiceAsync>();

                if (customerService == null)
                {
                    System.Console.WriteLine("Failed to get customer service.");
                    return;
                }

                #region Add Customer
                var customer = new Customer()
                {
                    Name = "Walter Heisi",
                    Email = "walter@somewhere.com",
                    PhoneNumber = "1234567890"
                };
               // await customerService.AddCustomerAsync(customer);
                System.Console.WriteLine("Customer added.");

                #endregion

                int lastCustomerId = 0;
                #region Get all customers
                IEnumerable<Customer> customers = await customerService.GetAllCustomersAsync();
                System.Console.WriteLine("All Customers:");
                foreach (var allCustomers in customers)
                {
                    System.Console.WriteLine($"ID: {allCustomers.Id}, Name: {allCustomers.Name}, Email: {allCustomers.Email} , Phone Number: {allCustomers.PhoneNumber}");
                    lastCustomerId = allCustomers.Id;
                }

                #endregion

                #region Get Customer by ID

                var customerById = await customerService.GetCustomerByIdAsync(lastCustomerId);
                if (customerById != null)
                {
                    System.Console.WriteLine($"Customer found: ID: {customerById.Id}, Name: {customerById.Name}, Email: {customerById.Email} , Phone Number :{customerById.PhoneNumber}");
                }
                #endregion


                #region Update a customer 
                var customerToUpdate = new Customer
                {
                    Id = lastCustomerId,
                    Name = "John Smith",
                    Email = "johnsmith@example.com",
                    PhoneNumber = "0987654321"
                };

                var updatedCustomer = await customerService.UpdateCustomerAsync(customerToUpdate);
                System.Console.WriteLine($"Updated Customer: {updatedCustomer.Name}, {updatedCustomer.Email}");

                #endregion

                #region Get Customer by email address
                var customerByEmail = await customerService.FindCustomerByEmailAsync("johnsmith@example.com");
                if (customerByEmail != null)
                {
                    System.Console.WriteLine($"Customer found by email: ID: {customerByEmail.Id}, Name: {customerByEmail.Name}, Email: {customerByEmail.Email} , Phone Number: {customerByEmail.PhoneNumber}");
                }
                #endregion

                #region Delete a customer 
                bool isDeleted = await customerService.DeleteCustomerAsync(lastCustomerId);
                System.Console.WriteLine(isDeleted ? "Customer deleted successfully." : "Customer deletion failed.");

                #endregion

            }


        }
    }
}
