using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Gym_Moves.Model
{
    class uSignUp
    {
       // [JsonProperty(PropertyName = "userType")]
        public int userType { get; set; }

       // [JsonProperty(PropertyName = "UserID")]
        public string UserID { get; set; }

      //  [JsonProperty(PropertyName = "FirstName")]
        public string Firstname { get; set; }

      //  [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }

     //   [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }

      //  [JsonProperty(PropertyName = "RegisteredGymFK")]
        public int RegisteredGymFK { get; set; }

    }
}