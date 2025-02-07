using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace KoperasiTenteraDAL.Context
{
    public class UserContextFactorycs : IDesignTimeDbContextFactory<UserContext>
    {
        public UserContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = config.GetConnectionString("BaseConnection");

            var options = new DbContextOptionsBuilder<UserContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new UserContext(options);
        }
    }
}