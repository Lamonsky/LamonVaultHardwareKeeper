using AdministrationApp.Helpers;
using Data;
using Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AdministrationApp.ViewModels.NewViewModel
{
    //
    public abstract class JedenViewModel<T> : WorkspaceViewModel
    {
        #region Command
        private BaseCommand _SaveAndCloseCommand;
        //ta komenda bedzie podpieta pod przycisk Zapisz i Zamknik
        public ICommand SaveAndCloseCommand
        {
            get
            {
                if (_SaveAndCloseCommand == null)
                    //kom....
                    _SaveAndCloseCommand = new BaseCommand(SaveAndClose);
                return _SaveAndCloseCommand;
            }
        }
        #endregion

        #region Konstruktor
        protected T item;
        public JedenViewModel(string displayName)
        {
            DisplayName = displayName;
        }
        #endregion

        #region Pomocniczy
        private void SaveAndClose()
        {
            //dodać wysyłanie do api jsona
            Save();            
            OnRequestClose();
            RefreshToken();

        }
        private async void RefreshToken()
        {
            RefreshTokenModel refreshToken = new(GlobalData.RefreshToken);
            RefreshTokenModel newuser = await RequestHelper.SendRequestAsync<object, RefreshTokenModel>(URLs.REFRESH, HttpMethod.Post, refreshToken, GlobalData.AccessToken);
            GlobalData.AccessToken = newuser.AccessToken;
            GlobalData.RefreshToken = newuser.RefreshToken;
        }
        public abstract void Save();

        #endregion

    }
}
