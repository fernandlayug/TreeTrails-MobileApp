using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TreeTails.Model;
using TreeTails.Services;

namespace TreeTails.ViewModel
{
    class TreeListViewModel : BaseViewModel
    {
        public string TreeCode { get; set; }
        public string InitialIdentification { get; set; }
        public string Notes { get; set; }
        public string GPSCoordinates { get; set; }
        public string Location { get; set; }
        public string LandmarkOfLocation { get; set; }
        public string Height { get; set; }
        public string DMB { get; set; }
        public string TrunkWounds { get; set; }


        private DBFirebase services;

        public Command btnSaveUpdate_Clicked { get; set; }

        public ObservableCollection<TreeModel> _TreeModel = new ObservableCollection<TreeModel>();

        public ObservableCollection<TreeModel> TreeModels
        {
            get
            {
                return _TreeModel;
            }
            set
            {
                _TreeModel = value;
                OnPropertyChanged();
            }
        }

        public TreeListViewModel()
        {
            services = new DBFirebase();
            TreeModels = services.getTreeModel();
            btnSaveUpdate_Clicked = new Command(async () => await addTreeAsync(TreeCode, InitialIdentification, Notes, GPSCoordinates, Location, LandmarkOfLocation, Height, DMB, TrunkWounds));
        }

        public async Task addTreeAsync( string TreeCode, string InitialIdentification, string Notes, string GPSCoordinates, string Location, string LandmarkOfLocation, string Height, string DMB, string TrunkWounds)
        {
            await services.AddTreeModel( TreeCode, InitialIdentification, Notes, GPSCoordinates, Location, LandmarkOfLocation, Height, DMB, TrunkWounds);
        }
    }
}
