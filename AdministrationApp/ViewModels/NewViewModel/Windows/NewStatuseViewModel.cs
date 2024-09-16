
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
    public class NewStatuseViewModel : JedenViewModel<StatusCreateEditVM>
    {
        private Window _window;
        #region Konstruktor
        public NewStatuseViewModel(Window window) : base("Nowy status")
        {
            item = new StatusCreateEditVM();
            _window = window;
        }
        public override async void Save()
        {
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = GlobalData.UserId;
            NewSaveLogs(item);
            await RequestHelper.SendRequestAsync(URLs.STATUS, HttpMethod.Post, item, GlobalData.AccessToken);
            Messenger.Default.Send("StatusRefresh");
            _window.Close();
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
