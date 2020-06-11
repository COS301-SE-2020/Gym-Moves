using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gym_Moves
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class sign : ContentPage
    {
        public int ut = 0;
        public sign()
        {
            InitializeComponent();
        }

        private void goLogin(object sender, EventArgs e)
        {
            MainActivity.Instance.goToLogIn();
        }

        private void UserSignUp(object sender, EventArgs e)
        {
            var uname = uName.Text;
            var surname = uSN.Text;
            var memID = uID.Text;
            var pass = uPass.Text;

            if (String.IsNullOrEmpty(uname) || String.IsNullOrEmpty(surname) || String.IsNullOrEmpty(memID) || String.IsNullOrEmpty(pass))
                MainActivity.Instance.ToastM("Please enter a value for all fields.");
            else
            {
                MainActivity.Instance.signup(uname, surname, memID, pass, ut);
            }

        }

        public void OnChooseType(object sender, EventArgs e)
        {
            RadioButton obj = (RadioButton)sender;

            switch(obj.Text)
            {
                case "Manager" :  ut = 2;
                    break;
                case "Instructor": ut = 1;
                    break;
                case "Gym Member": ut = 0;
                    break;
            }
        }
    }
}