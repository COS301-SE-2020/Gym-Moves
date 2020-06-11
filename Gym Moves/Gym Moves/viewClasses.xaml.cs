using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Android.Widget;
using System.Collections.ObjectModel;

namespace Gym_Moves
{
    [DesignTimeVisible(true)]

    class atGym
    {
        public int classTypeID { get; set; }
        public string className { get; set; }
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
    class Response
    {
        public int classID { get; set; }
        public int classTypeFK { get; set; }
        public classType classType { get; set; }

        public string instructorIDFK { get; set; }
        public string instructor { get; set; }
        public int atGymFK { get; set; }
        public int registeredCount { get; set; }
        public string students { get; set; }
        public classTime classTime { get; set; }
    public int maxCapacity { get; set; }
    // public atGym { get; set; } this is against Xamarin naming rules - needs to be changed on API side
   // public classTime { get; set; } this too

    
    }
    public partial class viewClasses : ContentPage
    {
        TableView myView;
        TableRoot myRoot;
        ObservableCollection<String> myData = new ObservableCollection<String>();
        public async void MakeGet()
        {
            MyListView.ItemsSource = myData;
            var client = new HttpClient();
            Uri uri = new Uri(string.Format("https://gymmoveswebapi.azurewebsites.net/api/class/findByGym?gymid=1", string.Empty));

            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                //MainActivity.Instance.ToastM(content);

                ObservableCollection<Response> data = JsonConvert.DeserializeObject<ObservableCollection<Response>>(content);
                //String st = new String[];
                List < Label > st = new List<Label>();
                //myData.Add("test");
                string ct = "Spinning";
                foreach (Response X in data)
                {
                    if (X.instructorIDFK == "9")
                        ct = "Aerobics";
                    else
                        ct = "Spinning";

                    myData.Add("Class " + ct  + " Time: " + X.classTime.time +" Spots Left: "+   X.registeredCount); // this is because of the naming issue
                   
                }
                


                //MyListView.ItemsSource = myData;
                /*
                foreach (Response X in myData)
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
                    

                    var template = new DataTemplate(typeof(TextCell));
                    template.SetBinding(TextCell.TextProperty, "FullName");
                    template.SetBinding(TextCell.DetailProperty, "Number");
                    MyListView.ItemTemplate = template;

                }
            */

            }
            //return myData;

        }
        public viewClasses()
        {

            InitializeComponent();
            /*
            myView = new TableView();
            myView.RowHeight = 50;
            myRoot = new TableRoot();
            myView.Root = myRoot;
            */
            //ListView myListView = MyListView;

            MakeGet();
        }


    }
}
