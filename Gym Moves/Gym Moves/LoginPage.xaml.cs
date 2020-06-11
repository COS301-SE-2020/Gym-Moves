
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Gym_Moves
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        void OnLogin(object sender, EventArgs e)
        {

            String username = mID.Text;
            String uPass = mPassword.Text;

            if (String.IsNullOrEmpty(username)|| String.IsNullOrEmpty(uPass))
                MainActivity.Instance.ToastM("Please enter a value for all fields.");

               // MainActivity.Instance.ToastM("'" + username + "' '"+ uPass+"'");

            MainActivity.Instance.LoginUser(username, uPass);

        }

        void goSignUp(object sender, EventArgs e)
        {
            MainActivity.Instance.goToSignUp();
        }
    }
}
