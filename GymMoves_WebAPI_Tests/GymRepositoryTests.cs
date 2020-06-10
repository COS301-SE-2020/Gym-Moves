using GymMoves_WebAPI.Data.DBContext;
using GymMoves_WebAPI.Data.EntityModels;
using GymMoves_WebAPI.Data.Repositories.Implementation;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GymMoves_WebAPI_Tests {
    public class GymRepositoryTests {
        [Fact]
        public async void DefaultGymExists() {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = new SqliteConnection(connectionStringBuilder.ToString());

            var options = new DbContextOptionsBuilder<MainDatabaseContext>()
                .UseSqlite(connectionString)
                .Options;

            using (var context = new MainDatabaseContext(options)) {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                var gymRepository = new GymRepository(context);

                GymEntity getEntity = await gymRepository.FindById(1);

                Assert.NotNull(getEntity);
            }
        }
    }
}
