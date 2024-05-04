using AdministrationApp.Helpers;
using Data;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdministrationApp.ViewModels.NewViewModel
{
    class NewNetworkDeviceViewModel : JedenViewModel<NetworkDeviceCreateEditVM>
    {
        #region Commands
        private BaseCommand _ChooseLocationCommand;
        private BaseCommand _ChooseUserCommand;

        public ICommand ChooseLocationCommand
        {
            get
            {
                if (_ChooseLocationCommand == null)
                {
                    _ChooseLocationCommand = new BaseCommand(() => Messenger.Default.Send("ChooseLocation"));
                }
                return _ChooseLocationCommand;
            }
        }
        public ICommand ChooseUserCommand
        {
            get
            {
                if (_ChooseUserCommand == null)
                {
                    _ChooseUserCommand = new BaseCommand(() => Messenger.Default.Send("ChooseUser"));
                }
                return _ChooseUserCommand;
            }
        }
        #endregion
        #region Konstruktor
        public NewNetworkDeviceViewModel() : base("Nowe urządzenie sieciowe")
        {
            item = new NetworkDeviceCreateEditVM();
            Messenger.Default.Register<LocationVM>(this, getChosenLokacja);
            Messenger.Default.Register<UserVM>(this, getChosenUser);
        }
        public override async void Save()
        {
            await RequestHelper.SendRequestAsync(URLs.NETWORKDEVICE, HttpMethod.Post, item, null);
            Messenger.Default.Send("NetworkDeviceRefresh");
        }
        #endregion
        #region CommandsFunctions
        private void getChosenUser(UserVM vM)
        {
            item.Users = vM.Id;
            UserName = vM.FirstName + " " + vM.LastName + " " + vM.InternalNumber + " " + vM.Position;
        }

        private async void getNetworkDeviceModelItems()
        {
            networkDeviceModelComboBoxItems = await RequestHelper.SendRequestAsync<object, List<NetworkDeviceModelVM>>(URLs.NETWORKDEVICEMODEL, HttpMethod.Get, null, null);

        }

        private async void getNetworkDeviceTypeItems()
        {
            networkDeviceTypeComboBoxItems = await RequestHelper.SendRequestAsync<object, List<NetworkDeviceTypeVM>>(URLs.NETWORKDEVICETYPE, HttpMethod.Get, null, null);
        }

        private async void getStatusItems()
        {
            statusComboBoxItems = await RequestHelper.SendRequestAsync<object, List<StatusVM>>(URLs.STATUS, HttpMethod.Get, null, null);
        }

        private async void getProducentItems()
        {
            manufacturerComboBoxItems = await RequestHelper.SendRequestAsync<object, List<ManufacturerVM>>(URLs.MANUFACTURER, HttpMethod.Get, null, null);
        }
        private async void getRackItems()
        {
            rackComboBoxItems = await RequestHelper.SendRequestAsync<object, List<RackCabinetVM>>(URLs.RACKCABINET, HttpMethod.Get, null, null);
        }
        private void getChosenLokacja(LocationVM vM)
        {
            item.LocationId = vM.Id;
            LokacjaName = vM.Name;
        }

        #endregion
        #region Dane
        public int Id
        {
            get
            {
                return item.Id;
            }
        }
        public string? Name
        {
            get
            {
                return item.Name;
            }
            set
            {
                item.Name = value;
                OnPropertyChanged(() => Name);
            }
        }
        public int? LocationId
        {
            get
            {
                return item.LocationId;
            }
            set
            {
                item.LocationId = value;
                OnPropertyChanged(() => LocationId);
            }
        }
        public int? StatusId
        {
            get
            {
                return item.StatusId;
            }
            set
            {
                item.StatusId = value;
                OnPropertyChanged(() => StatusId);
            }
        }
        public int? DeviceType
        {
            get
            {
                return item.DeviceType;
            }
            set
            {
                item.DeviceType = value;
                OnPropertyChanged(() => DeviceType);
            }
        }
        public int? Manufacturer
        {
            get
            {
                return item.Manufacturer;
            }
            set
            {
                item.Manufacturer = value;
                OnPropertyChanged(() => Manufacturer);
            }
        }
        public int? Model
        {
            get
            {
                return item.Model;
            }
            set
            {
                item.Model = value;
                OnPropertyChanged(() => Model);
            }
        }
        public string? SerialNumber
        {
            get
            {
                return item.SerialNumber;
            }
            set
            {
                item.SerialNumber = value;
                OnPropertyChanged(() => SerialNumber);
            }
        }
        public string? InventoryNumber
        {
            get
            {
                return item.InventoryNumber;
            }
            set
            {
                item.InventoryNumber = value;
                OnPropertyChanged(() => InventoryNumber);
            }
        }
        public int? Users
        {
            get
            {
                return item.Users;
            }
            set
            {
                item.Users = value;
                OnPropertyChanged(() => Users);
            }
        }
        public int? Rack
        {
            get
            {
                return item.Rack;
            }
            set
            {
                item.Rack = value;
                OnPropertyChanged(() => Rack);
            }
        }
        private string _LokacjaName;
        public string LokacjaName
        {
            get
            {
                return _LokacjaName;
            }
            set
            {
                if (_LokacjaName == null)
                {
                    _LokacjaName = value;
                    OnPropertyChanged(() => LokacjaName);
                }
            }
        }
        private string _UserName;
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                if (_UserName == null)
                {
                    _UserName = value;
                    OnPropertyChanged(() => UserName);
                }
            }
        }
        #endregion
        #region Foreign Keys
        private List<ManufacturerVM> _manufacturerComboBoxItems;
        public List<ManufacturerVM> manufacturerComboBoxItems
        {
            get
            {
                //jak lista....
                if (_manufacturerComboBoxItems == null)
                {
                    getProducentItems();
                }
                return _manufacturerComboBoxItems;
            }
            set
            {
                _manufacturerComboBoxItems = value;
                OnPropertyChanged(() => manufacturerComboBoxItems);
            }
        }
        private List<RackCabinetVM> _rackComboBoxItems;
        public List<RackCabinetVM> rackComboBoxItems
        {
            get
            {
                //jak lista....
                if (_rackComboBoxItems == null)
                {
                    getProducentItems();
                }
                return _rackComboBoxItems;
            }
            set
            {
                _rackComboBoxItems = value;
                OnPropertyChanged(() => rackComboBoxItems);
            }
        }
        private List<StatusVM> _statusComboBoxItems;
        public List<StatusVM> statusComboBoxItems
        {
            get
            {
                //jak lista....
                if (_statusComboBoxItems == null)
                {
                    getStatusItems();
                }
                return _statusComboBoxItems;
            }
            set
            {
                _statusComboBoxItems = value;
                OnPropertyChanged(() => statusComboBoxItems);
            }
        }
        private List<NetworkDeviceTypeVM> _networkDeviceTypeComboBoxItems;
        public List<NetworkDeviceTypeVM> networkDeviceTypeComboBoxItems
        {
            get
            {
                //jak lista....
                if (_networkDeviceTypeComboBoxItems == null)
                {
                    getNetworkDeviceTypeItems();
                }
                return _networkDeviceTypeComboBoxItems;
            }
            set
            {
                _networkDeviceTypeComboBoxItems = value;
                OnPropertyChanged(() => networkDeviceTypeComboBoxItems);
            }
        }
        private List<NetworkDeviceModelVM> _networkDeviceModelComboBoxItems;
        public List<NetworkDeviceModelVM> networkDeviceModelComboBoxItems
        {
            get
            {
                //jak lista....
                if (_networkDeviceModelComboBoxItems == null)
                {
                    getNetworkDeviceModelItems();
                }
                return _networkDeviceModelComboBoxItems;
            }
            set
            {
                _networkDeviceModelComboBoxItems = value;
                OnPropertyChanged(() => networkDeviceModelComboBoxItems);
            }
        }
        #endregion
    }
}
