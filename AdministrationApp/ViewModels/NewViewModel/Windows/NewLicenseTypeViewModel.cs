
using AdministrationApp.Helpers;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using Data;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Windows;

namespace AdministrationApp.ViewModels.NewViewModel.Windows
{
    public class NewLicenseTypeViewModel : JedenViewModel<NewLicenseTypeCreateEditVM>
    {
        private Window _window;
        #region Konstruktor
        public NewLicenseTypeViewModel(Window window) : base("LicenseType")
        {
            item = new LicenseTypeCreateEditVM();
            _window = window;
        }
        public override async void Save()
        {
            await RequestHelper.SendRequestAsync(URLs.LICENSETYPE, HttpMethod.Post, item, null);
            Messenger.Default.Send("LicenseTypeRefresh");
            _window.Close();
        }
        #endregion
        #region CommandsFunctions

        private async void getStatusItems()
        {
            statusComboBoxItems = await RequestHelper.SendRequestAsync<object, List<StatusVM>>(URLs.STATUS, HttpMethod.Get, null, null);
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