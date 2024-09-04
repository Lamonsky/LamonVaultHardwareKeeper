using AdministrationApp.Helpers;
using Data.Computers.SelectVMs;
using Data;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using AdministrationApp.Views.AllWindows;
using System.Windows;

namespace AdministrationApp.ViewModels.AllViewModel.Windows
{
    class AllUserViewModelWindow : WszystkieViewModel<UserVM>
    {
        private Window _window;
        #region Commands
        private UserVM _ChosenUser;
        public UserVM ChosenUser
        {
            set
            {
                if (_ChosenUser != value)
                {
                    _ChosenUser = value;
                }
            }
            get
            {
                return _ChosenUser;
            }
        }
        #endregion
        public AllUserViewModelWindow() : base("User")
        {
            Messenger.Default.Register<string>(this, open);
        }
        public AllUserViewModelWindow(Window window) : base("User")
        {
            Messenger.Default.Register<string>(this, open);
            _window = window;
        }
        private void open(string name)
        {
            if (name == "UserRefresh")
            {
                load();
            }
        }
        public override void Filter()
        {
            switch (FilterField)
            {
                default:
                    List = new List<UserVM>(
                        List.Where(item =>
                            (item.Usersname?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.FirstName?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.LastName?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Location?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Email?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Phone1?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Phone2?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.InternalNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Position?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Login":
                    List = new List<UserVM>(
                        List.Where(item =>
                            (item.Usersname?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Imię":
                    List = new List<UserVM>(
                        List.Where(item =>
                            (item.FirstName?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Nazwisko":
                    List = new List<UserVM>(
                        List.Where(item =>
                            (item.LastName?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Lokalizacja":
                    List = new List<UserVM>(
                        List.Where(item =>
                            (item.Location?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Email":
                    List = new List<UserVM>(
                        List.Where(item =>
                            (item.Email?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Numer telefonu 1":
                    List = new List<UserVM>(
                        List.Where(item =>
                            (item.Phone1?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Numer telefonu 2":
                    List = new List<UserVM>(
                        List.Where(item =>
                            (item.Phone2?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Numer wewnętrzny":
                    List = new List<UserVM>(
                        List.Where(item =>
                            (item.InternalNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Pozycja":
                    List = new List<UserVM>(
                        List.Where(item =>
                            (item.Position?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
            }
        }

        public override List<string> GetComboBoxFilterList()
        {
            return new List<string>
            {
                
                "Login",
                "Imię",
                "Nazwisko",
                "Lokalizacja",
                "Email",
                "Numer telefonu 1",
                "Numer telefonu 2",
                "Numer wewnętrzny",
                "Pozycja"
            };
        }

        public override async void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<UserVM>>(URLs.USER, HttpMethod.Get, null, GlobalData.AccessToken);
        }

        public override void Edit()
        {
            Messenger.Default.Send(DisplayName+"Edit/"+ChosenUser.Id.ToString());
        }

        public override async void Remove()
        {
            await RequestHelper.SendRequestAsync(URLs.USER_ID.Replace("{id}", ChosenUser.Id.ToString()), HttpMethod.Delete, ChosenUser, GlobalData.AccessToken);
            load();
        }

        public override void send()
        {
            Messenger.Default.Send(ChosenUser);
            _window.Close();
        }
    }
}
