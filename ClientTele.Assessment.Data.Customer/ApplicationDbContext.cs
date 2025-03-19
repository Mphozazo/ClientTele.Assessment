using ClientTele.Assessment.Data.Customer.Model;
using Microsoft.EntityFrameworkCore;

namespace ClientTele.Assessment.Data.Application
{
    public class ApplicationDbContext :DbContext
    {
        public DbSet<CustomerEntity> Customers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Using SQLite for simplicity
            optionsBuilder.UseSqlite("Data Source=customers.db");

        }

    }
}
