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
        public AllLocationsViewModelWindow(Window window) : base("Lokalizacje")
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
            switch (FilterField)
            {
                default:
                    List = new List<LocationVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Address?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.PostalCode?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.City?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Country?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.BuildingNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.RoomNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Nazwa":
                    List = new List<LocationVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Adres":
                    List = new List<LocationVM>(
                        List.Where(item =>
                            (item.Address?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Kod pocztowy":
                    List = new List<LocationVM>(
                        List.Where(item =>
                            (item.PostalCode?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Miasto":
                    List = new List<LocationVM>(
                        List.Where(item =>
                            (item.City?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Kraj":
                    List = new List<LocationVM>(
                        List.Where(item =>
                            (item.Country?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Numer budynku":
                    List = new List<LocationVM>(
                        List.Where(item =>
                            (item.BuildingNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Numer pokoju":
                    List = new List<LocationVM>(
                        List.Where(item =>
                            (item.RoomNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
            };
        }

        public override List<string> GetComboBoxFilterList()
        {
            return new List<string> 
            { 
                "Nazwa", 
                "Adres", 
                "Kod pocztowy", 
                "Miasto", 
                "Kraj", 
                "Numer budynku", 
                "Numer pokoju" 
            };
        }

        public override async void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<LocationVM>>(URLs.LOCATION, HttpMethod.Get, null, GlobalData.AccessToken);
        }

        public override void Edit()
        {
            Messenger.Default.Send(DisplayName+"Edit/"+ChosenItem.Id.ToString());
        }

        public override async void Remove()
        {
            if (ChosenItem != null) await RequestHelper.SendRequestAsync(URLs.LOCATION_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, GlobalData.AccessToken);
            load();
        }

        public override void send()
        {
            if(ChosenItem != null) Messenger.Default.Send(ChosenItem);
            _window.Close();
        }
    }
}
