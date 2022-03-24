using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeTails.ViewModel;
using TreeTails.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TreeTails.Services;
using System.Collections.ObjectModel;
using Xamarin.Essentials;

namespace TreeTails.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTree : ContentPage
    {
        DBFirebase services;
        public AddTree(TreeModel TreeModel)
        {
            InitializeComponent();
            BindingContext = TreeModel;
            services = new DBFirebase();
        }

        public async void BtnAdd(object sender, EventArgs e)
        {
            await services.AddTreeModel(TreeCode.Text, InitialIdentification.Text, Notes.Text, GPSCoordinates.Text, Location.Text, LandmarkOfLocation.Text, Height.Text, DMB.Text, TrunkWounds.Text);
            await Navigation.PushAsync(new ListTree());
        }
        public async void Back(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListTree());
        }

        private async void GetLocation(object sender, EventArgs e)
        {
            var result = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromMinutes(1)));
            GPSCoordinates.Text = $"{result.Latitude}, {result.Longitude}{Environment.NewLine}";
        }
    }
}