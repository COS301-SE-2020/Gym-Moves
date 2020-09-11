using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymMovesWebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GymAttendenceController : ControllerBase {
        private readonly IGymRepository gymRepository;
        private readonly IGymAttendenceRepository gymAttendenceRepository;

        public GymAttendenceController(IGymRepository gr, IGymAttendenceRepository gar) {
            gymRepository = gr;
            gymAttendenceRepository = gar;
        }


    }
}
