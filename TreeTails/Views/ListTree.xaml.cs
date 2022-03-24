using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeTails.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using TreeTails.ViewModel;
using TreeTails.Services;

namespace TreeTails.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListTree : ContentPage
    {
        iAuth auth;
        public ListTree()
        {
            InitializeComponent();
            BindingContext = new TreeListViewModel();
            auth = DependencyService.Get<iAuth>();
        }
        public async void OnItemSelected(object sender, ItemTappedEventArgs args)
        {
            var Treemodels = args.Item as TreeModel;

            if (Treemodels == null)
                return;

            await Navigation.PushAsync(new UpdateDeleteTree(Treemodels));
            TreeListView.SelectedItem = null;
        }

        public async void btnAddRecord_Clicked(object sender, EventArgs e)
        {
            TreeModel tree = new TreeModel();
            await Navigation.PushAsync(new AddTree(tree));
        }

        private void SignOutButton_Clicked(object sender, EventArgs e)
        {
            var signOut = auth.SignOut();

            if (signOut)
            {
                Application.Current.MainPage = new SignIn();
            }
        }

        private async void btnTreeTracker_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TreeTracker());
        }
    }
}