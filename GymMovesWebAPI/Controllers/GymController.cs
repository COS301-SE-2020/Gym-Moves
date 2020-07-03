/*
File Name:
    GymController.cs

Author:
    Longji

Date Created:
    02/07/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
02/07/2020      Longji          Create return all gyms function

Functional Description:
    

List of Classes:
    - GymController
*/

using GymMovesWebAPI.Data.Mappers;
using GymMovesWebAPI.Data.Models.GymModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.Models.DatabaseModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GymController : ControllerBase {
        private readonly IGymRepository gymRepository;

        public GymController(IGymRepository gr) {
            gymRepository = gr;
        }

        [HttpGet("getall")]
        public async Task<ActionResult<Gym>> listAllGyms() {
            GymModel[] results = GymMapper.mapToGymModel(await gymRepository.getAllGyms());
            return Ok(results);
        }
    }
}
