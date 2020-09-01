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
    public class NotificationsControllerTests : TestWithSqlite
    {

        [Fact]
        public async Task DatabaseIsAvailableAndCanBeConnectedTo()
        {
            Assert.True(await DbContext.Database.CanConnectAsync());
        }

        [Fact]
        public async Task getAnnouncementsTest()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("AppSettings.json");
            IConfiguration configuration = configurationBuilder.Build();

            GymRepository gym = new GymRepository(DbContext);
            await gym.addGym(new Gym { GymName = "testName", GymBranch = "testBranch" });

            NotificationsRepository notif = new NotificationsRepository(DbContext);


            await notif.addNotification(new Notifications 
            { Body = "body", Date = DateTime.Now, Heading = "heading", GymIdForeignKey = 1 });

            var controller = new NotificationsController(new NotificationSettingsRepository(DbContext), 
               notif, gym, new Mailer(configuration));


            var okObjectResult = Assert.IsType<Task<ActionResult<Notifications[]>>>(controller.getAllAnnouncements(1));


            //Assert.IsNotType<string>(okObjectResult.Value);
        }
    }
}
