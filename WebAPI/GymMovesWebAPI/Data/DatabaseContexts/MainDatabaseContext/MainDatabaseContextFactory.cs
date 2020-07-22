using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GymMoves_WebAPI.Data.DBContext {
    public class MainDatabaseContextFactory : IDesignTimeDbContextFactory<MainDatabaseContext> {
        public MainDatabaseContext CreateDbContext(string[] args) {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return new MainDatabaseContext(new DbContextOptionsBuilder<MainDatabaseContext>().Options, config);
        }
    }
}
