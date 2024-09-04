using AdministrationApp.Helpers;
using Data;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Windows.Input;

namespace AdministrationApp.ViewModels.NewViewModel
{
    public class NewServerViewModel : JedenViewModel<ServerCreateEditVM>
    {
        #region Commands
        private BaseCommand _ChooseStatusCommand;
        private BaseCommand _ChooseManufacturerCommand;
        private BaseCommand _ChooseServerModelCommand;
        private BaseCommand _ChooseOperatingSystemCommand;
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
        public ICommand ChooseServerModelCommand
        {
            get
            {
                if (_ChooseServerModelCommand == null)
                {
                    _ChooseServerModelCommand = new BaseCommand(() => Messenger.Default.Send("ChooseComputerModel"));
                }
                return _ChooseServerModelCommand;
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
        #endregion
        #region Konstruktor
        public NewServerViewModel() : base("NewServer")
        {
            item = new ServerCreateEditVM();
            Messenger.Default.Register<OperatingSystemVM>(this, getOS);
            Messenger.Default.Register<ManufacturerVM>(this, getManufacturer);
            Messenger.Default.Register<StatusVM>(this, getStatus);
            Messenger.Default.Register<ComputerModelVM>(this, getServerModel);
            Messenger.Default.Register<LocationVM>(this, getLocation);
            Messenger.Default.Register<UserVM>(this, getUser);

        }
        public override async void Save()
        {
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = GlobalData.UserId;
            
            await RequestHelper.SendRequestAsync(URLs.SERVER, HttpMethod.Post, item, GlobalData.AccessToken);
            Messenger.Default.Send("ServerRefresh");
        }
        #endregion
        #region CommandsFunctions
        private void getServerModel(ComputerModelVM vm)
        {
            item.Model = vm.Id;
            ServerModelName = vm.Name;
        }
        private void getStatus(StatusVM vm)
        {
            item.StatusId = vm.Id;
            StatusName = vm.Name;
        }
        private void getOS(OperatingSystemVM vM)
        {
            item.OperatingSystem = vM.Id;
            OperatingSystemName = vM.Name;
        }
        private void getManufacturer(ManufacturerVM vm)
        {
            item.Manufacturer = vm.Id;
            ManufacturerName = vm.Name;
        }
        private void getLocation(LocationVM vM)
        {
            item.LocationId = vM.Id;
            LocationName = vM.Name;
        }
        private void getUser(UserVM vm)
        {
            item.Users = vm.Id;
            UserName = vm.FirstName + " " + vm.LastName + " " + vm.Email;
        }

        #endregion
        #region Dane
        private string _OperatingSystemName;
        public string OperatingSystemName
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
        private string _LocationName;
        public string LocationName
        {
            get
            {
                return _LocationName;
            }
            set
            {
                if (_LocationName != value)
                {
                    _LocationName = value;
                    OnPropertyChanged(() => LocationName);
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
        private string _ServerModelName;
        public string ServerModelName
        {
            get
            {
                return _ServerModelName;
            }
            set
            {
                if (_ServerModelName != value)
                {
                    _ServerModelName = value;
                    OnPropertyChanged(() => ServerModelName);
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
        public string Name
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
        public string Processor
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
        public string Ram
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
        public string SerialNumber
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
        public string InventoryNumber
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
        public int? OperatingSystem
        {
            get
            {
                return item.OperatingSystem;
            }
            set
            {
                item.OperatingSystem = value;
                OnPropertyChanged(() => OperatingSystem);
            }
        }

        #endregion
    }
}
