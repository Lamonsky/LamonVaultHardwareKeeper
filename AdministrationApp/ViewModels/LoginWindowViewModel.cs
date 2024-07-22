using AdministrationApp.Helpers;
using AdministrationApp.ViewModels.NewViewModel;
using AdministrationApp.Views;
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
                LoggedUser user = await RequestHelper.SendRequestAsync<object, LoggedUser>(URLs.LOGIN, HttpMethod.Post, item, null);
                if(user != null)
                {
                    MainWindow window = new MainWindow();
                    var viewModel = new MainWindowViewModel();
                    window.DataContext = viewModel;
                    Messenger.Default.Send(item);
                    window.Show();
                    _window.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Błędne logowanie";
            }
            

        }
    }
}
