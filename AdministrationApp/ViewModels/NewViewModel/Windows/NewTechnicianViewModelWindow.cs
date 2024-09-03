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
        public NewTechnicianViewModelWindow(Window window) : base("Technician")
        {
            item = new TechnicianCreateEditVM();
            Messenger.Default.Register<UserVM>(this, getChosenUser);
            _window = window;
        }
        public override async void Save()
        {
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = GlobalData.UserId;
            await RequestHelper.SendRequestAsync(URLs.REFRESH, HttpMethod.Post, GlobalData.AccessToken, GlobalData.AccessToken);
            await RequestHelper.SendRequestAsync(URLs.TECHNICIAN, HttpMethod.Post, item, GlobalData.AccessToken);
            Messenger.Default.Send("TechnicianRefresh");
            _window.Close();
        }
        #endregion
        #region CommandsFunctions

        private async void getStatusItems()
        {
            statusComboBoxItems = await RequestHelper.SendRequestAsync<object, List<StatusVM>>(URLs.STATUS, HttpMethod.Get, null, GlobalData.AccessToken);
        }
        private async void getChosenUser(UserVM vm)
        {
            item.UsersId = vm.Id;
            UserName = vm.FirstName + " " + vm.LastName + " " + vm.InternalNumber + " " + vm.Position;
        }

        #endregion
        #region Dane
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
        private List<StatusVM> _statusComboBoxItems;
        public List<StatusVM> statusComboBoxItems
        {
            get
            {
                if (_statusComboBoxItems == null)
                {
                    getStatusItems();
                }
                return _statusComboBoxItems;
            }
            set
            {
                _statusComboBoxItems = value;
                OnPropertyChanged(() => statusComboBoxItems);
            }
        }
        #endregion
    }
}
