
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Gym_Moves.Model
{
    public class LoginReq
    {
        public string Password { get; set; }

        public string UserID { get; set; }
       
    }
}