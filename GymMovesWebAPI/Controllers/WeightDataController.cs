using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymMovesWebAPI.Data.Mappers;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.ResponseModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace GymMovesWebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class WeightDataController : ControllerBase {
        private readonly IWeightDataRepository weightDataRepository;
        private readonly IUserRepository userRepository;

        public WeightDataController(IWeightDataRepository wdr, IUserRepository ur) {
            weightDataRepository = wdr;
            userRepository = ur;
        }

        [HttpPost("add")]
        public async Task<ActionResult> addWeight(WeightData data) {
            if (await userRepository.getUser(data.Username) == null) {
                return StatusCode(StatusCodes.Status404NotFound, "User does not exist!");
            }

            if (DateAndTime.Now.CompareTo(data.Date) < 0) {
                return StatusCode(StatusCodes.Status403Forbidden, "Invalid date!");
            }

            WeightData existing = await weightDataRepository.getWeightOnDay(data.Username, data.Date);

            if (existing == null) {
                if (await weightDataRepository.addWeight(data)) {
                    return Ok("Successfully added!");
                } else {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Some internal server error occurred while attempting to add your data!");
                }
            } else {
                existing.Weight = data.Weight;
                existing.Height = data.Height;

                if (await weightDataRepository.updateWeight(existing)) {
                    return Ok("Successfully updated existing record!");
                } else {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Some internal server error occurred while attempting to edit your data!");
                }
            }
        }

        [HttpGet("getWeight")]
        public async Task<ActionResult<WeightResponse[]>> getAllWeight(string username) {
            if (await userRepository.getUser(username) == null) {
                return StatusCode(StatusCodes.Status404NotFound, "User not found!");
            }

            WeightData[] data = await weightDataRepository.getWeight(username);
            WeightResponse[] responses = new WeightResponse[data.Length];

            if (responses.Length == 0) {
                return Ok(responses);
            } else {
                for (int i = 0; i < responses.Length; i++) {
                    responses[i] = WeightMapper.minimize(data[i]);
                }

                return Ok(responses);
            }
        }

        [HttpGet("getWeightOnDay")]
        public async Task<ActionResult<WeightResponse>> getWeightOnDay(string username, DateTime date) {
            if (await userRepository.getUser(username) == null) {
                return StatusCode(StatusCodes.Status404NotFound, "User not found!");
            }

            WeightData data = await weightDataRepository.getWeightOnDay(username, date);

            if (data == null) {
                return Ok(null);
            } else {
                return Ok(WeightMapper.minimize(data));
            }
        }
    }
}
