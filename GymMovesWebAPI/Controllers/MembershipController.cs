/*
File Name:
    MembershipController.cs

Author:
    Longji

Date Created:
    04/08/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
04/08/2020    |  Longji        |  Add function to take csv file
--------------------------------------------------------------------------------

Functional Description:
    

List of Classes:
    - MembershipController
*/

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
    public class MembershipController : ControllerBase {
        private readonly IUserRepository userRepository;
        private readonly IGymMemberRepository memberRepository;

        public MembershipController(IUserRepository ur, IGymMemberRepository gmr) {
            userRepository = ur;
            memberRepository = gmr;
        }
    }
}
