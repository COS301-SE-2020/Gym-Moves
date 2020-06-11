using Gym_Moves.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Gym_Moves
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class addClass : ContentPage
    {
        public addClass()
        {
            InitializeComponent();
        }

        async void AddClass(object sender, EventArgs args)
        {

            var client = new HttpClient();
            var name = className.Text;
            var ctime = classTime.Time;
            var cday = classDay.Text;
            var inst = classIns.Text;
            int max = int.Parse(classMax.Text);
            int size = int.Parse(classSize.Text);
            var gym = gymName.Text;

            Uri uri = new Uri(string.Format("https://gymmoveswebapi.azurewebsites.net/api/class/add", string.Empty));

            /* var data = new addClassReq() { classID = 5, classTypeFK = 1, classType = new Model.classType() {classTypeID = 1, className = name},
                 instructorIDFK = "latestinstructor", instructor = inst, atGymFK = 1, atGym = new Model.atGym() {gymID = 1, gymName = gym, users = null},
                 classTimeFK = 1, classTime = new Model.classTime() {classTimeID = 1, day = cday, time = ctime.ToString()}, student = null, maxCapacity = max,
                 registeredCount = size
             };*/

            var data = new addClassReq() { classTypeFK = 1, instructorIDFK = "8", atGymFK = 1, classTimeFK = 1, maxCapacity = max, registeredCount = size };
            var json = JsonSerializer.Serialize(data);

            /*
            string data = @"{""classID"": ""1"",
                            ""classTypeFK"": ""1"",
                            ""classType"": ""{ ""classTypeID"": ""1"",
                                                ""className"": name}"",
                              ""instructorIDFK"": ""latestinstructor"",
                              ""instructor"": instructor,
                              ""atGymFK"": ""1"",
                              ""atGym"": ""{""gymID"" : ""1"",
                                            ""gymName"": gym,
                                            ""users"": null}"",
                              ""classTimeFK"": ""1"",0
                              ""classTime"": ""{""classTimeID"": ""1"",
                                               ""day"": day,
                                                ""time"": time}"",
                                ""student"":null,
                                ""maxCapacity"": max,
                                ""registeredCount"": size}";
            */
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await client.PostAsync(uri, content);
            HttpContent resContent = response.Content;
            var dt = await resContent.ReadAsStringAsync();
            //MainActivity.Instance.ToastM(dt);
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "The class was added succcesfully.", "OK");

            }
            else
                await DisplayAlert("Unsuccessful", "There was a problem adding the class, try again.", "OK");
            
        }
    }
}
