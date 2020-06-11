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

namespace Gym_Moves.Model
{
    class iSignUp
    {
        public string InstructorID { get; set; }

        public string Firstname { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public int RegisteredGymFK { get; set; }
    }
}