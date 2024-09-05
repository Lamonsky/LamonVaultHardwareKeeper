using AdministrationApp.Helpers;
using AdministrationApp.ViewModels.NewViewModel;
using Data;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Input;

namespace AdministrationApp.ViewModels.EditViewModel
{
    public class EditLicenseViewModel  : JedenViewModel<LicenseCreateEditVM>
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
                    _ChoosePublisherCommand = new BaseCommand(() => Messenger.Default.Send("ChoosePublisher"));
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
        public EditLicenseViewModel (LicenseCreateEditVM vm) : base("Edycja licencji")
        {
            oldItem = vm;
            item = vm;
            if (item != null) setForeignKeys();
            Messenger.Default.Register<LocationVM>(this, getChosenLokacja);
            Messenger.Default.Register<UserVM>(this, getChosenUser);
            Messenger.Default.Register<LicenseTypeVM>(this, getLicenseType);
            Messenger.Default.Register<SoftwaresVM>(this, getSoftware);
            Messenger.Default.Register<ManufacturerVM>(this, getPublisher);
            Messenger.Default.Register<StatusVM>(this, getStatus);

        }
        private async void setForeignKeys()
        {
            if(item.LocationId != null)
            {
                LocationVM lvm = await RequestHelper.SendRequestAsync<object, LocationVM>(URLs.LOCATION_ID.Replace("{id}", item.LocationId.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
                LokacjaName = lvm.Name;

            }
            if (item.Users != null)
            {
                UserVM ivm = await RequestHelper.SendRequestAsync<object, UserVM>(URLs.USER_ID.Replace("{id}", item.Users.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
                UserName = ivm.FirstName + " " + ivm.LastName + " " + ivm.InternalNumber + " " + ivm.Position;

            }
            if (item.LicenseType != null)
            {
                LicenseTypeVM ltvm = await RequestHelper.SendRequestAsync<object, LicenseTypeVM>(URLs.LICENSETYPE_ID.Replace("{id}", item.LicenseType.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
                LicenseTypeName = ltvm.Name;

            }
            if (item.Software != null)
            {
                SoftwaresVM svm = await RequestHelper.SendRequestAsync<object, SoftwaresVM>(URLs.SOFTWARE_ID.Replace("{id}", item.Software.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
                SoftwareName = svm.Name;

            }
            if (item.Publisher != null)
            {
                ManufacturerVM mvm = await RequestHelper.SendRequestAsync<object, ManufacturerVM>(URLs.MANUFACTURER_ID.Replace("{id}", item.Publisher.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
                PublisherName = mvm.Name;

            }
            if (item.StatusId != null)
            {
                StatusVM stvm = await RequestHelper.SendRequestAsync<object, StatusVM>(URLs.STATUS_ID.Replace("{id}", item.StatusId.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
                StatusName = stvm.Name;

            }

        }
        public override async void Save()
        {                      
            item.ModifiedAt = DateTime.Now;
            item.ModifiedBy = GlobalData.UserId;            
            await RequestHelper.SendRequestAsync(URLs.LICENSE_ID.Replace("{id}", item.Id.ToString()), HttpMethod.Put, item, GlobalData.AccessToken);
            Messenger.Default.Send("LicenseRefresh");
            EditSaveLogs(oldItem, item);
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
