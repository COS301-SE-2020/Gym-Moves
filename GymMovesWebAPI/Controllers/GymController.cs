/*
File Name:
    GymController.cs

Author:
    Longji

Date Created:
    03/07/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
03/07/2020    |  Longji        |  Create return all gyms function
--------------------------------------------------------------------------------

Functional Description:
    

List of Classes:
    - GymController
*/

using GymMovesWebAPI.Data.Mappers;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.GymModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymController : ControllerBase
    {
        private readonly IGymRepository gymRepository;

        public GymController(IGymRepository gr)
        {
            gymRepository = gr;
        }

        /*
        Method Name:
            listAllGyms
        Purpose:
            This function returns all the gyms that are registered in the gym
         */
        [HttpGet("getall")]
        public async Task<ActionResult<GymModel>> listAllGyms()
        {
            GymModel[] results = GymMapper.mapToGymModel(await gymRepository.getAllGyms());
            return Ok(results);
        }
    }
}
