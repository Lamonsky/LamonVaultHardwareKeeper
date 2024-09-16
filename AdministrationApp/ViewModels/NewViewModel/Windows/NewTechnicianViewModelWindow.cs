using AdministrationApp.Helpers;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using Data;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection.Metadata;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using AdministrationApp.Views.NewViews.Windows;

namespace AdministrationApp.ViewModels.NewViewModel.Windows
{
    public class NewTechnicianViewModelWindow : JedenViewModel<TechnicianCreateEditVM>
    {
        private Window _window;
        private BaseCommand _ChooseStatusCommand;
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
        private BaseCommand _ChooseUserCommand;
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
        #region Konstruktor
        public NewTechnicianViewModelWindow(Window window) : base("Nowy technik")
        {
            item = new TechnicianCreateEditVM();
            Messenger.Default.Register<UserVM>(this, getChosenUser);
            Messenger.Default.Register<StatusVM>(this, getStatus);
            _window = window;
        }
        public override async void Save()
        {
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = GlobalData.UserId;
            NewSaveLogs(item);
            await RequestHelper.SendRequestAsync(URLs.TECHNICIAN, HttpMethod.Post, item, GlobalData.AccessToken);
            Messenger.Default.Send("TechnicianRefresh");
            _window.Close();
        }
        #endregion
        #region CommandsFunctions

        private void getStatus(StatusVM vm)
        {
            item.Status = vm.Id;
            StatusName = vm.Name;
        }
        private async void getChosenUser(UserVM vm)
        {
            item.UsersId = vm.Id;
            UserName = vm.FirstName + " " + vm.LastName + " " + vm.InternalNumber + " " + vm.Position;
        }

        #endregion
        #region Dane
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
        private string _UserName;
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                if(_UserName != value)
                {
                    _UserName = value;
                    OnPropertyChanged(() => UserName);
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
        public int? UsersId
        {
            get
            {
                return item.UsersId;
            }
            set
            {
                item.UsersId = value;
                OnPropertyChanged(() => UsersId);
            }
        }        
        #endregion
    }
}
