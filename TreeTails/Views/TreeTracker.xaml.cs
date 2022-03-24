using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace TreeTails.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TreeTracker : ContentPage
    {
        public TreeTracker()
        {
            InitializeComponent();
            GetAddressCommand = new Command(async () => await OnGetAddress());
            GetPositionCommand = new Command(async () => await OnGetPosition());
            BindingContext = this;
        }

        private async void ButtonOpenCords_Clicked(object sender, EventArgs e)
        {
            if (!double.TryParse(EntryLatitude.Text, out double lat))
                return;

            if (!double.TryParse(EntryLongitude.Text, out double lng))
                return;

            await Map.OpenAsync(lat, lng, new MapLaunchOptions
            {
                Name = EntryNotes.Text,
                NavigationMode = NavigationMode.None
            });
        }

        private async void ButtonBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListTree());
        }

        string lat = "";
        string lon = "";
        string address = "Santa Rita, Pampanga, Philippines";
        string geocodeAddress;
        string geocodePosition;

        public ICommand GetAddressCommand { get; }
        public ICommand GetPositionCommand { get; }

        public string Latitude
        {
            get => lat;
            set => SetProperty(ref lat, value);
        }

        public string Longitude
        {
            get => lon;
            set => SetProperty(ref lon, value);
        }

        public string GeocodeAddress
        {
            get => geocodeAddress;
            set => SetProperty(ref geocodeAddress, value);
        }

        public string Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }

        public string GeocodePosition
        {
            get => geocodePosition;
            set => SetProperty(ref geocodePosition, value);
        }

        //Function that get the position

        async Task OnGetPosition()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                var locations = await Geocoding.GetLocationsAsync(Address);
                Location location = locations.FirstOrDefault();
                if (location == null)
                {
                    GeocodePosition = "Unable to detect location";
                }
                else
                {
                    GeocodePosition =
                        $"{nameof(location.Latitude)}: {location.Latitude}\n" +
                        $"{nameof(location.Longitude)}: {location.Longitude}\n";
                }
            }
            catch (Exception ex)
            {
                GeocodePosition = $"Unable to detect location: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        //Function Get Address
        async Task OnGetAddress()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                double.TryParse(EntryLatitude.Text, out var lt);
                double.TryParse(EntryLongitude.Text, out var ln);
                var placemarks = await Geocoding.GetPlacemarksAsync(lt, ln);
                Placemark placemark = placemarks.FirstOrDefault();
                if (placemark == null)
                {
                    GeocodeAddress = "Unable to detect placemark";
                }
                else
                {
                    GeocodeAddress =
                        $"{placemark.AdminArea}, " +
                        $"{placemark.Locality}, " +
                        $"{placemark.CountryName}";
                }
            }
            catch (Exception ex)
            {
                GeocodeAddress = $"Unable to detect placemarks: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        //SetProperty
        protected virtual bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null, Func<T, T, bool> validateValue = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            if (validateValue != null && !validateValue(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}