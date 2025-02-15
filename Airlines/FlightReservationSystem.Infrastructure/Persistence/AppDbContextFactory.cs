//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
//using FlightReservationSystem.Infrastructure.Persistence;
//using System.IO;

//namespace FlightReservationSystem.Infrastructure.Persistence
//{
//    /// <summary>
//    /// This factory helps EF Core to create AppDbContext at design-time.
//    /// </summary>
//    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
//    {
//        /// <summary>
//        /// This method is called by EF Core tools to create a DbContext instance.
//        /// </summary>
//        public AppDbContext CreateDbContext(string[] args)
//        {
//            // Build the options for the DbContext
//            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

//            // Get the configuration from appsettings.json
//            var config = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())  // Use Directory.GetCurrentDirectory() to find the base path
//                .AddJsonFile("appsettings.json") // Specify the appsettings.json file
//                .Build();

//            // Get the connection string from the configuration
//            var connectionString = config.GetConnectionString("DefaultConnection");

//            // Configure DbContext to use SQL Server with the connection string
//            optionsBuilder.UseSqlServer(connectionString);

//            return new AppDbContext(optionsBuilder.Options);
//        }
//    }
//}
