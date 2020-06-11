using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;

namespace Gym_Moves
{
    [DesignTimeVisible(false)]

    public class uatGym
    {
        public int classTypeID { get; set; }
        public string className { get; set; }
        public string users { get; set; }
    }

    public class uclassTime
    {
        public int classTimeID { get; set; }
        public string day { get; set; }
        public string time { get; set; }

    }

    public class uclassType
    {
        public int classTypeID { get; set; }
        public string className { get; set; }

    }
    public class uResponse
    {
        public int classID { get; set; }
        public int classTypeFK { get; set; }
        public uclassType classType { get; set; }

        public string instructorIDFK { get; set; }
        public string instructor { get; set; }
        public int atGymFK { get; set; }
        public int registeredCount { get; set; }
        public string students { get; set; }
        public uclassTime classTime { get; set; }
        public int maxCapacity { get; set; }
        // public atGym { get; set; } this is against Xamarin naming rules - needs to be changed on API side
        // public classTime { get; set; } this too


    }
    public partial class UsersView : ContentPage
    {
        TableView myView;
        TableRoot myRoot;

        public async Task<List<uResponse>> MakeGet() // I have no clue why this is happening
        {
            List<uResponse> myData = null;
            var client = new HttpClient();
            Uri uri = new Uri(string.Format("https://gymmoveswebapi.azurewebsites.net/api/class/findByGym?gymid=1", string.Empty));

            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                myData = JsonConvert.DeserializeObject<List<uResponse>>(content);

                foreach (uResponse X in myData)
                {
                    TableSection sec = new TableSection() { Title = X.classType.className };
                    //sec.Add(new TextCell { Text = "Class: " + X.classType.className });
                    sec.Add(new TextCell { Text = "Time: " + X.classTime.time }); // this is because of the naming issue
                    sec.Add(new TextCell { Text = "Day: " + X.classTime.day });
                    sec.Add(new TextCell { Text = "Instructor: " + X.instructor });
                    sec.Add(new TextCell { Text = "Maximum capacity: " + X.maxCapacity });
                    sec.Add(new TextCell { Text = "Registered count: " + X.registeredCount });
                    myRoot.Add(sec);

                    // System.Console.WriteLine("Time:" + X.reservation_time);
                }
            }
            return myData;

        }
         public UsersView()
        {

            InitializeComponent();

            myView = new TableView();
            myView.RowHeight = 50;
            myRoot = new TableRoot();
            myView.Root = myRoot;

            callGet();
        }

        async void callGet()
        {
            await MakeGet();
        }

        


   



    }
}
