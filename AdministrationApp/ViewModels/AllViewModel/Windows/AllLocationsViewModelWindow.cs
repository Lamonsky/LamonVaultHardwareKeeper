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
        private Window _window;
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
                }
            }
            get
            {
                return _ChosenLocation;
            }
        }
        #endregion
        public AllLocationsViewModelWindow(Window window) : base("Location")
        {
            Messenger.Default.Register<string>(this, open);
            _window = window;
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
            return new List<string> { "Nazwa", "Adres", "Kod pocztowy", "Miasto", "Kraj", "Numer budynku", "Numer pokoju" };
        }

        public override async void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<LocationVM>>(URLs.LOCATION, HttpMethod.Get, null, GlobalData.AccessToken);
        }

        public override void Edit()
        {
            Messenger.Default.Send(DisplayName+"Edit/"+ChosenLocation.Id.ToString());
        }

        public override async void Remove()
        {
            await RequestHelper.SendRequestAsync(URLs.LOCATION_ID.Replace("{id}", ChosenLocation.Id.ToString()), HttpMethod.Delete, ChosenLocation, GlobalData.AccessToken);
            load();
        }

        public override void send()
        {
            Messenger.Default.Send(ChosenLocation);
            _window.Close();
        }
    }
}
