using GymMovesWebAPI.Controllers;
using GymMovesWebAPI.Data.Enums;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.RequestModels;
using GymMovesWebAPI.Data.Models.ResponseModels;
using GymMovesWebAPI.Data.Repositories.Implementations;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.MailerProgram;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GymMovesWebAPI.Tests {
    public class GymClassesTests : TestWithSqlite {
        private readonly IUserRepository userRepository;
        private readonly IClassRepository classRepository;
        private readonly IClassRegisterRepository registerRepository;
        private readonly IGymRepository gymRepository;
        private readonly IClassAttendanceRepository classAttendanceRepository;
        private readonly IMailer mailer;

        private readonly ClassesController classesController;

        public GymClassesTests() : base() {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("AppSettings.json");
            IConfiguration configuration = configurationBuilder.Build();

            userRepository = new UserRepository(DbContext);
            registerRepository = new ClassRegisterRepository(DbContext);
            classRepository = new ClassRepository(DbContext, registerRepository);
            gymRepository = new GymRepository(DbContext);
            classAttendanceRepository = new ClassAttendanceRepository(DbContext);

            mailer = new Mailer(configuration);

            classesController = new ClassesController(userRepository, classRepository, registerRepository, gymRepository, classAttendanceRepository, mailer);
        }

        [Fact]
        public async Task CheckConnection() {
            Assert.True(await DbContext.Database.CanConnectAsync());
        }

        [Fact]
        public async Task CanAddClassCanBookClassCanRemoveClass() {
            await gymRepository.addGym(new Gym {
                GymId = 10,
                GymName = "Temp",
                GymBranch = "Gym"
            });

            await userRepository.addUser(new Users {
                Username = "instructor",
                GymIdForeignKey = 10,
                UserType = UserTypes.Instructor
            });

            await userRepository.addUser(new Users {
                Username = "manager",
                GymIdForeignKey = 10,
                UserType = UserTypes.Manager
            });

            GymClassRequest request = new GymClassRequest {
                Username = "manager",
                NewClass = new GymClassResponse {
                    ClassId = 3,
                    GymId = 10,
                    Instructor = "instructor",
                    Name = "Test",
                    Description = "Desc",
                    Day = "Wednesday",
                    StartTime = "11:00",
                    EndTime = "12:00",
                    MaxCapacity = 10
                }
            };

            var response = await classesController.addClass(request);

            Assert.IsType<OkObjectResult>(response.Result);

            /* Book */
            RegisterUserForClassRequest request1 = new RegisterUserForClassRequest { 
                Username = "manager",
                ClassId = 3
            };

            response = await classesController.signUpUserToClass(request1);

            Assert.IsType<OkObjectResult>(response.Result);

            GymClassRemoveRequest request2 = new GymClassRemoveRequest {
                Username = "manager",
                ClassId = 3
            };

            response = await classesController.removeClass(request2);

            Assert.IsType<OkObjectResult>(response.Result);
        }
    }
}
