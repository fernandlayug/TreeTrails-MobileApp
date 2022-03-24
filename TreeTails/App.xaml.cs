using System;
using TreeTails.Model;
using TreeTails.Services;
using TreeTails.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TreeTails
{
    public partial class App : Application
    {
        TreeModel treeModel = new TreeModel();
        iAuth auth;
        public App()
        {
            InitializeComponent();
            auth = DependencyService.Get<iAuth>();

            if (auth.IsSignIn())
            {
                MainPage = new NavigationPage(new Views.HomePage());
            }
            else
            {
                MainPage = new SignIn();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
