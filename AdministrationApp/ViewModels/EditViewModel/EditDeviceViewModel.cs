using AdministrationApp.Helpers;
using AdministrationApp.ViewModels.NewViewModel;
using Data;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Windows.Input;

namespace AdministrationApp.ViewModels.EditViewModel
{
    class EditDeviceViewModel : JedenViewModel<DevicesCreateEditVM>
    {
        #region Commands
        private BaseCommand _ChooseUserCommand;
        private BaseCommand _ChooseStatusCommand;
        private BaseCommand _ChooseDeviceModelCommand;
        private BaseCommand _ChooseDeviceTypeCommand;
        private BaseCommand _ChooseManufacturerCommand;

        public ICommand ChooseManufacturerCommand
        {
            get
            {
                if (_ChooseManufacturerCommand == null)
                {
                    _ChooseManufacturerCommand = new BaseCommand(() => Messenger.Default.Send("ChooseManufacturer"));
                }
                return _ChooseManufacturerCommand;
            }
        }
        public ICommand ChooseDeviceTypeCommand
        {
            get
            {
                if (_ChooseDeviceTypeCommand == null)
                {
                    _ChooseDeviceTypeCommand = new BaseCommand(() => Messenger.Default.Send("ChooseDeviceType"));
                }
                return _ChooseDeviceTypeCommand;
            }
        }
        public ICommand ChooseDeviceModelCommand
        {
            get
            {
                if (_ChooseDeviceModelCommand == null)
                {
                    _ChooseDeviceModelCommand = new BaseCommand(() => Messenger.Default.Send("ChooseDeviceModel"));
                }
                return _ChooseDeviceModelCommand;
            }
        }
        public ICommand ChooseStatusCommand
        {
            get
            {
                if (_ChooseStatusCommand == null)
                {
                    _ChooseStatusCommand = new BaseCommand(() => Messenger.Default.Send("ChooseStatus"));
                }
                return _ChooseStatusCommand;
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
        public EditDeviceViewModel(DevicesCreateEditVM vm) : base("Edycja urządzenia")
        {
            item = vm;
            setForeignKeys();
            Messenger.Default.Register<UserVM>(this, getChosenUser);
            Messenger.Default.Register<DeviceModelVM>(this, getDeviceModel);
            Messenger.Default.Register<ManufacturerVM>(this, getManufacturer);
            Messenger.Default.Register<StatusVM>(this, getStatus);

        }
        public override async void Save()
        {
            await RequestHelper.SendRequestAsync(URLs.DEVICE_ID.Replace("{id}", item.Id.ToString()), HttpMethod.Put, item, null);
            Messenger.Default.Send("DeviceRefresh");
        }
        #endregion
        #region CommandsFunctions
        public async void setForeignKeys()
        {
            StatusVM statusvm = await RequestHelper.SendRequestAsync<object, StatusVM>(URLs.STATUS_ID.Replace("{id}", item.StatusId.ToString()), HttpMethod.Get, null, null);
            UserVM userVM = await RequestHelper.SendRequestAsync<object, UserVM>(URLs.USER_ID.Replace("{id}", item.Users.ToString()), HttpMethod.Get, null, null);
            DeviceTypeVM typevm = await RequestHelper.SendRequestAsync<object, DeviceTypeVM>(URLs.DEVICETYPE_ID.Replace("{id}", item.DeviceType.ToString()), HttpMethod.Get, null, null);
            DeviceModelVM modelvm = await RequestHelper.SendRequestAsync<object, DeviceModelVM>(URLs.DEVICEMODEL_ID.Replace("{id}", item.Model.ToString()), HttpMethod.Get, null, null);
            ManufacturerVM manufacturerVM = await RequestHelper.SendRequestAsync<object, ManufacturerVM>(URLs.MANUFACTURER_ID.Replace("{id}", item.Manufacturer.ToString()), HttpMethod.Get, null, null);
            StatusName = statusvm.Name;
            UserName = userVM.FirstName + " " + userVM.LastName + " " + userVM.InternalNumber + " " + userVM.Position;
            DeviceTypeName = typevm.Name;
            ManufacturerName = manufacturerVM.Name;
            DeviceModelName = modelvm.Name;
        }
        private void getStatus(StatusVM vm)
        {
            item.StatusId = vm.Id;
            StatusName = vm.Name;
        }
        private void getChosenUser(UserVM vM)
        {
            item.Users = vM.Id;
            UserName = vM.FirstName + " " + vM.LastName + " " + vM.InternalNumber + " " + vM.Position;
        }
        private void getManufacturer(ManufacturerVM vm)
        {
            item.Manufacturer = vm.Id;
            ManufacturerName = vm.Name;
        }
        private void getDeviceModel(DeviceModelVM vm)
        {
            item.Model = vm.Id;
            DeviceModelName = vm.Name;
        }

        #endregion
        #region Dane
        private string _ManufacturerName;
        public string ManufacturerName
        {
            get
            {
                return _ManufacturerName;
            }
            set
            {
                if (_ManufacturerName == null)
                {
                    _ManufacturerName = value;
                    OnPropertyChanged(() => ManufacturerName);
                }

            }
        }
        private string _StatusName;
        public string StatusName
        {
            get
            {
                return _StatusName;
            }
            set
            {
                if (_StatusName == null)
                {
                    _StatusName = value;
                    OnPropertyChanged(() => StatusName);
                }

            }
        }
        public int Id
        {
            get
            {
                return item.Id;
            }
        }
        private string _DeviceModelName;
        public string DeviceModelName
        {
            get
            {
                return _DeviceModelName;
            }
            set
            {
                if (_DeviceModelName == null)
                {
                    _DeviceModelName = value;
                    OnPropertyChanged(() => DeviceModelName);
                }

            }
        }
        private string _DeviceTypeName;
        public string DeviceTypeName
        {
            get
            {
                return _DeviceTypeName;
            }
            set
            {
                if (_DeviceTypeName != value)
                {
                    _DeviceTypeName = value;
                    OnPropertyChanged(() => _DeviceTypeName);
                }

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
    }
}
