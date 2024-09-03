

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
    public class EditMonitorTypeViewModel : JedenViewModel<MonitorTypeCreateEditVM>
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
        public EditMonitorTypeViewModel(Window window, MonitorTypeCreateEditVM vm) : base("MonitorType")
        {
            item = vm;
            _window = window;
            setForeignKeys();
            Messenger.Default.Register<StatusVM>(this, getStatus);
        }
        public override async void Save()
        {
            item.ModifiedAt = DateTime.Now;
            item.ModifiedBy = GlobalData.UserId;
            
            await RequestHelper.SendRequestAsync(URLs.MONITORTYPE_ID.Replace("{id}", item.Id.ToString()), HttpMethod.Put, item, GlobalData.AccessToken);
            Messenger.Default.Send("MonitorTypeRefresh");
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

