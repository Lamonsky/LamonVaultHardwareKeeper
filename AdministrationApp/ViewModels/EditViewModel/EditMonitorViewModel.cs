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
    public class EditMonitorViewModel : JedenViewModel<MonitorsCreateEditVM>
    {
        #region Commands
        private BaseCommand _ChooseLocationCommand;
        private BaseCommand _ChooseUserCommand;
        private BaseCommand _ChooseStatusCommand;
        private BaseCommand _ChooseManufacturerCommand;
        private BaseCommand _ChooseMonitorModelCommand;
        private BaseCommand _ChooseMonitorTypeCommand;

        public ICommand ChooseMonitorTypeCommand
        {
            get
            {
                if (_ChooseMonitorTypeCommand == null)
                {
                    _ChooseMonitorTypeCommand = new BaseCommand(() => Messenger.Default.Send("ChooseMonitorType"));
                }
                return _ChooseMonitorTypeCommand;
            }
        }
        public ICommand ChooseMonitorModelCommand
        {
            get
            {
                if (_ChooseMonitorModelCommand == null)
                {
                    _ChooseMonitorModelCommand = new BaseCommand(() => Messenger.Default.Send("ChooseMonitorModel"));
                }
                return _ChooseMonitorModelCommand;
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
        public EditMonitorViewModel(MonitorsCreateEditVM vm) : base("Edycja monitora")
        {
            item = vm;
            oldItem = vm;
            setForeignKeys();
            Messenger.Default.Register<LocationVM>(this, getChosenLokacja);
            Messenger.Default.Register<UserVM>(this, getChosenUser);
            Messenger.Default.Register<ManufacturerVM>(this, getManufacturer);
            Messenger.Default.Register<StatusVM>(this, getStatus);
            Messenger.Default.Register<MonitorModelVM>(this, getMonitorModel);
            Messenger.Default.Register<MonitorTypeVM>(this, getMonitorType);

        }
        public override async void Save()
        {
            item.ModifiedAt = DateTime.Now;
            item.ModifiedBy = GlobalData.UserId;
            EditSaveLogs(oldItem, item);
            await RequestHelper.SendRequestAsync(URLs.MONITORS_ID.Replace("{id}", item.Id.ToString()), HttpMethod.Put, item, GlobalData.AccessToken);
            Messenger.Default.Send("MonitorRefresh");
        }
        #endregion
        #region CommandsFunctions
        public async void setForeignKeys()
        {
            StatusVM statusvm = await RequestHelper.SendRequestAsync<object, StatusVM>(URLs.STATUS_ID.Replace("{id}", item.StatusId.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            LocationVM locationVM = await RequestHelper.SendRequestAsync<object, LocationVM>(URLs.LOCATION_ID.Replace("{id}", item.LocationId.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            UserVM userVM = await RequestHelper.SendRequestAsync<object, UserVM>(URLs.USER_ID.Replace("{id}", item.Users.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            MonitorTypeVM ctypevm = await RequestHelper.SendRequestAsync<object, MonitorTypeVM>(URLs.MONITORTYPE_ID.Replace("{id}", item.MonitorType.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            MonitorModelVM cmodelvm = await RequestHelper.SendRequestAsync<object, MonitorModelVM>(URLs.MONITORMODEL_ID.Replace("{id}", item.Model.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            ManufacturerVM manufacturerVM = await RequestHelper.SendRequestAsync<object, ManufacturerVM>(URLs.MANUFACTURER_ID.Replace("{id}", item.Manufacturer.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            StatusName = statusvm.Name;
            LokacjaName = locationVM.Name;
            UserName = userVM.FirstName + " " + userVM.LastName + " " + userVM.InternalNumber + " " + userVM.Position;
            MonitorTypeName = ctypevm.Name;
            ManufacturerName = manufacturerVM.Name;
            MonitorModelName = cmodelvm.Name;
        }
        private void getMonitorType(MonitorTypeVM vm)
        {
            item.MonitorType = vm.Id;
            MonitorTypeName = vm.Name;
        }
        private void getMonitorModel(MonitorModelVM vm)
        {
            item.Model = vm.Id;
            MonitorModelName = vm.Name;
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

        private void getChosenLokacja(LocationVM vM)
        {
            item.LocationId = vM.Id;
            LokacjaName = vM.Name;
        }

        #endregion
        #region Dane
        private string _MonitorTypeName;
        public string MonitorTypeName
        {
            get
            {
                return _MonitorTypeName;
            }
            set
            {
                if (_MonitorTypeName != value)
                {
                    _MonitorTypeName = value;
                    OnPropertyChanged(() => MonitorTypeName);
                }

            }
        }
        private string _MonitorModelName;
        public string MonitorModelName
        {
            get
            {
                return _MonitorModelName;
            }
            set
            {
                if (_MonitorModelName != value)
                {
                    _MonitorModelName = value;
                    OnPropertyChanged(() => MonitorModelName);
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
        public int? MonitorType
        {
            get
            {
                return item.MonitorType;
            }
            set
            {
                item.MonitorType = value;
                OnPropertyChanged(() => MonitorType);
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
