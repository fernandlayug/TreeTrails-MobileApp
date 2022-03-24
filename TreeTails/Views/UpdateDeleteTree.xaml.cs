using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeTails.Model;
using TreeTails.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TreeTails.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateDeleteTree : ContentPage
    {
        DBFirebase services;

        public UpdateDeleteTree(TreeModel TreeModel)
        {
            InitializeComponent();
            BindingContext = TreeModel;
            services = new DBFirebase();
        }

        public async void BtnDelete(object sender, EventArgs e)
        {
            await services.DeleteTreeModel(TreeCode.Text,InitialIdentification.Text,Notes.Text, GPSCoordinates.Text,Location.Text,LandmarkOfLocation.Text,Height.Text,DMB.Text,TrunkWounds.Text);
            await Navigation.PushAsync(new ListTree());
        }

        public async void BtnUpdate(object sender, EventArgs e)
        {
            await services.UpdateTreeModel(TreeCode.Text, InitialIdentification.Text, Notes.Text, GPSCoordinates.Text, Location.Text, LandmarkOfLocation.Text, Height.Text, DMB.Text, TrunkWounds.Text);
            await Navigation.PushAsync(new ListTree());
        }
        public async void Back(object sender, EventArgs e)
        {
            
           await Navigation.PushAsync(new ListTree());
        }
    }
}