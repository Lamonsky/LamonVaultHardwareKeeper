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
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdministrationApp.ViewModels.EditViewModel
{
    public class EditHardDriveViewModel : JedenViewModel<HardDriveCreateEditVM>
    {
        #region Commands
        private BaseCommand _ChooseStatusCommand;
        private BaseCommand _ChooseManufacturerCommand;
        private BaseCommand _ChooseHardDriveModelCommand;
        private BaseCommand _ChooseServerCommand;
        public ICommand ChooseHardDriveModelCommand
        {
            get
            {
                if (_ChooseHardDriveModelCommand == null)
                {
                    _ChooseHardDriveModelCommand = new BaseCommand(() => Messenger.Default.Send("ChooseHardDriveModel"));
                }
                return _ChooseHardDriveModelCommand;
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
        public ICommand ChooseServerCommand
        {
            get
            {
                if (_ChooseServerCommand == null)
                {
                    _ChooseServerCommand = new BaseCommand(() => Messenger.Default.Send("ChooseServer"));
                }
                return _ChooseServerCommand;
            }
        }
        #endregion
        #region Konstruktor
        public EditHardDriveViewModel(HardDriveCreateEditVM vm) : base("NewHardDrive")
        {
            item = vm;
            oldItem = vm;
            if (item != null) setForeignKeys();
            Messenger.Default.Register<ServerVM>(this, getServer);
            Messenger.Default.Register<ManufacturerVM>(this, getManufacturer);
            Messenger.Default.Register<StatusVM>(this, getStatus);
            Messenger.Default.Register<HardDriveModelVM>(this, getHardDriveModel);

        }
        public async void setForeignKeys()
        {
            if (item.Server != null)
            {
                try
                {
                    ServerVM svm = await RequestHelper.SendRequestAsync<object, ServerVM>(
                    URLs.SERVER_ID.Replace("{id}", item.Server.ToString()),
                    HttpMethod.Get,
                    null,
                    GlobalData.AccessToken
                );
                    ServerName = svm.Name;
                }
                catch
                {
                    ServerName = "Nieaktywny serwer";
                }
                
            }

            if (item.Manufacturer != null)
            {
                try
                {
                    ManufacturerVM Mvm = await RequestHelper.SendRequestAsync<object, ManufacturerVM>(
                    URLs.MANUFACTURER_ID.Replace("{id}", item.Manufacturer.ToString()),
                    HttpMethod.Get,
                    null,
                    GlobalData.AccessToken
                );
                    ManufacturerName = Mvm.Name;
                }
                catch
                {
                    ManufacturerName = "Nieaktywny producent";
                }
                
            }

            if (item.Status != null)
            {
                try
                {
                    StatusVM sTvm = await RequestHelper.SendRequestAsync<object, StatusVM>(
                                        URLs.STATUS_ID.Replace("{id}", item.Status.ToString()),
                                        HttpMethod.Get,
                                        null,
                                        GlobalData.AccessToken
                                    );
                    StatusName = sTvm.Name;
                }
                catch
                {
                    StatusName = "Nieaktywny status";
                }
                
            }

            if (item.Model != null)
            {
                try
                {
                    HardDriveModelVM hdmvm = await RequestHelper.SendRequestAsync<object, HardDriveModelVM>(
                                        URLs.HARDDRIVEMODEL_ID.Replace("{id}", item.Model.ToString()),
                                        HttpMethod.Get,
                                        null,
                                        GlobalData.AccessToken
                                    );
                    HardDriveModelName = hdmvm.Name;
                }
                catch
                {
                    HardDriveModelName = "Nieaktywny model";
                }
                
            }

        }
        public override async void Save()
        {
            item.ModifiedAt = DateTime.Now;
            item.ModifiedBy = GlobalData.UserId;
            EditSaveLogs(oldItem, item);
            await RequestHelper.SendRequestAsync(URLs.HARDDRIVE_ID.Replace("{id}", item.Id.ToString()), HttpMethod.Put, item, GlobalData.AccessToken);
            Messenger.Default.Send("HardDrivesRefresh");
        }
        #endregion
        #region CommandsFunctions
        private void getHardDriveModel(HardDriveModelVM vm)
        {
            item.Model = vm.Id;
            HardDriveModelName = vm.Name;
        }
        private void getStatus(StatusVM vm)
        {
            item.Status = vm.Id;
            StatusName = vm.Name;
        }
        private void getServer(ServerVM vM)
        {
            item.Server = vM.Id;
            ServerName = vM.Name + " " + vM.Manufacturer + " " + vM.Model + " " + vM.Location;
        }
        private void getManufacturer(ManufacturerVM vm)
        {
            item.Manufacturer = vm.Id;
            ManufacturerName = vm.Name;
        }

        #endregion
        #region Dane
        private string _ServerName;
        public string ServerName
        {
            get
            {
                return _ServerName;
            }
            set
            {
                if (_ServerName != value)
                {
                    _ServerName = value;
                    OnPropertyChanged(() => ServerName);
                }

            }
        }
        private string _HardDriveModelName;
        public string HardDriveModelName
        {
            get
            {
                return _HardDriveModelName;
            }
            set
            {
                if (_HardDriveModelName != value)
                {
                    _HardDriveModelName = value;
                    OnPropertyChanged(() => HardDriveModelName);
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
        public int? Status
        {
            get
            {
                return item.Status;
            }
            set
            {
                item.Status = value;
                OnPropertyChanged(() => Status);
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
        public int? Capacity
        {
            get
            {
                return item.Capacity;
            }
            set
            {
                item.Capacity = value;
                OnPropertyChanged(() => Capacity);
            }
        }
        public int? Server
        {
            get
            {
                return item.Server;
            }
            set
            {
                item.Server = value;
                OnPropertyChanged(() => Server);
            }
        }
        
        #endregion
    }
}
