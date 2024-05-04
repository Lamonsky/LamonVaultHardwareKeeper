using AdministrationApp.Helpers;
using AdministrationApp.ViewModels.NewViewModel;
using Data;
using Data.Helpers;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdministrationApp.ViewModels
{
    public class LoginWindowViewModel : JedenViewModel<RestApiUsers>
    {
        public LoginWindowViewModel() : base("Logowanie")
        {
            item = new RestApiUsers();
        }

        public event EventHandler OnRequestClose;
        public void CloseWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        #region Dane

        public string? Email
        {
            get
            {
                return item.Email;
            }
            set
            {
                item.Email = value;
                OnPropertyChanged(() => Email);
            }
        }
        public string? Password
        {
            get
            {
                return item.Password;
            }
            set
            {
                item.Password = value;
                OnPropertyChanged(() => Password);
            }
        }

        #endregion

        public async override void Save()
        {
            await RequestHelper.SendRequestAsync(URLs.LOGIN, HttpMethod.Post, item, null);
            Messenger.Default.Send(item);
            
        }
    }
}
