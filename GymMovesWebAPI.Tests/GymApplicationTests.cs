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
using GymMovesWebAPI.Controllers;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.Data.Models.RequestModels;
using System.Net;
using GymMovesWebAPI.Data.Enums;

namespace GymMovesWebAPI.Tests {
    public class GymApplicationTests : TestWithSqlite {
        /* Repositories */
        private readonly IGymApplicationRepository applicationRepository;
        private readonly IGymRepository gymRepository;
        private readonly ISupportStaffRepository staffRepository;
        private readonly IApplicationCodeRepository codeRepository;
        private readonly IMailer mailer;

        private readonly GymApplicationController gymApplicationController;

        public GymApplicationTests() : base() {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            IConfiguration configuration = configurationBuilder.Build();

            /* Setup Repositories */
            applicationRepository = new GymApplicationRepository(DbContext);
            gymRepository = new GymRepository(DbContext);
            staffRepository = new SupportStaffRepository(DbContext);
            codeRepository = new ApplicationCodeRepository(DbContext);
            mailer = new Mailer(configuration);

            /* Setup Controllers */
            gymApplicationController = new GymApplicationController(applicationRepository, gymRepository, staffRepository, codeRepository, mailer);
        }

        /* Check that we can connect to the database */
        [Fact]
        public async Task CanConnectToDatabase() {
            Assert.True(await DbContext.Database.CanConnectAsync());
        }

        /* Check new gym application can be added */
        [Fact]
        public async Task AddGymApplication() {
            GymApplicationsRequest request = new GymApplicationsRequest {
                Name = "Longji",
                Surname = "Kang",
                Email = "brachypelmavagans159@gmail.com",
                PhoneNumber = "0629058357",
                GymName = "Test",
                BranchName = "Gym",
                Address = "An address",
                Extra = "None"
            };

            var response = await gymApplicationController.addApplication(request);

            Assert.IsType<OkObjectResult>(response.Result);

            bool removed = await applicationRepository.removeApplication((await applicationRepository.getApplication("Test", "Gym"))[0]);

            Assert.True(removed);
        }

        /* Test gym application approval */

        [Fact]
        public async Task ApproveGymApplication() {
            bool added = await applicationRepository.addApplication(
                new GymApplications {
                    Name = "Longji",
                    Surname = "Kang",
                    Email = "brachypelmavagans159@gmail.com",
                    PhoneNumber = "0629058357",
                    GymName = "Test",
                    BranchName = "Gym2",
                    Address = "An address",
                    Extra = "None",
                    Status = ApplicationStatus.Pending
                }
            );

            Assert.True(added);

            SetApplicationState request = new SetApplicationState {
                status = "Approve",
                GymName = "Test",
                BranchName = "Gym2"
            };

            var response = await gymApplicationController.AcceptApplications(request);

            Assert.IsType<OkObjectResult>(response.Result);

            bool removed = await applicationRepository.removeApplication((await applicationRepository.getApplication("Test", "Gym2"))[0]);

            Assert.True(removed);
        }

        /* Test application denial */
        [Fact]
        public async Task DenyGymApplication() {
            bool added = await applicationRepository.addApplication(
                new GymApplications {
                    Name = "Longji",
                    Surname = "Kang",
                    Email = "brachypelmavagans159@gmail.com",
                    PhoneNumber = "0629058357",
                    GymName = "Test",
                    BranchName = "Gym2",
                    Address = "An address",
                    Extra = "None",
                    Status = ApplicationStatus.Pending
                }
            );

            Assert.True(added);

            SetApplicationState request = new SetApplicationState {
                status = "Reject",
                GymName = "Test",
                BranchName = "Gym2"
            };

            var response = await gymApplicationController.AcceptApplications(request);

            Assert.IsType<OkObjectResult>(response.Result);

            bool removed = await applicationRepository.removeApplication((await applicationRepository.getApplication("Test", "Gym2"))[0]);

            Assert.True(removed);
        }

        /* Test Get All Applications Request */
        [Fact]
        public async Task GetAllApplications() {
            GetAllApplicationsRequest request = new GetAllApplicationsRequest {
                Username = "mastersupport"
            };

            var response = await gymApplicationController.getAllApplications(request);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        /* Test get specific application request */
        [Fact]
        public async Task GetSpecificAppliction() {
            bool added = await applicationRepository.addApplication(
               new GymApplications {
                   Name = "Longji",
                   Surname = "Kang",
                   Email = "brachypelmavagans159@gmail.com",
                   PhoneNumber = "0629058357",
                   GymName = "Test",
                   BranchName = "Gym2",
                   Address = "An address",
                   Extra = "None",
                   Status = ApplicationStatus.Pending
               }
            );

            Assert.True(added);

            GetAnApplicationRequest request = new GetAnApplicationRequest {
                Username = "mastersupport",
                GymName = "Test",
                BranchName = "Gym2"
            };

            var response = await gymApplicationController.getApplication(request);

            Assert.IsType<OkObjectResult>(response.Result);

            bool removed = await applicationRepository.removeApplication((await applicationRepository.getApplication("Test", "Gym2"))[0]);

            Assert.True(removed);
        }
    }
}
