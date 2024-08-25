

using AdministrationApp.Helpers;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using Data;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Windows;
using AdministrationApp.ViewModels.NewViewModel;
using System.Windows.Input;

namespace AdministrationApp.ViewModels.EditViewModel.Windows
{
    public class EditNetworkDeviceTypeViewModel : JedenViewModel<NetworkDeviceTypeCreateEditVM>
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
        #region Konstruktor
        public EditNetworkDeviceTypeViewModel(Window window, NetworkDeviceTypeCreateEditVM vm) : base("NetworkDeviceType")
        {
            item = vm;
            _window = window;
            setForeignKeys();
            Messenger.Default.Register<StatusVM>(this, getStatus);
        }
        public override async void Save()
        {
            await RequestHelper.SendRequestAsync(URLs.NETWORKDEVICETYPE_ID.Replace("{id}", item.Id.ToString()), HttpMethod.Put, item, null);
            Messenger.Default.Send("NetworkDeviceTypeRefresh");
            _window.Close();
        }
        #endregion
        #region CommandsFunctions
        private async void setForeignKeys()
        {
            StatusVM vm = await RequestHelper.SendRequestAsync<object, StatusVM>(URLs.STATUS_ID.Replace("{id}", item.Status.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            StatusName = vm.Name;
        }
        private void getStatus(StatusVM vm)
        {
            item.Status = vm.Id;
            StatusName = vm.Name;
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
                if(_StatusName != value)
                {
                    _StatusName = value;
                    OnPropertyChanged(() => StatusName);
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
        public string? Comment
        {
            get
            {
                return item.Comment;
            }
            set
            {
                item.Comment = value;
                OnPropertyChanged(() => Comment);
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
        #endregion
    }
}

