﻿using AdministrationApp.Helpers;
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
    class EditPhoneViewModel : JedenViewModel<PhonesCreateEditVM>
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
        public EditPhoneViewModel(PhonesCreateEditVM vm) : base("Edycja Telefonu")
        {
            item = vm;
            oldItem = vm;
            if (item != null) setForeignKeys();
            Messenger.Default.Register<LocationVM>(this, getChosenLokacja);
            Messenger.Default.Register<UserVM>(this, getChosenUser);
            Messenger.Default.Register<SimCardsVM>(this, getChosenSimCard1);
            Messenger.Default.Register<SimCardsVM>(this, getChosenSimCard2);
            Messenger.Default.Register<StatusVM>(this, getStatus);
            Messenger.Default.Register<ManufacturerVM>(this, getManufacturer);
            Messenger.Default.Register<PhoneModelVM>(this, getPhoneModel);
            Messenger.Default.Register<PhoneTypeVM>(this, getPhoneType);

        }
        public override async void Save()
        {
            item.ModifiedAt = DateTime.Now;
            item.ModifiedBy = GlobalData.UserId;
            EditSaveLogs(oldItem, item);
            await RequestHelper.SendRequestAsync(URLs.PHONE_ID.Replace("{id}", item.Id.ToString()), HttpMethod.Put, item, GlobalData.AccessToken);
            Messenger.Default.Send("PhoneRefresh");
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

            if (item.PhoneType != null)
            {
                try
                {
                    PhoneTypeVM ctypevm = await RequestHelper.SendRequestAsync<object, PhoneTypeVM>(
                        URLs.PHONETYPE_ID.Replace("{id}", item.PhoneType.ToString()),
                        HttpMethod.Get,
                        null,
                        GlobalData.AccessToken
                    );
                    PhoneTypeName = ctypevm.Name;
                }
                catch
                {
                    PhoneTypeName = "Nieaktywny typ telefonu";
                }
            }

            if (item.Model != null)
            {
                try
                {
                    PhoneModelVM cmodelvm = await RequestHelper.SendRequestAsync<object, PhoneModelVM>(
                        URLs.PHONEMODEL_ID.Replace("{id}", item.Model.ToString()),
                        HttpMethod.Get,
                        null,
                        GlobalData.AccessToken
                    );
                    PhoneModelName = cmodelvm.Name;
                }
                catch
                {
                    PhoneModelName = "Nieaktywny model";
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

            if (item.SimCard1 != null)
            {
                try
                {
                    SimCardsVM scvm1 = await RequestHelper.SendRequestAsync<object, SimCardsVM>(
                        URLs.SIMCARD_ID.Replace("{id}", item.SimCard1.ToString()),
                        HttpMethod.Get,
                        null,
                        GlobalData.AccessToken
                    );
                    SimCard1S = scvm1.PhoneNumber + " " + scvm1.InventoryNumber + " " + scvm1.SerialNumber;
                }
                catch
                {
                    SimCard1S = "Nieaktywna karta sim";
                }
            }

            if (item.SimCard2 != null)
            {
                try
                {
                    SimCardsVM scvm2 = await RequestHelper.SendRequestAsync<object, SimCardsVM>(
                        URLs.SIMCARD_ID.Replace("{id}", item.SimCard2.ToString()),
                        HttpMethod.Get,
                        null,
                        GlobalData.AccessToken
                    );
                    SimCard2S = scvm2.PhoneNumber + " " + scvm2.InventoryNumber + " " + scvm2.SerialNumber;
                }
                catch
                {
                    SimCard2S = "Nieaktywna karta sim";
                }
            }


        }
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
        private void getPhoneModel(PhoneModelVM vM)
        {
            item.Model = vM.Id;
            PhoneModelName = vM.Name;
        }
        private void getPhoneType(PhoneTypeVM vM)
        {
            item.PhoneType = vM.Id;
            PhoneTypeName = vM.Name;
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
        private string _PhoneModelName;
        public string PhoneModelName
        {
            get
            {
                return _PhoneModelName;
            }
            set
            {
                if (_PhoneModelName != value)
                {
                    _PhoneModelName = value;
                    OnPropertyChanged(() => PhoneModelName);
                }

            }
        }
        private string _PhoneTypeName;
        public string PhoneTypeName
        {
            get
            {
                return _PhoneTypeName;
            }
            set
            {
                if (_PhoneTypeName != value)
                {
                    _PhoneTypeName = value;
                    OnPropertyChanged(() => PhoneTypeName);
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
