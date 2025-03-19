using ClientTele.Assessment.Data.Customer.Model;
using Microsoft.EntityFrameworkCore;

namespace ClientTele.Assessment.Data.Application
{
    public class ApplicationDbContext :DbContext
    {
        public DbSet<Customer.Model.Customer> Customers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Using SQLite for simplicity
            optionsBuilder.UseSqlite("Data Source=customers.db");
           

        }

        public void CreateDatabaseIfNotExists()
        {
            // Create the database if it doesn't already exist
            this.Database.EnsureCreated();
        }

    }
}
