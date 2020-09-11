using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymMovesWebAPI.Data.Enums;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.RequestModels;
using GymMovesWebAPI.Data.Models.ResponseModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymMovesWebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GymAttendanceController : ControllerBase {
        private readonly IGymRepository gymRepository;
        private readonly IGymAttendenceRepository gymAttendenceRepository;

        public GymAttendanceController(IGymRepository gr, IGymAttendenceRepository gar) {
            gymRepository = gr;
            gymAttendenceRepository = gar;
        }

        [HttpPost("change")]
        public async Task<ActionResult<GymAttendanceRecord>> addAttendence(GymAttendenceAddModel record) {
            Gym gym = await gymRepository.getGymById(record.gymid);

            if (gym == null) {
                return StatusCode(StatusCodes.Status404NotFound, "Gym not found!");
            }

            GymAttendanceRecord attendance = await gymAttendenceRepository.getAttendenceRecord(record.gymid, record.time, record.day, record.month, record.year);

            if (attendance == null) {
                attendance = new GymAttendanceRecord();

                attendance.GymId = record.gymid;
                attendance.Time = record.time;
                attendance.Day = record.day;
                attendance.Month = record.month;
                attendance.Year = record.year;
                attendance.Count = 0;

                if (!await gymAttendenceRepository.addAttendence(attendance)) {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while attempting to create a new record!");
                }
            }

            attendance.Count++;

            if (await gymAttendenceRepository.updateAttendence(attendance)) {
                return Ok(attendance);
            } else {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while updating the record in the database!");
            }
        }

        [HttpGet("get")]
        public async Task<ActionResult<GymAttendanceRecord[]>> getAttendence(int gymid) {
            Gym gym = await gymRepository.getGymById(gymid);

            if (gym == null) {
                return StatusCode(StatusCodes.Status404NotFound, "Gym not found!");
            }

            return await gymAttendenceRepository.GetAttendanceRecords(gymid);
        }
    }
}
