using GymMoves_WebAPI.Data.DBContext;
using GymMoves_WebAPI.Data.EntityModels;
using GymMoves_WebAPI.Data.Repositories.Implementation;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
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

        [Fact]
        public async void TestCheckDuplicateAddUnsuccessful() {
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

                UserEntity newUser1 = new UserEntity() {
                    UserID = "unittestuser",
                    PhoneNumber = "0629058357",
                    FirstName = "TestUser",
                    LastName = "McTesto",
                    Password = "somepassword",
                    UserType = UserType.Member,
                    RegisteredGymFK = 1
                };

                await repository.Add(newUser);
                Assert.False(await repository.Add(newUser1));
            }
        }

        [Fact]
        public async void TestCheckGetsRightUserWhenMultipleUsersExist() {
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

                UserEntity newUser1 = new UserEntity() {
                    UserID = "unittestuser1",
                    PhoneNumber = "0629058357",
                    FirstName = "TestUser",
                    LastName = "McTesto",
                    Password = "somepassword",
                    UserType = UserType.Member,
                    RegisteredGymFK = 1
                };

                UserEntity newUser2 = new UserEntity() {
                    UserID = "unittestuser2",
                    PhoneNumber = "0629058357",
                    FirstName = "TestUser",
                    LastName = "McTesto",
                    Password = "somepassword",
                    UserType = UserType.Manager,
                    RegisteredGymFK = 1
                };

                UserEntity newUser3 = new UserEntity() {
                    UserID = "unittestuser3",
                    PhoneNumber = "0629058357",
                    FirstName = "TestUser",
                    LastName = "McTesto",
                    Password = "somepassword",
                    UserType = UserType.Member,
                    RegisteredGymFK = 1
                };

                await repository.Add(newUser);
                await repository.Add(newUser1);
                await repository.Add(newUser2);
                await repository.Add(newUser3);

                UserEntity getEntity = await repository.GetUserWithID(newUser2.UserID);

                Assert.Equal(UserType.Manager, getEntity.UserType);
            }
        }

        [Fact]
        public async void TestCheckAddInstructorReturnsInstructor() {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = new SqliteConnection(connectionStringBuilder.ToString());

            var options = new DbContextOptionsBuilder<MainDatabaseContext>()
                .UseSqlite(connectionString)
                .Options;

            using (var context = new MainDatabaseContext(options)) {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                var repository = new UserRepository(context);

                InstructorEntity instructor = new InstructorEntity() {
                    InstructorID = "testinstructor",
                    PhoneNumber = "0629058357",
                    FirstName = "TestUser",
                    LastName = "McTesto",
                    Password = "somepassword",
                    RegisteredGymFK = 1
                };

                await repository.AddInstructor(instructor);

                InstructorEntity returnedInstructor = await repository.GetInstructorWithID(instructor.InstructorID);

                Assert.NotNull(returnedInstructor);
            }
        }
    }
}
