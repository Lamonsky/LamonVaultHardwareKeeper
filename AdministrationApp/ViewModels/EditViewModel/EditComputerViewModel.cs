using AdministrationApp.Helpers;
using AdministrationApp.ViewModels.NewViewModel;
using Data;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

namespace AdministrationApp.ViewModels.EditViewModel
{
    public class EditComputerViewModel : JedenViewModel<ComputersCreateEditVM>
    {
        #region Commands
        private BaseCommand? _ChooseLocationCommand;
        private BaseCommand? _ChooseUserCommand;
        private BaseCommand? _ChooseStatusCommand;
        private BaseCommand? _ChooseComputerModelCommand;
        private BaseCommand? _ChooseComputerTypeCommand;
        private BaseCommand? _ChooseManufacturerCommand;
        private BaseCommand? _ChooseOperatingSystemCommand;

        public ICommand ChooseOperatingSystemCommand
        {
            get
            {
                if (_ChooseOperatingSystemCommand == null)
                {
                    _ChooseOperatingSystemCommand = new BaseCommand(() => Messenger.Default.Send("ChooseOperatingSystem"));
                }
                return _ChooseOperatingSystemCommand;
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
        public ICommand ChooseComputerTypeCommand
        {
            get
            {
                if (_ChooseComputerTypeCommand == null)
                {
                    _ChooseComputerTypeCommand = new BaseCommand(() => Messenger.Default.Send("ChooseComputerType"));
                }
                return _ChooseComputerTypeCommand;
            }
        }
        public ICommand ChooseComputerModelCommand
        {
            get
            {
                if (_ChooseComputerModelCommand == null)
                {
                    _ChooseComputerModelCommand = new BaseCommand(() => Messenger.Default.Send("ChooseComputerModel"));
                }
                return _ChooseComputerModelCommand;
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
        public EditComputerViewModel(ComputersCreateEditVM vm) : base("Edycja komputera")
        {
            oldItemJson = JsonSerializer.Serialize(vm);
            item = vm;
            setForeignKeys();
            Messenger.Default.Register<LocationVM>(this, getChosenLokacja);
            Messenger.Default.Register<UserVM>(this, getChosenUser);
            Messenger.Default.Register<ComputerTypeVM>(this, getComputerType);
            Messenger.Default.Register<ComputerModelVM>(this, getComputerModel);
            Messenger.Default.Register<ManufacturerVM>(this, getManufacturer);
            Messenger.Default.Register<StatusVM>(this, getStatus);
            Messenger.Default.Register<OperatingSystemVM>(this, getOperatingSystem);

        }
        public async void setForeignKeys()
        {
            StatusVM statusvm = await RequestHelper.SendRequestAsync<object, StatusVM>(URLs.STATUS_ID.Replace("{id}", item.StatusId.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            LocationVM locationVM = await RequestHelper.SendRequestAsync<object, LocationVM>(URLs.LOCATION_ID.Replace("{id}", item.LocationId.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            UserVM userVM = await RequestHelper.SendRequestAsync<object, UserVM>(URLs.USER_ID.Replace("{id}", item.UserId.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            ComputerTypeVM ctypevm = await RequestHelper.SendRequestAsync<object, ComputerTypeVM>(URLs.COMPUTERTYPE_ID.Replace("{id}", item.ComputerTypeId.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            ComputerModelVM cmodelvm = await RequestHelper.SendRequestAsync<object, ComputerModelVM>(URLs.COMPUTERMODEL_ID.Replace("{id}", item.ComputerModelId.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            ManufacturerVM manufacturerVM = await RequestHelper.SendRequestAsync<object, ManufacturerVM>(URLs.MANUFACTURER_ID.Replace("{id}", item.ManufacturerId.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            OperatingSystemVM osvm = await RequestHelper.SendRequestAsync<object, OperatingSystemVM>(URLs.OPERATINGSYSTEM_ID.Replace("{id}", item.OperatingSystemId.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            StatusName = statusvm.Name;
            OperatingSystemName = osvm.Name;
            LokacjaName = locationVM.Name;
            UserName = userVM.FirstName + " " + userVM.LastName + " " + userVM.InternalNumber + " " + userVM.Position;
            ComputerTypeName = ctypevm.Name;
            ManufacturerName = manufacturerVM.Name;
            ComputerModelName = cmodelvm.Name;
        }
        public override async void Save()
        {
            item.ModifiedAt = DateTime.Now;
            item.ModifiedBy = GlobalData.UserId;
            
            await RequestHelper.SendRequestAsync(URLs.COMPUTERS_ID.Replace("{id}", item.Id.ToString()), HttpMethod.Put, item, GlobalData.AccessToken);
            Messenger.Default.Send("ComputersRefresh");

            string newitem = JsonSerializer.Serialize(item);
            LogCreateEditVM log = new();
            log.Users = GlobalData.UserId;
            log.LogDate = DateTime.Now;
            log.CreatedAt = DateTime.Now;
            log.CreatedBy = GlobalData.UserId;
            log.Description = "Zmodyfikowano rekord w tabeli Komputery. Stara wartość " + oldItemJson + " Nowa wartość " + newitem;
            await RequestHelper.SendRequestAsync(URLs.LOG, HttpMethod.Post, log, GlobalData.AccessToken);
        }
        #endregion
        #region CommandsFunctions
        private void getStatus(StatusVM vm)
        {
            item.StatusId = vm.Id;
            StatusName = vm.Name;
        }
        private void getOperatingSystem(OperatingSystemVM vm)
        {
            item.OperatingSystemId = vm.Id;
            OperatingSystemName = vm.Name;
        }
        private void getChosenUser(UserVM vM)
        {
            item.UserId = vM.Id;
            UserName = vM.FirstName + " " + vM.LastName + " " + vM.InternalNumber + " " + vM.Position;
        }

        private void getComputerType(ComputerTypeVM vm)
        {
            item.ComputerTypeId = vm.Id;
            ComputerTypeName = vm.Name;
        }
        private void getManufacturer(ManufacturerVM vm)
        {
            item.ManufacturerId = vm.Id;
            ManufacturerName = vm.Name;
        }

        private void getChosenLokacja(LocationVM vM)
        {
            item.LocationId = vM.Id;
            LokacjaName = vM.Name;
        }
        private void getComputerModel(ComputerModelVM vm)
        {
            item.ComputerModelId = vm.Id;
            ComputerModelName = vm.Name;
        }
        #endregion
        #region Dane     
        private string _oldItemJson;
        public string oldItemJson
        {
            get
            {
                return _oldItemJson;
            }
            set
            {
                if( _oldItemJson != value )
                {
                    _oldItemJson = value;
                    OnPropertyChanged(() => oldItemJson);
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
        private string? _StatusName;
        public string? StatusName
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
        private string? _OperatingSystemName;
        public string? OperatingSystemName
        {
            get
            {
                return _OperatingSystemName;
            }
            set
            {
                if (_OperatingSystemName != value)
                {
                    _OperatingSystemName = value;
                    OnPropertyChanged(() => OperatingSystemName);
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
        public int? ComputerTypeId
        {
            get
            {
                return item.ComputerTypeId;
            }
            set
            {
                item.ComputerTypeId = value;
                OnPropertyChanged(() => ComputerTypeId);
            }
        }
        public int? ManufacturerId
        {
            get
            {
                return item.ManufacturerId;
            }
            set
            {
                item.ManufacturerId = value;
                OnPropertyChanged(() => ManufacturerId);
            }
        }
        public int? ComputerModelId
        {
            get
            {
                return item.ComputerModelId;
            }
            set
            {
                item.ComputerModelId = value;
                OnPropertyChanged(() => ComputerModelId);
            }
        }
        public string? Processor
        {
            get
            {
                return item.Processor;
            }
            set
            {
                item.Processor = value;
                OnPropertyChanged(() => Processor);
            }
        }
        public string? Ram
        {
            get
            {
                return item.Ram;
            }
            set
            {
                item.Ram = value;
                OnPropertyChanged(() => Ram);
            }
        }
        public string? Disk
        {
            get
            {
                return item.Disk;
            }
            set
            {
                item.Disk = value;
                OnPropertyChanged(() => Disk);
            }
        }
        public string? GraphicsCard
        {
            get
            {
                return item.GraphicsCard;
            }
            set
            {
                item.GraphicsCard = value;
                OnPropertyChanged(() => GraphicsCard);
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
        public int? OperatingSystemId
        {
            get
            {
                return item.OperatingSystemId;
            }
            set
            {
                item.OperatingSystemId = value;
                OnPropertyChanged(() => OperatingSystemId);
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
        public int? UserId
        {
            get
            {
                return item.UserId;
            }
            set
            {
                item.UserId = value;
                OnPropertyChanged(() => UserId);
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
        private string _ComputerTypeName;
        public string ComputerTypeName
        {
            get
            {
                return _ComputerTypeName;
            }
            set
            {
                if (_ComputerTypeName != value)
                {
                    _ComputerTypeName = value;
                    OnPropertyChanged(() => ComputerTypeName);
                }

            }
        }
        private string _ComputerModelName;
        public string ComputerModelName
        {
            get
            {
                return _ComputerModelName;
            }
            set
            {
                if (_ComputerModelName != value)
                {
                    _ComputerModelName = value;
                    OnPropertyChanged(() => ComputerModelName);
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
