using GymMoves_WebAPI.Data.DBContext;
using GymMoves_WebAPI.Data.EntityModels;
using GymMoves_WebAPI.Data.Repositories.Implementation;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GymMoves_WebAPI_Tests {
    public class ClassRepositoryTests {
        [Fact]
        public async void TestAddClassReturnsClass() {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = new SqliteConnection(connectionStringBuilder.ToString());

            var options = new DbContextOptionsBuilder<MainDatabaseContext>()
                .UseSqlite(connectionString)
                .Options;

            using (var context = new MainDatabaseContext(options)) {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                var userRepository = new UserRepository(context);

                InstructorEntity instructor = new InstructorEntity() {
                    InstructorID = "testinstructor",
                    PhoneNumber = "0629058357",
                    FirstName = "TestUser",
                    LastName = "McTesto",
                    Password = "somepassword",
                    RegisteredGymFK = 1
                };

                await userRepository.AddInstructor(instructor);

                var repository = new ClassRepository(context);

                ClassEntity newClass = new ClassEntity() {
                    ClassTypeFK = 1,
                    InstructorIDFK = "testinstructor",
                    AtGymFK = 1,
                    ClassTimeFK = 1,
                    maxCapacity = 50,
                    registeredCount = 0
                };

                await repository.Add(newClass);

                ClassEntity returnedClass = await repository.FindByID(1);

                Assert.NotNull(returnedClass);
            }
        }

        [Fact]
        public async void TestAddClassesAndFetchByGym() {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = new SqliteConnection(connectionStringBuilder.ToString());

            var options = new DbContextOptionsBuilder<MainDatabaseContext>()
                .UseSqlite(connectionString)
                .Options;

            using (var context = new MainDatabaseContext(options)) {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                var userRepository = new UserRepository(context);

                InstructorEntity instructor = new InstructorEntity() {
                    InstructorID = "testinstructor",
                    PhoneNumber = "0629058357",
                    FirstName = "TestUser",
                    LastName = "McTesto",
                    Password = "somepassword",
                    RegisteredGymFK = 1
                };

                await userRepository.AddInstructor(instructor);

                var repository = new ClassRepository(context);

                ClassEntity newClass = new ClassEntity() {
                    ClassTypeFK = 1,
                    InstructorIDFK = "testinstructor",
                    AtGymFK = 1,
                    ClassTimeFK = 1,
                    maxCapacity = 50,
                    registeredCount = 0
                };

                ClassEntity newClass1 = new ClassEntity() {
                    ClassTypeFK = 1,
                    InstructorIDFK = "testinstructor",
                    AtGymFK = 1,
                    ClassTimeFK = 1,
                    maxCapacity = 50,
                    registeredCount = 0
                };

                ClassEntity newClass2 = new ClassEntity() {
                    ClassTypeFK = 1,
                    InstructorIDFK = "testinstructor",
                    AtGymFK = 1,
                    ClassTimeFK = 1,
                    maxCapacity = 50,
                    registeredCount = 0
                };

                await repository.Add(newClass);
                await repository.Add(newClass1);
                await repository.Add(newClass2);

                var gymRepository = new GymRepository(context);

                ClassEntity[] returnedClasses = await repository.FindByGym(await gymRepository.FindById(1));

                Assert.Equal(3, returnedClasses.Length);
            }
        }
    }
}
