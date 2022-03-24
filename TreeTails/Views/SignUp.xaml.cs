using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeTails.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TreeTails.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        iAuth auth;
        public SignUp()
        {
            InitializeComponent();
            auth = DependencyService.Get<iAuth>();
        }

        async void SignUpClicked(object sender, EventArgs e)
        {
            var user = auth.SignUpWithEmailAndPassword(EmailInput.Text, PasswordInput.Text);

            if (user != null)
            {
                await DisplayAlert("Congratulations", "Your account has been successfully created.", "Ok");
                var signOut = auth.SignOut();

                if (signOut)
                {
                    Application.Current.MainPage = new SignIn();
                }
            }
            else
            {
                await DisplayAlert("Eror", "Something went wrong, please try again", "OK");
            }
        }

        private async void BackClicked_Tapped(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SignIn();
        }
    }
}