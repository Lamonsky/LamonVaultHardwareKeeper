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
    class EditRackCabinetViewModel : JedenViewModel<RackCabinetCreateEditVM>
    {
        #region Commands
        private BaseCommand _ChooseLocationCommand;
        private BaseCommand _ChooseUserCommand;
        private BaseCommand _ChooseStatusCommand;
        private BaseCommand _ChooseManufacturerCommand;
        private BaseCommand _ChooseRackCabinetModelCommand;
        private BaseCommand _ChooseRackCabinetTypeCommand;

        public ICommand ChooseRackCabinetTypeCommand
        {
            get
            {
                if (_ChooseRackCabinetTypeCommand == null)
                {
                    _ChooseRackCabinetTypeCommand = new BaseCommand(() => Messenger.Default.Send("ChooseRackCabinetType"));
                }
                return _ChooseRackCabinetTypeCommand;
            }
        }
        public ICommand ChooseRackCabinetModelCommand
        {
            get
            {
                if (_ChooseRackCabinetModelCommand == null)
                {
                    _ChooseRackCabinetModelCommand = new BaseCommand(() => Messenger.Default.Send("ChooseRackCabinetModel"));
                }
                return _ChooseRackCabinetModelCommand;
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
        public EditRackCabinetViewModel(RackCabinetCreateEditVM vm) : base("Edycja Szafy Rack")
        {
            item = vm;
            oldItem = vm;
            if (item != null) setForeignKeys();
            Messenger.Default.Register<LocationVM>(this, getChosenLokacja);
            Messenger.Default.Register<UserVM>(this, getChosenUser);
            Messenger.Default.Register<StatusVM>(this, getStatus);
            Messenger.Default.Register<RackCabinetModelVM>(this, getRackCabinetModel);
            Messenger.Default.Register<ManufacturerVM>(this, getManufacturer);
            Messenger.Default.Register<RackCabinetTypeVM>(this, getRackCabinetType);

        }
        public override async void Save()
        {
            item.ModifiedAt = DateTime.Now;
            item.ModifiedBy = GlobalData.UserId;
            EditSaveLogs(oldItem, item);
            await RequestHelper.SendRequestAsync(URLs.RACKCABINET_ID.Replace("{id}", item.Id.ToString()), HttpMethod.Put, item, GlobalData.AccessToken);
            Messenger.Default.Send("RackCabinetRefresh");
        }
        #endregion
        #region CommandsFunctions
        public async void setForeignKeys()
        {
            if (item.StatusId != null)
            {
                StatusVM statusvm = await RequestHelper.SendRequestAsync<object, StatusVM>(
                    URLs.STATUS_ID.Replace("{id}", item.StatusId.ToString()),
                    HttpMethod.Get,
                    null,
                    GlobalData.AccessToken
                );
                StatusName = statusvm.Name;
            }

            if (item.LocationId != null)
            {
                LocationVM locationVM = await RequestHelper.SendRequestAsync<object, LocationVM>(
                    URLs.LOCATION_ID.Replace("{id}", item.LocationId.ToString()),
                    HttpMethod.Get,
                    null,
                    GlobalData.AccessToken
                );
                LokacjaName = locationVM.Name;
            }

            if (item.Users != null)
            {
                UserVM userVM = await RequestHelper.SendRequestAsync<object, UserVM>(
                    URLs.USER_ID.Replace("{id}", item.Users.ToString()),
                    HttpMethod.Get,
                    null,
                    GlobalData.AccessToken
                );
                UserName = userVM.FirstName + " " + userVM.LastName + " " + userVM.InternalNumber + " " + userVM.Position;
            }

            if (item.CabinetType != null)
            {
                RackCabinetTypeVM ctypevm = await RequestHelper.SendRequestAsync<object, RackCabinetTypeVM>(
                    URLs.RACKCABINETTYPE_ID.Replace("{id}", item.CabinetType.ToString()),
                    HttpMethod.Get,
                    null,
                    GlobalData.AccessToken
                );
                RackCabinetTypeName = ctypevm.Name;
            }

            if (item.Model != null)
            {
                RackCabinetModelVM cmodelvm = await RequestHelper.SendRequestAsync<object, RackCabinetModelVM>(
                    URLs.RACKCABINETMODEL_ID.Replace("{id}", item.Model.ToString()),
                    HttpMethod.Get,
                    null,
                    GlobalData.AccessToken
                );
                RackCabinetModelName = cmodelvm.Name;
            }

            if (item.Manufacturer != null)
            {
                ManufacturerVM manufacturerVM = await RequestHelper.SendRequestAsync<object, ManufacturerVM>(
                    URLs.MANUFACTURER_ID.Replace("{id}", item.Manufacturer.ToString()),
                    HttpMethod.Get,
                    null,
                    GlobalData.AccessToken
                );
                ManufacturerName = manufacturerVM.Name;
            }

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

        private void getChosenLokacja(LocationVM vM)
        {
            item.LocationId = vM.Id;
            LokacjaName = vM.Name;
        }
        private void getRackCabinetModel(RackCabinetModelVM vm)
        {
            item.Id = vm.Id;
            RackCabinetModelName = vm.Name;
        }
        private void getManufacturer(ManufacturerVM vm)
        {
            item.Manufacturer = vm.Id;
            ManufacturerName = vm.Name;
        }
        private void getRackCabinetType(RackCabinetTypeVM vm)
        {
            item.CabinetType = vm.Id;
            RackCabinetTypeName = vm.Name;
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
        private string _RackCabinetTypeName;
        public string RackCabinetTypeName
        {
            get
            {
                return _RackCabinetTypeName;
            }
            set
            {
                if (_RackCabinetTypeName != value)
                {
                    _RackCabinetTypeName = value;
                    OnPropertyChanged(() => RackCabinetTypeName);
                }

            }
        }
        private string _RackCabinetModelName;
        public string RackCabinetModelName
        {
            get
            {
                return _RackCabinetModelName;
            }
            set
            {
                if (_RackCabinetModelName != value)
                {
                    _RackCabinetModelName = value;
                    OnPropertyChanged(() => RackCabinetModelName);
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
        public int? CabinetType
        {
            get
            {
                return item.CabinetType;
            }
            set
            {
                item.CabinetType = value;
                OnPropertyChanged(() => CabinetType);
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
        public int? Height
        {
            get
            {
                return item.Height; 
            }
            set
            {
                item.Height = value;
                OnPropertyChanged(() => Height);
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
