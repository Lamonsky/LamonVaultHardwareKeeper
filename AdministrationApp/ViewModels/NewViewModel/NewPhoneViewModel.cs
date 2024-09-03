using AdministrationApp.Helpers;
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

namespace AdministrationApp.ViewModels.NewViewModel
{
    class NewPhoneViewModel : JedenViewModel<PhonesCreateEditVM>
    {
        #region Commands
        private BaseCommand _ChooseLocationCommand;
        private BaseCommand _ChooseUserCommand;
        private BaseCommand _ChooseSimCard1Command;
        private BaseCommand _ChooseSimCard2Command;
        private BaseCommand _ChooseStatusCommand;
        private BaseCommand _ChooseManufacturerCommand;
        private BaseCommand _ChoosePhoneModelCommand;
        private BaseCommand _ChoosePhoneTypeCommand;

        public ICommand ChoosePhoneTypeCommand
        {
            get
            {
                if (_ChoosePhoneTypeCommand == null)
                {
                    _ChoosePhoneTypeCommand = new BaseCommand(() => Messenger.Default.Send("ChoosePhoneType"));
                }
                return _ChoosePhoneTypeCommand;
            }
        }
        public ICommand ChoosePhoneModelCommand
        {
            get
            {
                if (_ChoosePhoneModelCommand == null)
                {
                    _ChoosePhoneModelCommand = new BaseCommand(() => Messenger.Default.Send("ChoosePhoneModel"));
                }
                return _ChoosePhoneModelCommand;
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
        public ICommand ChooseSimCard1Command
        {
            get
            {
                if (_ChooseSimCard1Command == null)
                {
                    _ChooseSimCard1Command = new BaseCommand(() => Messenger.Default.Send("ChooseSimCard"));
                }
                return _ChooseSimCard1Command;
            }
        }
        public ICommand ChooseSimCard2Command
        {
            get
            {
                if (_ChooseSimCard2Command == null)
                {
                    _ChooseSimCard2Command = new BaseCommand(() => Messenger.Default.Send("ChooseSimCard"));
                }
                return _ChooseSimCard2Command;
            }
        }
        #endregion
        #region Konstruktor
        public NewPhoneViewModel() : base("Nowy Telefon")
        {
            item = new PhonesCreateEditVM();
            Messenger.Default.Register<LocationVM>(this, getChosenLokacja);
            Messenger.Default.Register<UserVM>(this, getChosenUser);
            Messenger.Default.Register<SimCardsVM>(this, getChosenSimCard1);
            Messenger.Default.Register<SimCardsVM>(this, getChosenSimCard2);
            Messenger.Default.Register<StatusVM>(this, getStatus);
            Messenger.Default.Register<ManufacturerVM>(this, getManufacturer);

        }
        public override async void Save()
        {
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = GlobalData.UserId;
            
            await RequestHelper.SendRequestAsync(URLs.PHONE, HttpMethod.Post, item, GlobalData.AccessToken);
            Messenger.Default.Send("PhoneRefresh");
        }
        #endregion
        #region CommandsFunctions
        private void getManufacturer(ManufacturerVM vm)
        {
            item.Manufacturer = vm.Id;
            ManufacturerName = vm.Name;
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
        private void getChosenSimCard1(SimCardsVM scvm1)
        {
            item.SimCard1 = scvm1.Id;
            SimCard1S = scvm1.PhoneNumber + " " + scvm1.InventoryNumber + " " + scvm1.SerialNumber;
        }
        private void getChosenSimCard2(SimCardsVM scvm2)
        {
            item.SimCard2 = scvm2.Id;
            SimCard2S = scvm2.PhoneNumber + " " + scvm2.InventoryNumber + " " + scvm2.SerialNumber;
        }
        private void getChosenLokacja(LocationVM vM)
        {
            item.LocationId = vM.Id;
            LokacjaName = vM.Name;
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
        public int? PhoneType
        {
            get
            {
                return item.PhoneType;
            }
            set
            {
                item.PhoneType = value;
                OnPropertyChanged(() => PhoneType);
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
        public int? SimCard1
        {
            get
            {
                return item.SimCard1;
            }
            set
            {
                item.SimCard1 = value;
                OnPropertyChanged(() => SimCard1);
            }
        }
        public int? SimCard2
        {
            get
            {
                return item.SimCard2;
            }
            set
            {
                item.SimCard2 = value;
                OnPropertyChanged(() => SimCard2);
            }
        }
        private string _SimCard1S;
        public string? SimCard1S
        {
            get
            {
                return _SimCard1S;
            }
            set
            {
                _SimCard1S = value;
                OnPropertyChanged(() => SimCard1S);
            }
        }
        private string _SimCard2S;
        public string? SimCard2S
        {
            get
            {
                return _SimCard2S;
            }
            set
            {
                _SimCard2S = value;
                OnPropertyChanged(() => SimCard2S);
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
                if (_LokacjaName == null)
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
