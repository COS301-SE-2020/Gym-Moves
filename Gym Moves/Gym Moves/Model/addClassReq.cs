using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text.Format;
using Android.Views;
using Android.Widget;

namespace Gym_Moves.Model
{
    class atGym
    {
        public int gymID { get; set; }
        public string gymName { get; set; }
        public string users { get; set; }
    }

    class classTime
    {
        public int classTimeID { get; set; }
        public string day { get; set; }
        public string time { get; set; }

    }

    class classType
    {
        public int classTypeID { get; set; }
        public string className { get; set; }

    }

    class addClassReq
    {
        //public int classID { get; set; }
        public int classTypeFK { get; set; }
       // public classType classType { get; set; }

        public string instructorIDFK { get; set; }
      //  public string instructor { get; set; }
        public int atGymFK { get; set; }
      //  public atGym atGym { get; set; }
        public int classTimeFK { get; set; }
     //   public classTime classTime { get; set; }
     //   public string student { get; set; }

        public int maxCapacity { get; set; }
        public int registeredCount { get; set; }
        // public atGym { get; set; } this is against Xamarin naming rules - needs to be changed on API side
        // public classTime { get; set; } this too


    }
}