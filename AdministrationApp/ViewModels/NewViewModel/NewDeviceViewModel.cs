using AdministrationApp.Helpers;
using Data;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Windows.Input;

namespace AdministrationApp.ViewModels.NewViewModel
{
    class NewDeviceViewModel : JedenViewModel<DevicesCreateEditVM>
    {
        #region Commands
        private BaseCommand _ChooseUserCommand;
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
        public NewDeviceViewModel() : base("Nowe urządzenie")
        {
            item = new DevicesCreateEditVM();
            Messenger.Default.Register<UserVM>(this, getChosenUser);
        }
        public override async void Save()
        {
            await RequestHelper.SendRequestAsync(URLs.DEVICE, HttpMethod.Post, item, null);
            Messenger.Default.Send("DeviceRefresh");
        }
        #endregion
        #region CommandsFunctions
        private void getChosenUser(UserVM vM)
        {
            item.Users = vM.Id;
            UserName = vM.FirstName + " " + vM.LastName + " " + vM.InternalNumber + " " + vM.Position;
        }

        private async void getDeviceModelItems()
        {
            deviceModelComboBoxItems = await RequestHelper.SendRequestAsync<object, List<DeviceModelVM>>(URLs.DEVICEMODEL, HttpMethod.Get, null, null);

        }

        private async void getDeviceTypeItems()
        {
            deviceTypeComboBoxItems = await RequestHelper.SendRequestAsync<object, List<DeviceTypeVM>>(URLs.DEVICETYPE, HttpMethod.Get, null, null);
        }

        private async void getStatusItems()
        {
            statusComboBoxItems = await RequestHelper.SendRequestAsync<object, List<StatusVM>>(URLs.STATUS, HttpMethod.Get, null, null);
        }

        private async void getProducentItems()
        {
            manufacturerComboBoxItems = await RequestHelper.SendRequestAsync<object, List<ManufacturerVM>>(URLs.MANUFACTURER, HttpMethod.Get, null, null);
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
        private List<DeviceTypeVM> _deviceTypeComboBoxItems;
        public List<DeviceTypeVM> deviceTypeComboBoxItems
        {
            get
            {
                //jak lista....
                if (_deviceTypeComboBoxItems == null)
                {
                    getDeviceTypeItems();
                }
                return _deviceTypeComboBoxItems;
            }
            set
            {
                _deviceTypeComboBoxItems = value;
                OnPropertyChanged(() => deviceTypeComboBoxItems);
            }
        }
        private List<DeviceModelVM> _deviceModelComboBoxItems;
        public List<DeviceModelVM> deviceModelComboBoxItems
        {
            get
            {
                //jak lista....
                if (_deviceModelComboBoxItems == null)
                {
                    getDeviceModelItems();
                }
                return _deviceModelComboBoxItems;
            }
            set
            {
                _deviceModelComboBoxItems = value;
                OnPropertyChanged(() => deviceModelComboBoxItems);
            }
        }
        #endregion
    }
}
