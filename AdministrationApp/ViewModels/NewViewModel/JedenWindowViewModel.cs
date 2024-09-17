using AdministrationApp.Helpers;
using Data.Computers.CreateEditVMs;
using Data.Helpers;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AdministrationApp.ViewModels.NewViewModel
{
    public abstract class JedenWindowViewModel<T> : WorkspaceViewModel
    {
        protected Window _window;

        #region Command
        private BaseCommand _SaveAndCloseCommand;
        public ICommand SaveAndCloseCommand
        {
            get
            {
                if (_SaveAndCloseCommand == null)
                    _SaveAndCloseCommand = new BaseCommand(SaveAndClose);
                return _SaveAndCloseCommand;
            }
        }
        private BaseCommand _CloseCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                    _CloseCommand = new BaseCommand(CloseWindow);
                return _CloseCommand;
            }
        }
        #endregion

        #region Konstruktor
        protected T item;
        private T _oldItem;
        public T oldItem
        {
            get
            {
                return _oldItem;
            }
            set
            {
                _oldItem = value;
            }
        }
        private bool _IsValid;
        public bool IsValid
        {
            get
            {
                return _IsValid;
            }
            set
            {
                if (_IsValid != value)
                {
                    _IsValid = value;
                    OnPropertyChanged(() => IsValid);
                }
            }
        }
        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(() => ErrorMessage);
            }
        }
        public JedenWindowViewModel(string displayName)
        {
            DisplayName = displayName;
            IsValid = true;
        }
        #endregion

        #region Pomocniczy
        private void SaveAndClose()
        {
            Save();
            CloseWindow();
        }
        private void CloseWindow()
        {
            _window.Close();
        }
        private async void RefreshToken()
        {
            RefreshTokenModel refreshToken = new(GlobalData.RefreshToken);
            RefreshTokenModel newuser = await RequestHelper.SendRequestAsync<object, RefreshTokenModel>(URLs.REFRESH, HttpMethod.Post, refreshToken, GlobalData.AccessToken);
            GlobalData.AccessToken = newuser.AccessToken;
            GlobalData.RefreshToken = newuser.RefreshToken;
        }
        public abstract void Save();
        public async void EditSaveLogs(T oldvm, T newvm)
        {
            string olditem = JsonSerializer.Serialize(oldvm);
            string newitem = JsonSerializer.Serialize(newvm);
            LogCreateEditVM log = new();
            log.LogDate = DateTime.Now;
            log.Users = GlobalData.UserId;
            log.CreatedAt = DateTime.Now;
            log.CreatedBy = GlobalData.UserId;
            log.Description = $"Zmodyfikowano rekord w tabeli {DisplayName}. Stary rekord {olditem}. Nowy rekord {newitem}.";
            await RequestHelper.SendRequestAsync(URLs.LOG, HttpMethod.Post, log, GlobalData.AccessToken);
        }
        public async void NewSaveLogs(T newvm)
        {
            string newitem = JsonSerializer.Serialize(newvm);
            LogCreateEditVM log = new();
            log.LogDate = DateTime.Now;
            log.Users = GlobalData.UserId;
            log.CreatedAt = DateTime.Now;
            log.CreatedBy = GlobalData.UserId;
            log.Description = $"Utworzono rekord w tabeli {DisplayName}. Nowy rekord {newitem}.";
            await RequestHelper.SendRequestAsync(URLs.LOG, HttpMethod.Post, log, GlobalData.AccessToken);
        }

        #endregion

    }
}
