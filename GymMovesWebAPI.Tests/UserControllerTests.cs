using GymMovesWebAPI.Controllers;
using GymMovesWebAPI.Data.Models.RequestModels;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using Microsoft.Data.Sqlite;
using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using System.Threading.Tasks;
using GymMovesWebAPI.Data.Repositories.Implementations;
using GymMovesWebAPI.MailerProgram;
using Microsoft.Extensions.Configuration;
using GymMovesWebAPI.Data.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.VerificationDatabaseModels;

namespace GymMovesWebAPI.Tests
{
    public class UserControllerTests : TestWithSqlite
    {

        [Fact]
        public async Task DatabaseIsAvailableAndCanBeConnectedTo()
        {
            Assert.True(await DbContext.Database.CanConnectAsync());
        }

        [Fact]
        public async Task signUpTest()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("AppSettings.json");
            IConfiguration configuration = configurationBuilder.Build();

            GymRepository gym = new GymRepository(DbContext);
            await gym.addGym(new Gym { GymName = "testName", GymBranch = "testBranch" });

            GymMemberRepository gymMember = new GymMemberRepository(DbContext);
           bool add = await gymMember.addMember(new GymMember
            {
                MembershipId = "1",
                Email = "email@oh.com",
                GymId = 1,
                Name = "name",
                Surname = "surname",
                PhoneNumber = "0321456956",
                UserType = Data.Enums.UserTypes.Instructor
            });

            Assert.True(add);


            var userController = new UserController(new UserRepository(DbContext), gymMember,
               gym, new NotificationSettingsRepository(DbContext), new PasswordResetRepository(DbContext), 
                new Mailer(configuration));

           ActionResult<UserResponseModel> signedup = await userController.signUp(new SignUpRequestModel {username = "test",
            password = "test", gymBranch = "testBranch", gymMemberId = "1", gymName = "testName"});

            var okObjectResult = Assert.IsType<ActionResult<UserResponseModel>>(signedup);

            Assert.IsNotType<string>(okObjectResult.Value);
        }


        [Fact]
        public async Task ChangePassword()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("AppSettings.json");
            IConfiguration configuration = configurationBuilder.Build();

            GymRepository gym = new GymRepository(DbContext);
            await gym.addGym(new Gym { GymName = "testName", GymBranch = "testBranch" });

            GymMemberRepository gymMember = new GymMemberRepository(DbContext);
            bool add = await gymMember.addMember(new GymMember
            {
                MembershipId = "1",
                Email = "email@oh.com",
                GymId = 1,
                Name = "name",
                Surname = "surname",
                PhoneNumber = "0321456956",
                UserType = Data.Enums.UserTypes.Instructor
            });

            Assert.True(add);


            var userController = new UserController(new UserRepository(DbContext), gymMember,
               gym, new NotificationSettingsRepository(DbContext), new PasswordResetRepository(DbContext),
                new Mailer(configuration));

            ActionResult<UserResponseModel> signedup = await userController.signUp(new SignUpRequestModel
            {
                username = "test",
                password = "test",
                gymBranch = "testBranch",
                gymMemberId = "1",
                gymName = "testName"
            });

           ActionResult<UserResponseModel> changedPassword = await userController.changePassword(new ChangePasswordRequestModel
            {
                username = "test",
                oldPassword = "test",
                newPassword = "test1",
            });

            var okObjectResult = Assert.IsType<ActionResult<UserResponseModel>>(changedPassword);

            Assert.IsNotType<string>(okObjectResult.Value);
        }



        [Fact]
        public async Task LogInTest()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("AppSettings.json");
            IConfiguration configuration = configurationBuilder.Build();

            GymRepository gym = new GymRepository(DbContext);
            await gym.addGym(new Gym { GymName = "testName", GymBranch = "testBranch" });

            GymMemberRepository gymMember = new GymMemberRepository(DbContext);
            bool add = await gymMember.addMember(new GymMember
            {
                MembershipId = "1",
                Email = "email@oh.com",
                GymId = 1,
                Name = "name",
                Surname = "surname",
                PhoneNumber = "0321456956",
                UserType = Data.Enums.UserTypes.Instructor
            });

            Assert.True(add);


            var userController = new UserController(new UserRepository(DbContext), gymMember,
               gym, new NotificationSettingsRepository(DbContext), new PasswordResetRepository(DbContext),
                new Mailer(configuration));

            ActionResult<UserResponseModel> signedup = await userController.signUp(new SignUpRequestModel
            {
                username = "test",
                password = "test",
                gymBranch = "testBranch",
                gymMemberId = "1",
                gymName = "testName"
            });

            ActionResult<UserResponseModel> loggedin = await userController.logIn(new LogInRequestModel
            {
                username = "test",
                password = "test",
            });

            var okObjectResult = Assert.IsType<ActionResult<UserResponseModel>>(loggedin);

            Assert.IsNotType<string>(okObjectResult.Value);
        }


        [Fact]
        public async Task getInstructors()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("AppSettings.json");
            IConfiguration configuration = configurationBuilder.Build();

            GymRepository gym = new GymRepository(DbContext);
            await gym.addGym(new Gym { GymName = "testName", GymBranch = "testBranch" });

            GymMemberRepository gymMember = new GymMemberRepository(DbContext);
            bool add = await gymMember.addMember(new GymMember
            {
                MembershipId = "1",
                Email = "email@oh.com",
                GymId = 1,
                Name = "name",
                Surname = "surname",
                PhoneNumber = "0321456956",
                UserType = Data.Enums.UserTypes.Instructor
            });

            Assert.True(add);


            var userController = new UserController(new UserRepository(DbContext), gymMember,
               gym, new NotificationSettingsRepository(DbContext), new PasswordResetRepository(DbContext),
                new Mailer(configuration));

            ActionResult<UserResponseModel> signedup = await userController.signUp(new SignUpRequestModel
            {
                username = "test",
                password = "test",
                gymBranch = "testBranch",
                gymMemberId = "1",
                gymName = "testName"
            });

            ActionResult<InstructorResponseModel[]> instructors = await userController.getAllInstructors(1);

            var okObjectResult = Assert.IsType<ActionResult<InstructorResponseModel[]>>(instructors);

            Assert.IsNotType<string>(okObjectResult.Value);
        }

    }
}
