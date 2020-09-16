using GymMovesWebAPI.Controllers;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.RequestModels;
using GymMovesWebAPI.Data.Repositories.Implementations;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GymMovesWebAPI.Tests {
    public class GymAttendanceControllerTests : TestWithSqlite {
        private readonly IGymRepository gymRepository;
        private readonly IGymAttendenceRepository gymAttendenceRepository;

        private readonly GymAttendanceController attendanceController;

        public GymAttendanceControllerTests() : base() {
            gymRepository = new GymRepository(DbContext);
            gymAttendenceRepository = new GymAttendenceRepository(DbContext);

            attendanceController = new GymAttendanceController(gymRepository, gymAttendenceRepository);
        }

        [Fact]
        public async Task CanAddAttendanceCanGetAttendance() {
            bool added = await gymRepository.addGym(new Gym {
                GymId = 15,
                GymName = "New",
                GymBranch = "Gym"
            });

            Assert.True(added);

            GymAttendenceAddModel request = new GymAttendenceAddModel {
                gymid = 15,
                time = "15:00",
                day = 1,
                month = 9,
                year = 2020
            };

            var response = await attendanceController.addAttendence(request);

            Assert.IsType<OkObjectResult>(response.Result);

            var response1 = await attendanceController.getAttendence(15);

            Assert.NotEmpty(response1.Value);
        }
    }
}
