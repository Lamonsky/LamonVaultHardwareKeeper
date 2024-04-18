using AdministrationApp.Helpers;
using Data.Computers.SelectVMs;
using Data;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using AdministrationApp.Views.AllWindows;
using System.Windows;

namespace AdministrationApp.ViewModels.AllViewModel.Windows
{
    class AllLocationsViewModelWindow : WszystkieViewModel<LocationVM>
    {
        public event EventHandler OnRequestClose;
        public void CloseWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
        #region Commands
        private LocationVM _ChosenLocation;
        public LocationVM ChosenLocation
        {
            set
            {
                if (_ChosenLocation != value)
                {
                    _ChosenLocation = value;
                    Messenger.Default.Send(_ChosenLocation);
                    OnRequestClose(this, new EventArgs());
                }
            }
            get
            {
                return _ChosenLocation;
            }
        }
        #endregion
        public AllLocationsViewModelWindow() : base("Lokalizacje")
        {
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            if (name == "LocationRefresh")
            {
                load();
            }
        }
        public override void Filter()
        {
            //throw new NotImplementedException();
        }

        public override List<string> GetComboBoxFilterList()
        {
            //throw new NotImplementedException();
            return new List<string>();
        }

        public override List<string> GetComboBoxSortList()
        {
            //throw new NotImplementedException();
            return new List<string>();
        }

        public override async void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<LocationVM>>(URLs.LOCATION, HttpMethod.Get, null, null);
        }

        public override void Sort()
        {
            //throw new NotImplementedException();
        }

        public override void Edit()
        {
            throw new NotImplementedException();
        }

        public override void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
