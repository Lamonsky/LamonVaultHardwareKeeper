﻿using AdministrationApp.Helpers;
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
    class NewSimCardViewModel : JedenViewModel<SimCardsCreateEditVM>
    {
        #region Commands
        private BaseCommand _ChooseUserCommand;
        private BaseCommand _ChooseStatusCommand;
        private BaseCommand _ChooseSimComponentCommand;

        public ICommand ChooseSimComponentCommand
        {
            get
            {
                if (_ChooseSimComponentCommand == null)
                {
                    _ChooseSimComponentCommand = new BaseCommand(() => Messenger.Default.Send("ChooseSimComponent"));
                }
                return _ChooseSimComponentCommand;
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
        public NewSimCardViewModel() : base("Nowa Karta Sim")
        {
            item = new SimCardsCreateEditVM();
            Messenger.Default.Register<UserVM>(this, getChosenUser);
            Messenger.Default.Register<StatusVM>(this, getStatus);
            Messenger.Default.Register<SimComponentVM>(this, getComponent);

        }
        public override async void Save()
        {
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = GlobalData.UserId;
            NewSaveLogs(item);
            await RequestHelper.SendRequestAsync(URLs.SIMCARD, HttpMethod.Post, item, GlobalData.AccessToken);
            Messenger.Default.Send("SimCardRefresh");
        }
        #endregion
        #region CommandsFunctions
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
        private void getComponent(SimComponentVM vm)
        {
            item.Component = vm.Id;
            ComponentName = vm.Name;
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
                if (_StatusName != value)
                {
                    _StatusName = value;
                    OnPropertyChanged(() => StatusName);
                }

            }
        }
        private string _ComponentName;
        public string ComponentName
        {
            get
            {
                return _ComponentName;
            }
            set
            {
                if (_ComponentName != value)
                {
                    _ComponentName = value;
                    OnPropertyChanged(() => ComponentName);
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
