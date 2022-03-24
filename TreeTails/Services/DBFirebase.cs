using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeTails.Model;

namespace TreeTails.Services
{
    class DBFirebase
    {
        FirebaseClient client;

        public DBFirebase()
        {
            client = new FirebaseClient("https://treetrails-1cf7c-default-rtdb.firebaseio.com/");
        }

        public ObservableCollection<TreeModel> getTreeModel()
        {
            var TreeModelData = client
                .Child("TreeModel")
                .AsObservable<TreeModel>()
                .AsObservableCollection();

            return TreeModelData;
        }

        public async Task AddTreeModel( string TreeCode, string InitialIdentification, string Notes, string GPSCoordinates, string Location , string LandmarkOfLocation , string Height, string DMB ,string TrunkWounds)
        {
            TreeModel em = new TreeModel()
            {
                TreeCode = TreeCode,
                InitialIdentification = InitialIdentification,
                Notes = Notes,
                GPSCoordinates = GPSCoordinates,
                Location = Location,
                LandmarkOfLocation = LandmarkOfLocation,
                Height = Height,
                DMB= DMB,
                TrunkWounds = TrunkWounds
            };

            await client
                .Child("TreeModel")
                .PostAsync(em);
        }

        public async Task DeleteTreeModel( string TreeCode, string InitialIdentification, string Notes, string GPSCoordinates, string Location, string LandmarkOfLocation, string Height, string DMB ,string TrunkWounds)
        {
            var toDeleteTreeModel = (await client
                .Child("TreeModel")
                .OnceAsync<TreeModel>()).FirstOrDefault
                (a => a.Object.TreeCode == TreeCode);
            await client
                .Child("TreeModel")
                .Child(toDeleteTreeModel.Key)
                .DeleteAsync();
        }

        public async Task UpdateTreeModel( string TreeCode, string InitialIdentification, string Notes, string GPSCoordinates, string Location, string LandmarkOfLocation, string Height, string DMB, string TrunkWounds)
        {
            var toUpdateTreeModel = (await client
                .Child("TreeModel")
                .OnceAsync<TreeModel>()).FirstOrDefault();

            TreeModel s = new TreeModel()
            {
                TreeCode = TreeCode,
                InitialIdentification = InitialIdentification,
                Notes = Notes,
                GPSCoordinates = GPSCoordinates,
                Location = Location,
                LandmarkOfLocation = LandmarkOfLocation,
                Height = Height,
                DMB = DMB,
                TrunkWounds = TrunkWounds
            };

            await client
                .Child("TreeModel")
                .Child(toUpdateTreeModel.Key)
                .PutAsync(s);
        }
    }
}
