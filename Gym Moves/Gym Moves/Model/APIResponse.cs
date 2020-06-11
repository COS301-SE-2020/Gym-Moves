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
    class APIResponse
    {
        public string status { get; set; }

        public string user { get; set; }

        public String message { get; set; }



    }
}