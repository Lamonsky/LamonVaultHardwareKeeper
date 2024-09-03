using AdministrationApp.Helpers;
using AdministrationApp.ViewModels.NewViewModel;
using AdministrationApp.Views;
using Data;
using Data.Computers.SelectVMs;
using Data.Helpers;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AdministrationApp.ViewModels
{
    public class LoginWindowViewModel : JedenViewModel<RestApiUsers>
    {
        private Window _window;
        public LoginWindowViewModel(Window window) : base("Logowanie")
        {
            item = new RestApiUsers();
            _window = window;
        }

        private BaseCommand _LoginCommand;

        public ICommand LoginCommand
        {
            get
            {
                if (_LoginCommand == null)
                {
                    _LoginCommand = new BaseCommand(() => Save());
                }
                return _LoginCommand;
            }
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
        #endregion

        public async override void Save()
        {
            try
            {
                LoggedUser user = await RequestHelper.SendRequestAsync<object, LoggedUser>(URLs.LOGIN, HttpMethod.Post, item, GlobalData.AccessToken);
                UserInAdminRole adminuser = await RequestHelper.SendRequestAsync<object, UserInAdminRole>(URLs.IDENTITY_CHECK_USER_ADMIN_ROLE.Replace("{email}", item.Email), HttpMethod.Get, null, GlobalData.AccessToken);
                if (adminuser.IsInRole)
                {
                    GlobalData.AccessToken = user.AccessToken;
                    GlobalData.RefreshToken = user.RefreshToken;
                    GlobalData.Email = item.Email;
                    List<UserVM> userVMs = await RequestHelper.SendRequestAsync<object, List<UserVM>>(URLs.USER, HttpMethod.Get, null, GlobalData.AccessToken);
                    UserVM loggeduser = userVMs.Where(item => item.Email == GlobalData.Email).First();
                    GlobalData.UserId = loggeduser.Id;                    
                    MainWindow window = new MainWindow();
                    var viewModel = new MainWindowViewModel();
                    window.DataContext = viewModel;
                    window.Show();
                    _window.Close();
                }
                else
                {
                    ErrorMessage = "Nie masz uprawnień do zalogowania. Skontaktuj się z administratorem";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Błędne logowanie";
            }
            

        }
    }
}
