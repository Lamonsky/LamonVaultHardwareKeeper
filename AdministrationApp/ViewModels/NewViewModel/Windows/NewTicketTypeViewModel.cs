
using AdministrationApp.Helpers;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using Data;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace AdministrationApp.ViewModels.NewViewModel.Windows
{
    public class NewTicketTypeViewModel : JedenViewModel<TicketTypeCreateEditVM>
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
        public NewTicketTypeViewModel(Window window) : base("TicketType")
        {
            item = new TicketTypeCreateEditVM();
            Messenger.Default.Register<StatusVM>(this, getStatus);
            _window = window;
        }
        public override async void Save()
        {
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = GlobalData.UserId;
            await RequestHelper.SendRequestAsync(URLs.TICKETTYPE, HttpMethod.Post, item, GlobalData.AccessToken);
            NewSaveLogs(item);
            Messenger.Default.Send("TicketTypeRefresh");
            _window.Close();
        }
        #endregion
        #region CommandsFunctions

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
                if (_StatusName != value)
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
