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
using System.Windows.Media;

namespace AdministrationApp.ViewModels.AllViewModel
{
    class AllNetworkDeviceViewModel : WszystkieViewModel<NetworkDeviceVM>
    {
        private NetworkDeviceVM _ChosenNetworkDevice;
        public NetworkDeviceVM ChosenNetworkDevice
        {
            set
            {
                if (_ChosenNetworkDevice != value)
                {
                    _ChosenNetworkDevice = value;
                }
            }
            get
            {
                return _ChosenNetworkDevice;
            }
        }
        public AllNetworkDeviceViewModel() : base("Urządzenia sieciowe") {
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            if (name == "NetworkDeviceRefresh")
            {
                load();
            }
        }
        public override void Edit()
        {
            Messenger.Default.Send(DisplayName + "Edit/" + ChosenNetworkDevice.Id);
        }
        public override void Filter()
        {
            switch (FilterField)
            {
                default:
                    List = new List<NetworkDeviceVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Location?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Manufacturer?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Model?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.DeviceType?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Rack?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||                          
                            (item.SerialNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.InventoryNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Users?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Nazwa":
                    List = new List<NetworkDeviceVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Lokalizacja":
                    List = new List<NetworkDeviceVM>(
                        List.Where(item =>
                            (item.Location?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Status":
                    List = new List<NetworkDeviceVM>(
                        List.Where(item =>
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Producent":
                    List = new List<NetworkDeviceVM>(
                        List.Where(item =>
                            (item.Manufacturer?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Model":
                    List = new List<NetworkDeviceVM>(
                        List.Where(item =>
                            (item.Model?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Typ urządzenia":
                    List = new List<NetworkDeviceVM>(
                        List.Where(item =>
                            (item.DeviceType?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Szafa Rack":
                    List = new List<NetworkDeviceVM>(
                        List.Where(item =>
                            (item.Rack?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Numer seryjny":
                    List = new List<NetworkDeviceVM>(
                        List.Where(item =>
                            (item.SerialNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Numer inwentarzowy":
                    List = new List<NetworkDeviceVM>(
                        List.Where(item =>
                            (item.InventoryNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Osoba odpowiedzialna":
                    List = new List<NetworkDeviceVM>(
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
                
                "Nazwa",
                "Lokalizacja",
                "Status",
                "Producent",
                "Model",
                "Typ urządzenia",
                "Szafa Rack",
                "Numer seryjny",
                "Numer inwentarzowy",
                "Osoba odpowiedzialna"
            };
        }
        
        public async override void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<NetworkDeviceVM>>(URLs.NETWORKDEVICE, HttpMethod.Get, null, GlobalData.AccessToken);
        }
        public async override void Remove()
        {
            await RequestHelper.SendRequestAsync(URLs.NETWORKDEVICE_ID.Replace("{id}", ChosenNetworkDevice.Id.ToString()), HttpMethod.Delete, ChosenNetworkDevice, null);
            load();
        }
       
        public override void send()
        {
            throw new NotImplementedException();
        }
    }
}
