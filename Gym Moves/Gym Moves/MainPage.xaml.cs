using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Gym_Moves
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void viewClasses(object sender, EventArgs e)
        {
            MainActivity.Instance.viewClass();
        }

        void addClasses(object sender, EventArgs e)
        {
            MainActivity.Instance.addClass();
        }

        async void onLogout(object sender, EventArgs e)
        {
            var res = await DisplayAlert("Logout", "Are you sure you want to logout?", "OK", "Cancel");
            if (res)
                MainActivity.Instance.Logout();
        }
    }
}
