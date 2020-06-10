using GymMoves_WebAPI.Data.DBContext;
using GymMoves_WebAPI.Data.EntityModels;
using GymMoves_WebAPI.Data.Repositories.Implementation;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GymMoves_WebAPI_Tests {
    public class UserRepositoryTests {

        [Fact]
        public async void TestAddUserReturnsUser() {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = new SqliteConnection(connectionStringBuilder.ToString());

            var options = new DbContextOptionsBuilder<MainDatabaseContext>()
                .UseSqlite(connectionString)
                .Options;

            using (var context = new MainDatabaseContext(options)) {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                var repository = new UserRepository(context);

                UserEntity newUser = new UserEntity() {
                    UserID = "unittestuser",
                    PhoneNumber = "0629058357",
                    FirstName = "TestUser",
                    LastName = "McTesto",
                    Password = "somepassword",
                    UserType = UserType.Member,
                    RegisteredGymFK = 1
                };

                await repository.Add(newUser);

                UserEntity returnUser = await repository.GetUserWithID("unittestuser");

                Assert.NotNull(returnUser);
            }
        }
    }
}
