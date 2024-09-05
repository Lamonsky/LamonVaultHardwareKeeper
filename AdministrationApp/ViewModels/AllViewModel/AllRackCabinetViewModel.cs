using AdministrationApp.Helpers;
using Data;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdministrationApp.ViewModels.AllViewModel
{
    class AllRackCabinetViewModel : WszystkieViewModel<RackCabinetVM>
    {
        private Window _window;
        public AllRackCabinetViewModel() : base("Szafy Rack")
        {
            Messenger.Default.Register<string>(this, open);
        }
        public AllRackCabinetViewModel(Window window) : base("Szafy Rack")
        {
            _window = window;
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            if (name == "RackCabinetRefresh")
            {
                load();
            }
        }
        public override void Edit()
        {
            Messenger.Default.Send(DisplayName + "Edit/" + ChosenItem.Id);
        }

        public override void Filter()
        {
            switch(FilterField)
            {
                default:
                    List = new List<RackCabinetVM>(
                        List.Where(item =>
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Location?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.CabinetType?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Manufacturer?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Model?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Height?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.SerialNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.InventoryNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Users?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Status":
                    List = new List<RackCabinetVM>(
                        List.Where(item =>
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Lokalizacja":
                    List = new List<RackCabinetVM>(
                        List.Where(item =>
                            (item.Location?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Typ":
                    List = new List<RackCabinetVM>(
                        List.Where(item =>
                            (item.CabinetType?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Producent":
                    List = new List<RackCabinetVM>(
                        List.Where(item =>
                            (item.Manufacturer?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Model":
                    List = new List<RackCabinetVM>(
                        List.Where(item =>
                            (item.Model?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Wysokość":
                    List = new List<RackCabinetVM>(
                        List.Where(item =>
                            (item.Height?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Numer seryjny":
                    List = new List<RackCabinetVM>(
                        List.Where(item =>
                            (item.SerialNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Numer inwentarzowy":
                    List = new List<RackCabinetVM>(
                        List.Where(item =>
                            (item.InventoryNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Osoba odpowiedzialna":
                    List = new List<RackCabinetVM>(
                        List.Where(item =>
                            (item.Users?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
            }
        }

        public override List<string> GetComboBoxFilterList()
        {
            return new List<string>
            {
                
                "Status",
                "Lokalizacja",
                "Typ",
                "Producent",
                "Model",
                "Wysokość",
                "Numer seryjny",
                "Numer inwentarzowy",
                "Osoba odpowiedzialna"
            };
        }

        public async override void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<RackCabinetVM>>(URLs.RACKCABINET, HttpMethod.Get, null, GlobalData.AccessToken);
        }

        public async override void Remove()
        {
            
            await RequestHelper.SendRequestAsync(URLs.RACKCABINET_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, GlobalData.AccessToken);
            load();
        }


        public override void send()
        {
            Messenger.Default.Send(ChosenItem);
            _window.Close();
        }
    }
}
