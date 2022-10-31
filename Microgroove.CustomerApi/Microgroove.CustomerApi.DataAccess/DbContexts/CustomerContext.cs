using Microgroove.CustomerApi.DataAccess.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Microgroove.CustomerApi.DataAccess.DbContexts
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public string DbPath { get; }

        static CustomerContext() 
        {
            EnsureDbCreated();
        }

        public CustomerContext()
        {
            var folder = 
                Environment.SpecialFolder.LocalApplicationData;

            var path = Environment.GetFolderPath(folder);

            DbPath = Path.Join(path, "customers.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        #region Private methods

        public static void EnsureDbCreated()
        {
            using var db =
                new CustomerContext();
            db.Database.EnsureCreated();
        }

        #endregion
    }
}
