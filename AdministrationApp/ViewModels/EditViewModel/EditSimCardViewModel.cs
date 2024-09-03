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
    class EditSimCardViewModel : JedenViewModel<SimCardsCreateEditVM>
    {
        #region Commands
        private BaseCommand _ChooseUserCommand;
        private BaseCommand _ChooseStatusCommand;
        private BaseCommand _ChooseManufacturerCommand;
        private BaseCommand _ChooseSimComponentTypeCommand;

        public ICommand ChooseSimComponentTypeCommand
        {
            get
            {
                if (_ChooseSimComponentTypeCommand == null)
                {
                    _ChooseSimComponentTypeCommand = new BaseCommand(() => Messenger.Default.Send("ChooseSimComponentType"));
                }
                return _ChooseSimComponentTypeCommand;
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
        public EditSimCardViewModel(SimCardsCreateEditVM vm) : base("Edycja Karty Sim")
        {
            item = vm;
            Messenger.Default.Register<UserVM>(this, getChosenUser);
            Messenger.Default.Register<StatusVM>(this, getStatus);

        }
        public override async void Save()
        {
            item.ModifiedAt = DateTime.Now;
            item.ModifiedBy = GlobalData.UserId;
            
            await RequestHelper.SendRequestAsync(URLs.SIMCARD_ID.Replace("{id}", item.Id.ToString()), HttpMethod.Post, item, GlobalData.AccessToken);
            Messenger.Default.Send("SimCardRefresh");
        }
        #endregion
        #region CommandsFunctions
        public async void setForeignKeys()
        {
            StatusVM statusvm = await RequestHelper.SendRequestAsync<object, StatusVM>(URLs.STATUS_ID.Replace("{id}", item.StatusId.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            UserVM userVM = await RequestHelper.SendRequestAsync<object, UserVM>(URLs.USER_ID.Replace("{id}", item.Users.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            StatusName = statusvm.Name;
            UserName = userVM.FirstName + " " + userVM.LastName + " " + userVM.InternalNumber + " " + userVM.Position;
        }
        private void getChosenUser(UserVM vM)
        {
            item.Users = vM.Id;
            UserName = vM.FirstName + " " + vM.LastName + " " + vM.InternalNumber + " " + vM.Position;
        }
        private void getStatus(StatusVM vm)
        {
            item.StatusId = vm.Id;
            StatusName = vm.Name;
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
        public int? Component
        {
            get
            {
                return item.Component;
            }
            set
            {
                item.Component = value;
                OnPropertyChanged(() => Component);
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
        public string? PinCode
        {
            get
            {
                return item.PinCode;
            }
            set
            {
                item.PinCode = value;
                OnPropertyChanged(() => PinCode);
            }
        }
        public string? PukCode
        {
            get
            {
                return item.PukCode;
            }
            set
            {
                item.PukCode = value;
                OnPropertyChanged(() => PukCode);
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
        public string? PhoneNumber
        {
            get
            {
                return item.PhoneNumber;
            }
            set
            {
                item.PhoneNumber = value;
                OnPropertyChanged(() => PhoneNumber);
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
