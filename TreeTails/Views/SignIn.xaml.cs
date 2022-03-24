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
    [DesignTimeVisible(true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignIn : ContentPage
    {
        iAuth auth;
        public SignIn()
        {
            InitializeComponent();
            auth = DependencyService.Get<iAuth>();
        }

        async void LoginClicked(object sender, EventArgs e)
        {
            string token = await auth.LoginWithEmailAndPassword(EmailInput.Text, PasswordInput.Text);

            if (token != string.Empty)
            {
                Application.Current.MainPage = new NavigationPage(new Views.HomePage());
            }
            else
            {
                await DisplayAlert("Invalid Login Credentials", "The email or password you entered is incorrect", "OK");
            }
        }

        void SignUpClicked(object sender, EventArgs e)
        {
            var signOut = auth.SignOut();

            if (signOut)
            {
                Application.Current.MainPage = new Views.SignUp();
            }
        }
    }
}