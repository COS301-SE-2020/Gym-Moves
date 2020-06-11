using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
//using Newtonsoft.Json.Serialization;
//using Newtonsoft.Json.Converters;
//using Newtonsoft.Json;
using Gym_Moves.Model;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xamarin.Essentials;
using ModernHttpClient;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Android.Preferences;

namespace Gym_Moves
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    
    public class MainActivity : AppCompatActivity
    {
        public static MainActivity Instance;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Instance = this;
            /*
             JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
             {
                 ContractResolver = new CamelCasePropertyNamesContractResolver(),
                 Converters = { new StringEnumConverter() }
             };
            */
            Forms.SetFlags("RadioButton_Experimental");

            base.OnCreate(savedInstanceState);
            Forms.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            if (Preferences.ContainsKey("uType"))
            {
                if(Preferences.Get("uType", null) == "0" || Preferences.Get("uType", null) == "1")
                {

                    Android.Support.V4.App.Fragment mainPage = new UserPage().CreateSupportFragment(this);
                    SupportFragmentManager
                        .BeginTransaction()
                        .Replace(Resource.Id.fragment_frame_layout, mainPage)
                        .Commit();
                }
                else if (Preferences.Get("uType", null) == "2")
                {

                    Android.Support.V4.App.Fragment mainPage = new MainPage().CreateSupportFragment(this);
                    SupportFragmentManager
                        .BeginTransaction()
                        .Replace(Resource.Id.fragment_frame_layout, mainPage)
                        .Commit();
                }
            }
            else
            { 

            Android.Support.V4.App.Fragment mainPage = new sign().CreateSupportFragment(this);
            SupportFragmentManager
                .BeginTransaction()
                .Replace(Resource.Id.fragment_frame_layout, mainPage)
                .Commit();
        }
      
        }

        public void Logout()
        {
            //Preferences.Clear();
            Android.Support.V4.App.Fragment mainPage = new sign().CreateSupportFragment(this);
            SupportFragmentManager
                .BeginTransaction()
                .Replace(Resource.Id.fragment_frame_layout, mainPage)
                .Commit();
        }

        public void LoginUser(String username, String uPass)
        {

            valUser(username, uPass);

        }

        private async void valUser(String u, String p)
        {



    
    try
    {

        var req = new HttpRequestMessage();
        req.RequestUri = new Uri("https://gymmoveswebapi.azurewebsites.net/api/login/user");
        req.Method = HttpMethod.Post;
        req.Headers.Add("Accept", "application/json");
                var cont = new Object();
                if (Preferences.ContainsKey("uType"))
                {
                    if (Preferences.Get("uType", null) == "0" || Preferences.Get("uType", null) == "2")
                    {
                        cont = new LoginReq() { Password = p, UserID = u };
                    }else
                    {
                        req.RequestUri = new Uri("https://gymmoveswebapi.azurewebsites.net/api/login/instructor");
                        cont = new iLogin() { Password = p, InstructorID = u };
                    }
                }else
                {
                    Toast.MakeText(this, "error", ToastLength.Long).Show();
                }
        var json = JsonSerializer.Serialize(cont);


        var content = new StringContent(json, Encoding.UTF8, "application/json");


        req.Content = content;

        String ou = req.Content.ToString();
        //Toast.MakeText(this, ou , ToastLength.Long).Show();

        var client = new HttpClient();
        HttpResponseMessage res = await client.SendAsync(req);

        HttpContent resContent = res.Content;
        var data = await resContent.ReadAsStringAsync();
        var resp = JsonSerializer.Deserialize<APIResponse>(data);
       //var responseObj = new APIResponse();

        if(resp.status== "successful")
        {
                    if (Preferences.Get("uType", null) == "0" || Preferences.Get("uType", null) == "1")
                    {

                        Android.Support.V4.App.Fragment mainPage = new UserPage().CreateSupportFragment(this);
                        SupportFragmentManager
                            .BeginTransaction()
                            .Replace(Resource.Id.fragment_frame_layout, mainPage)
                            .Commit();
                    }
                    else if (Preferences.Get("uType", null) == "2")
                    {

                        Android.Support.V4.App.Fragment mainPage = new MainPage().CreateSupportFragment(this);
                        SupportFragmentManager
                            .BeginTransaction()
                            .Replace(Resource.Id.fragment_frame_layout, mainPage)
                            .Commit();
                    }
                }
                else
                {

                    Toast.MakeText(this, resp.status + ": Please make sure your gymID and password are correct", ToastLength.Long).Show();
                }
    }
    catch (Exception ex)
    {
        Toast.MakeText(this, ex.StackTrace, ToastLength.Long).Show();

    }
    
}

        public void goToLogIn()
        {
            Android.Support.V4.App.Fragment userlogin = new LoginPage
            {
            }.CreateSupportFragment(this);
            SupportFragmentManager
                .BeginTransaction()
                .AddToBackStack(null)
                .Replace(Resource.Id.fragment_frame_layout, userlogin)
                .Commit();
        }

        public void goToSignUp()
        {
            Android.Support.V4.App.Fragment usersignup = new sign
            {
            }.CreateSupportFragment(this);
            SupportFragmentManager
                .BeginTransaction()
                .AddToBackStack(null)
                .Replace(Resource.Id.fragment_frame_layout, usersignup)
                .Commit();
        }

        public void viewClass()
        {
            Android.Support.V4.App.Fragment vClasses = new viewClasses
            {
            }.CreateSupportFragment(this);
            SupportFragmentManager
                .BeginTransaction()
                .AddToBackStack(null)
                .Replace(Resource.Id.fragment_frame_layout, vClasses)
                .Commit();
        }

        public void addClass()
        {
            Android.Support.V4.App.Fragment aClasses = new addClass
            {
            }.CreateSupportFragment(this);
            SupportFragmentManager
                .BeginTransaction()
                .AddToBackStack(null)
                .Replace(Resource.Id.fragment_frame_layout, aClasses)
                .Commit();
        }


        private async void valSignUp(String uname, String surname, String memID, String pass, int utype)
        {

            try
            {
                //Toast.MakeText(this, "Gym Member", ToastLength.Short).Show();
                var req = new HttpRequestMessage();
                //req.RequestUri = new Uri("https://wheatley.cs.up.ac.za/u18059903/testapi.php");
                req.RequestUri = new Uri("https://gymmoveswebapi.azurewebsites.net/api/signup/user");
                req.Method = HttpMethod.Post;
                req.Headers.Add("Accept", "application/json");
                String tp = "";

                //String credentials = "u18059903:Space4321";
                //req.Headers.Add("Authorization", credentials);
                var cont = new Object();

                switch (utype)
                {
                    case 0 :
                        {
                            cont = new uSignUp() { userType = 0, UserID = memID, Firstname = uname, LastName = surname, Password = pass, RegisteredGymFK = 1 };
                            tp = "0";
                            //Preferences.Set("uType", "0");
                        }
                        break;

                    case 1:
                        {
                            cont = new iSignUp() { InstructorID = memID, Firstname = uname, LastName = surname, Password = pass, RegisteredGymFK = 1 };
                            req.RequestUri = new Uri("https://gymmoveswebapi.azurewebsites.net/api/signup/instructor");
                            tp = "1";
                            //Preferences.Set("uType", "1");
                        }
                        break;
                    case 2:
                        {
                            cont = new uSignUp() { userType = 2, UserID = memID, Firstname = uname, LastName = surname, Password = pass, RegisteredGymFK = 1 };
                            tp = "2";
                            //Preferences.Set("uType", "2");
                        }
                        break;
                }
                var json = JsonSerializer.Serialize(cont);


                var content = new StringContent(json, Encoding.UTF8, "application/json");
                req.Content = content;

                String ou = req.Content.ToString();
                //Toast.MakeText(this, ou, ToastLength.Long).Show();

                var client = new HttpClient();
                HttpResponseMessage res = await client.SendAsync(req);

                HttpContent resContent = res.Content;
                var data = await resContent.ReadAsStringAsync();
                var resp = JsonSerializer.Deserialize<APIResponse>(data);
                //var responseObj = new APIResponse();

                if (resp.status == "successful")
                {
                    Preferences.Set("uType", tp);
                    if (Preferences.Get("uType", null) == "0" || Preferences.Get("uType", null) == "1")
                    {

                        Android.Support.V4.App.Fragment mainPage = new UserPage().CreateSupportFragment(this);
                        SupportFragmentManager
                            .BeginTransaction()
                            .Replace(Resource.Id.fragment_frame_layout, mainPage)
                            .Commit();
                    }
                    else if (Preferences.Get("uType", null) == "2")
                    {

                        Android.Support.V4.App.Fragment mainPage = new MainPage().CreateSupportFragment(this);
                        SupportFragmentManager
                            .BeginTransaction()
                            .Replace(Resource.Id.fragment_frame_layout, mainPage)
                            .Commit();
                    }
                }
                else
                {

                    Toast.MakeText(this, resp.message, ToastLength.Long).Show();
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.StackTrace, ToastLength.Long).Show();

            }

        }
        

        public void signup(String uname, String surname,String memID, String pass, int ut)
        {
            valSignUp(uname, surname, memID, pass, ut);
        }

        public void ToastM(String mess)
        {
            System.Diagnostics.Debug.WriteLine(mess);
            var ctx = Android.App.Application.Context;

            Toast.MakeText(this, mess, ToastLength.Long).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
