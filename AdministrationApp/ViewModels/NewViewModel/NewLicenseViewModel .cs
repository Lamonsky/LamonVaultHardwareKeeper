using AdministrationApp.Helpers;
using Data;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Input;

namespace AdministrationApp.ViewModels.NewViewModel
{
    public class NewLicenseViewModel  : JedenViewModel<LicenseCreateEditVM>
    {
        #region Commands
        private BaseCommand? _ChooseLocationCommand;
        private BaseCommand? _ChooseUserCommand;
        private BaseCommand? _ChooseStatusCommand;
        private BaseCommand? _ChooseSoftwareCommand;
        private BaseCommand? _ChooseLicenseTypeCommand;
        private BaseCommand? _ChoosePublisherCommand;
        public ICommand ChoosePublisherCommand
        {
            get
            {
                if (_ChoosePublisherCommand == null)
                {
                    _ChoosePublisherCommand = new BaseCommand(() => Messenger.Default.Send("ChooseManufacturer"));
                }
                return _ChoosePublisherCommand;
            }
        }
        public ICommand ChooseLicenseTypeCommand
        {
            get
            {
                if (_ChooseLicenseTypeCommand == null)
                {
                    _ChooseLicenseTypeCommand = new BaseCommand(() => Messenger.Default.Send("ChooseLicenseType"));
                }
                return _ChooseLicenseTypeCommand;
            }
        }
        public ICommand ChooseSoftwareCommand
        {
            get
            {
                if (_ChooseSoftwareCommand == null)
                {
                    _ChooseSoftwareCommand = new BaseCommand(() => Messenger.Default.Send("ChooseSoftware"));
                }
                return _ChooseSoftwareCommand;
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
        public NewLicenseViewModel () : base("Nowa licencja")
        {
            item = new LicenseCreateEditVM();
            Messenger.Default.Register<LocationVM>(this, getChosenLokacja);
            Messenger.Default.Register<UserVM>(this, getChosenUser);
            Messenger.Default.Register<LicenseTypeVM>(this, getLicenseType);
            Messenger.Default.Register<SoftwaresVM>(this, getSoftware);
            Messenger.Default.Register<ManufacturerVM>(this, getPublisher);
            Messenger.Default.Register<StatusVM>(this, getStatus);

        }
        public override async void Save()
        {                      
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = GlobalData.UserId;            
            await RequestHelper.SendRequestAsync(URLs.LICENSE, HttpMethod.Post, item, GlobalData.AccessToken);
            Messenger.Default.Send("LicenseRefresh");
            NewSaveLogs(item);
        }
        #endregion
        #region CommandsFunctions
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

        private void getLicenseType(LicenseTypeVM vm)
        {
            item.LicenseType = vm.Id;
            LicenseTypeName = vm.Name;
        }
        private void getPublisher(ManufacturerVM vm)
        {
            item.Publisher = vm.Id;
            PublisherName = vm.Name;
        }
        
        private void getChosenLokacja(LocationVM vM)
        {
            item.LocationId = vM.Id;
            LokacjaName = vM.Name;
        }
        private void getSoftware(SoftwaresVM vm)
        {
            item.Software = vm.Id;
            SoftwareName = vm.Name;
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
        public DateTime? ExpiryDate
        {
            get
            {
                return item.ExpiryDate;
            }
            set
            {
                item.ExpiryDate = value;
                OnPropertyChanged(() => ExpiryDate);
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
        public int? LicenseType
        {
            get
            {
                return item.LicenseType;
            }
            set
            {
                item.LicenseType = value;
                OnPropertyChanged(() => LicenseType);
            }
        }
        public int? Publisher
        {
            get
            {
                return item.Publisher;
            }
            set
            {
                item.Publisher = value;
                OnPropertyChanged(() => Publisher);
            }
        }
        public int? Software
        {
            get
            {
                return item.Software;
            }
            set
            {
                item.Software = value;
                OnPropertyChanged(() => Software);
            }
        }
        private string _PublisherName;
        public string PublisherName
        {
            get
            {
                return _PublisherName;
            }
            set
            {
                if (_PublisherName != value)
                {
                    _PublisherName = value;
                    OnPropertyChanged(() => PublisherName);
                }

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
                if( _LokacjaName != value)
                {
                    _LokacjaName = value;
                    OnPropertyChanged(() =>  LokacjaName);
                }
            }
        }
        private string _LicenseTypeName;
        public string LicenseTypeName
        {
            get
            {
                return _LicenseTypeName;
            }
            set
            {
                if(_LicenseTypeName != value)
                {
                    _LicenseTypeName = value;
                    OnPropertyChanged(() => LicenseTypeName);
                }
                
            }
        }
        private string _SoftwareName;
        public string SoftwareName
        {
            get
            {
                return _SoftwareName;
            }
            set
            {
                if(_SoftwareName != value)
                {
                    _SoftwareName = value;
                    OnPropertyChanged(() => SoftwareName);
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
