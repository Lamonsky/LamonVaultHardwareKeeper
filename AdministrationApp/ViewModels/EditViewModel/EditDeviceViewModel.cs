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
            oldItem = vm;
            if (item != null) setForeignKeys();
            Messenger.Default.Register<UserVM>(this, getChosenUser);
            Messenger.Default.Register<DeviceModelVM>(this, getDeviceModel);
            Messenger.Default.Register<ManufacturerVM>(this, getManufacturer);
            Messenger.Default.Register<StatusVM>(this, getStatus);
            Messenger.Default.Register<DeviceTypeVM>(this, getDeviceType);
        }
        public override async void Save()
        {
            item.ModifiedAt = DateTime.Now;
            item.ModifiedBy = GlobalData.UserId;
            EditSaveLogs(oldItem, item);
            await RequestHelper.SendRequestAsync(URLs.DEVICE_ID.Replace("{id}", item.Id.ToString()), HttpMethod.Put, item, GlobalData.AccessToken);
            Messenger.Default.Send("DeviceRefresh");
        }
        #endregion
        #region CommandsFunctions
        public async void setForeignKeys()
        {
            if (item.StatusId != null)
            {
                try
                {
                    StatusVM statusvm = await RequestHelper.SendRequestAsync<object, StatusVM>(
                    URLs.STATUS_ID.Replace("{id}", item.StatusId.ToString()),
                    HttpMethod.Get,
                    null,
                    GlobalData.AccessToken
                );
                    StatusName = statusvm.Name;
                }
                catch
                {
                    StatusName = "Nieaktywny status";
                }
                
            }

            if (item.Users != null)
            {
                try
                {
                    UserVM userVM = await RequestHelper.SendRequestAsync<object, UserVM>(
                    URLs.USER_ID.Replace("{id}", item.Users.ToString()),
                    HttpMethod.Get,
                    null,
                    GlobalData.AccessToken
                );
                    UserName = userVM.FirstName + " " + userVM.LastName + " " + userVM.InternalNumber + " " + userVM.Position;
                }
                catch
                {
                    UserName = "Nieaktywny użytkownik";
                }
                
            }

            if (item.DeviceType != null)
            {
                try
                {
                    DeviceTypeVM typevm = await RequestHelper.SendRequestAsync<object, DeviceTypeVM>(
                    URLs.DEVICETYPE_ID.Replace("{id}", item.DeviceType.ToString()),
                    HttpMethod.Get,
                    null,
                    GlobalData.AccessToken
                );
                    DeviceTypeName = typevm.Name;
                }
                catch
                {
                    DeviceTypeName = "Nieaktywny typ urządzenia";
                }
                
            }

            if (item.Model != null)
            {
                try
                {
                    DeviceModelVM modelvm = await RequestHelper.SendRequestAsync<object, DeviceModelVM>(
                                        URLs.DEVICEMODEL_ID.Replace("{id}", item.Model.ToString()),
                                        HttpMethod.Get,
                                        null,
                                        GlobalData.AccessToken
                                    );
                    DeviceModelName = modelvm.Name;
                }
                catch
                {
                    DeviceModelName = "Nieaktywny model urządzenia";
                }
                
            }

            if (item.Manufacturer != null)
            {
                try
                {
                    ManufacturerVM manufacturerVM = await RequestHelper.SendRequestAsync<object, ManufacturerVM>(
                    URLs.MANUFACTURER_ID.Replace("{id}", item.Manufacturer.ToString()),
                    HttpMethod.Get,
                    null,
                    GlobalData.AccessToken
                );
                    ManufacturerName = manufacturerVM.Name;
                }
                catch
                {
                    ManufacturerName = "Nieaktywny producent";
                }
                
            }

        }
        private void getStatus(StatusVM vm)
        {
            item.StatusId = vm.Id;
            StatusName = vm.Name;
        }
        private void getDeviceType(DeviceTypeVM vm)
        {
            item.DeviceType = vm.Id;
            DeviceTypeName = vm.Name;
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
                if (_ManufacturerName != value)
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
                if (_StatusName != value)
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
                if (_DeviceModelName != value)
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
                    OnPropertyChanged(() => DeviceTypeName);
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
                if (_UserName != value)
                {
                    _UserName = value;
                    OnPropertyChanged(() => UserName);
                }
            }
        }
        #endregion
    }
}
