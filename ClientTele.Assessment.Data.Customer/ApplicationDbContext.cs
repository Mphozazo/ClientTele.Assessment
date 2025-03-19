using Microsoft.EntityFrameworkCore;

namespace ClientTele.Assessment.Data.Application
{
    public class ApplicationDbContext :DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Using SQLite for simplicity
            optionsBuilder.UseSqlite("Data Source=customers.db");

        }

    }
}
