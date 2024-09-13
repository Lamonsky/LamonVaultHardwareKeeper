using AdministrationApp.Helpers;
using AdministrationApp.ViewModels.NewViewModel;
using Data;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdministrationApp.ViewModels.EditViewModel
{
    class EditPrinterViewModel : JedenViewModel<PrintersCreateEditVM>
    {
        #region Commands
        private BaseCommand _ChooseLocationCommand;
        private BaseCommand _ChooseUserCommand;
        private BaseCommand _ChoosePrinterTypeCommand;
        private BaseCommand _ChoosePrinterModelCommand;
        private BaseCommand _ChooseStatusCommand;
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
        public ICommand ChoosePrinterTypeCommand
        {
            get
            {
                if (_ChoosePrinterTypeCommand == null)
                {
                    _ChoosePrinterTypeCommand = new BaseCommand(() => Messenger.Default.Send("ChoosePrinterType"));
                }
                return _ChoosePrinterTypeCommand;
            }
        }
        public ICommand ChoosePrinterModelCommand
        {
            get
            {
                if (_ChoosePrinterModelCommand == null)
                {
                    _ChoosePrinterModelCommand = new BaseCommand(() => Messenger.Default.Send("ChoosePrinterModel"));
                }
                return _ChoosePrinterModelCommand;
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
        public EditPrinterViewModel(PrintersCreateEditVM vm) : base("Nowa drukarka")
        {
            item = vm;
            oldItem = vm;
            if (item != null) setForeignKeys();
            Messenger.Default.Register<LocationVM>(this, getChosenLokacja);
            Messenger.Default.Register<UserVM>(this, getChosenUser);
            Messenger.Default.Register<PrinterTypeVM>(this, getPrinterType);
            Messenger.Default.Register<PrinterModelVM>(this, getPrinterModel);
            Messenger.Default.Register<StatusVM>(this, getStatus);
            Messenger.Default.Register<ManufacturerVM>(this, getManufacturer);
        }
        public override async void Save()
        {
            item.ModifiedAt = DateTime.Now;
            item.ModifiedBy = GlobalData.UserId;
            EditSaveLogs(oldItem, item);
            await RequestHelper.SendRequestAsync(URLs.PRINTER_ID.Replace("{id}", item.Id.ToString()), HttpMethod.Put, item, GlobalData.AccessToken);
            Messenger.Default.Send("PrinterRefresh");
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
                catch (Exception ex)
                {
                    StatusName = "Niekatywny status";
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

            if (item.PrinterType != null)
            {
                try
                {
                    PrinterTypeVM ctypevm = await RequestHelper.SendRequestAsync<object, PrinterTypeVM>(
                    URLs.PRINTERTYPE_ID.Replace("{id}", item.PrinterType.ToString()),
                    HttpMethod.Get,
                    null,
                    GlobalData.AccessToken
                );
                    PrinterTypeName = ctypevm.Name;
                }
                catch 
                {
                    PrinterTypeName = "Nieaktywny typ drukarki";
                }
                
            }

            if (item.Model != null)
            {
                try
                {
                    PrinterModelVM cmodelvm = await RequestHelper.SendRequestAsync<object, PrinterModelVM>(
                    URLs.PRINTERMODEL_ID.Replace("{id}", item.Model.ToString()),
                    HttpMethod.Get,
                    null,
                    GlobalData.AccessToken
                );
                    PrinterModelName = cmodelvm.Name;
                }
                catch
                {
                    PrinterModelName = "Nieaktywny model drukarki";
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
        public void ValidateIPAddress()
        {
            if (IPAddress.TryParse(IpAddress, out _))
            {
                IsValid = true;
                ErrorMessage = null;
            }
            else
            {
                IsValid = false;
            }
            if (!IsValid)
            {
                ErrorMessage = ("Proszę wpisać poprawny adres IP");
            }
        }
        private void getChosenUser(UserVM vM)
        {
            item.Users = vM.Id;
            UserName = vM.FirstName + " " + vM.LastName + " " + vM.InternalNumber + " " + vM.Position;
        }

        private async void getPrinterType(PrinterTypeVM vM)
        {
            item.PrinterType = vM.Id;
            PrinterTypeName = vM.Name;
        }
        private void getChosenLokacja(LocationVM vM)
        {
            item.LocationId = vM.Id;
            LokacjaName = vM.Name;
        }
        private async void getPrinterModel(PrinterModelVM vM)
        {
            item.Model = vM.Id;
            PrinterModelName = vM.Name;
        }
        private void getManufacturer(ManufacturerVM vM)
        {
            item.Manufacturer = vM.Id;
            ManufacturerName = vM.Name;
        }
        private void getStatus(StatusVM vM)
        {
            item.StatusId = vM.Id;
            StatusName = vM.Name;
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
        public int? PrinterType
        {
            get
            {
                return item.PrinterType;
            }
            set
            {
                item.PrinterType = value;
                OnPropertyChanged(() => PrinterType);
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
        public string? IpAddress
        {
            get
            {
                return item.IpAddress;
            }
            set
            {
                item.IpAddress = value;
                OnPropertyChanged(() => IpAddress);
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
        private string _PrinterTypeName;
        public string PrinterTypeName
        {
            get
            {
                return _PrinterTypeName;
            }
            set
            {
                if (_PrinterTypeName != value)
                {
                    _PrinterTypeName = value;
                    OnPropertyChanged(() => PrinterTypeName);
                }
            }
        }
        private string _PrinterModelName;
        public string PrinterModelName
        {
            get
            {
                return _PrinterModelName;
            }
            set
            {
                if (_PrinterModelName != value)
                {
                    _PrinterModelName = value;
                    OnPropertyChanged(() => PrinterModelName);
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
