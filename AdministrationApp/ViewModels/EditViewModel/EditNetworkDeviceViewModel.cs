using AdministrationApp.Helpers;
using AdministrationApp.ViewModels.NewViewModel;
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

namespace AdministrationApp.ViewModels.EditViewModel
{
    class EditNetworkDeviceViewModel : JedenViewModel<NetworkDeviceCreateEditVM>
    {
        #region Commands
        private BaseCommand _ChooseLocationCommand;
        private BaseCommand _ChooseUserCommand;
        private BaseCommand _ChooseStatusCommand;
        private BaseCommand _ChooseManufacturerCommand;
        private BaseCommand _ChooseNetworkDeviceModelCommand;
        private BaseCommand _ChooseNetworkDeviceTypeCommand;
        private BaseCommand _ChooseRackCabinetCommand;

        public ICommand ChooseRackCabinetCommand
        {
            get
            {
                if (_ChooseRackCabinetCommand == null)
                {
                    _ChooseRackCabinetCommand = new BaseCommand(() => Messenger.Default.Send("ChooseRackCabinet"));
                }
                return _ChooseRackCabinetCommand;
            }
        }
        public ICommand ChooseNetworkDeviceTypeCommand
        {
            get
            {
                if (_ChooseNetworkDeviceTypeCommand == null)
                {
                    _ChooseNetworkDeviceTypeCommand = new BaseCommand(() => Messenger.Default.Send("ChooseNetworkDeviceType"));
                }
                return _ChooseNetworkDeviceTypeCommand;
            }
        }
        public ICommand ChooseNetworkDeviceModelCommand
        {
            get
            {
                if (_ChooseNetworkDeviceModelCommand == null)
                {
                    _ChooseNetworkDeviceModelCommand = new BaseCommand(() => Messenger.Default.Send("ChooseNetworkDeviceModel"));
                }
                return _ChooseNetworkDeviceModelCommand;
            }
        }
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
        public EditNetworkDeviceViewModel(NetworkDeviceCreateEditVM vm) : base("Edycja urządzenia sieciowego")
        {
            item = vm;
            oldItem = vm;
            if (item != null) setForeignKeys();
            Messenger.Default.Register<LocationVM>(this, getChosenLokacja);
            Messenger.Default.Register<UserVM>(this, getChosenUser);
            Messenger.Default.Register<StatusVM>(this, getStatus);
            Messenger.Default.Register<ManufacturerVM>(this, getManufacturer);
            Messenger.Default.Register<RackCabinetVM>(this, getRackCabinet);
            Messenger.Default.Register<NetworkDeviceTypeVM>(this, getType);
            Messenger.Default.Register<NetworkDeviceModelVM>(this, getModel);

        }
        public override async void Save()
        {
            item.ModifiedAt = DateTime.Now;
            item.ModifiedBy = GlobalData.UserId;
            EditSaveLogs(oldItem, item);
            await RequestHelper.SendRequestAsync(URLs.NETWORKDEVICE_ID.Replace("{id}", item.Id.ToString()), HttpMethod.Put, item, GlobalData.AccessToken);
            Messenger.Default.Send("NetworkDeviceRefresh");
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

            if (item.LocationId != null)
            {
                try
                {
                    LocationVM locationVM = await RequestHelper.SendRequestAsync<object, LocationVM>(
                        URLs.LOCATION_ID.Replace("{id}", item.LocationId.ToString()),
                        HttpMethod.Get,
                        null,
                        GlobalData.AccessToken
                    );
                    LokacjaName = locationVM.Name;
                }
                catch
                {
                    LokacjaName = "Nieaktywna lokalizacja";
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
                    NetworkDeviceTypeVM ctypevm = await RequestHelper.SendRequestAsync<object, NetworkDeviceTypeVM>(
                        URLs.NETWORKDEVICETYPE_ID.Replace("{id}", item.DeviceType.ToString()),
                        HttpMethod.Get,
                        null,
                        GlobalData.AccessToken
                    );
                    NetworkDeviceTypeName = ctypevm.Name;
                }
                catch
                {
                    NetworkDeviceTypeName = "Nieaktywny typ urządzenia sieciowego";
                }
            }

            if (item.Model != null)
            {
                try
                {
                    NetworkDeviceModelVM cmodelvm = await RequestHelper.SendRequestAsync<object, NetworkDeviceModelVM>(
                        URLs.NETWORKDEVICEMODEL_ID.Replace("{id}", item.Model.ToString()),
                        HttpMethod.Get,
                        null,
                        GlobalData.AccessToken
                    );
                    NetworkDeviceModelName = cmodelvm.Name;
                }
                catch
                {
                    NetworkDeviceModelName = "Nieaktywny model urządzenia sieciowego";
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
        private void getRackCabinet(RackCabinetVM vm)
        {
            item.Rack = vm.Id;
            RackCabinetName = vm.Manufacturer + " " + vm.Model + " " + vm.Location + " " + vm.InventoryNumber;
        }
        private void getChosenUser(UserVM vM)
        {
            item.Users = vM.Id;
            UserName = vM.FirstName + " " + vM.LastName + " " + vM.InternalNumber + " " + vM.Position;
        }
        private void getChosenLokacja(LocationVM vM)
        {
            item.LocationId = vM.Id;
            LokacjaName = vM.Name;
        }
        private void getManufacturer(ManufacturerVM vm)
        {
            item.Manufacturer = vm.Id;
            ManufacturerName = vm.Name;
        }
        private void getModel(NetworkDeviceModelVM vM)
        {
            item.Model = vM.Id;
            NetworkDeviceModelName = vM.Name;
        }
        private void getType(NetworkDeviceTypeVM vm)
        {
            item.DeviceType = vm.Id;
            NetworkDeviceTypeName = vm.Name;
        }
        #endregion
        #region Dane
        private string _RackCabinetName;
        public string RackCabinetName
        {
            get
            {
                return _RackCabinetName;
            }
            set
            {
                if (_RackCabinetName != value)
                {
                    _RackCabinetName = value;
                    OnPropertyChanged(() => RackCabinetName);
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
        private string _NetworkDeviceModelName;
        public string NetworkDeviceModelName
        {
            get
            {
                return _NetworkDeviceModelName;
            }
            set
            {
                if (_NetworkDeviceModelName != value)
                {
                    _RackCabinetName = value;
                    OnPropertyChanged(() => NetworkDeviceModelName);
                }

            }
        }
        private string _NetworkDeviceTypeName;
        public string NetworkDeviceTypeName
        {
            get
            {
                return _NetworkDeviceTypeName;
            }
            set
            {
                if (_NetworkDeviceTypeName != value)
                {
                    _NetworkDeviceTypeName = value;
                    OnPropertyChanged(() => NetworkDeviceTypeName);
                }

            }
        }
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
                if (_LokacjaName != value)
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
