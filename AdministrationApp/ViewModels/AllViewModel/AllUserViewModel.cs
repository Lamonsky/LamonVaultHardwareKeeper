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

namespace AdministrationApp.ViewModels.AllViewModel
{
    internal class AllUserViewModel : WszystkieViewModel<UserVM>
    {
        public AllUserViewModel() : base("Użytkownicy")
        {
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            if (name == "UsersRefresh")
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
            Messenger.Default.Send(DisplayName + "Edit/" + ChosenItem.Id);
        }

        public override async void Remove()
        {
            RemoveSaveLogs(ChosenItem);
            await RequestHelper.SendRequestAsync(URLs.USER_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, GlobalData.AccessToken);
            load();
        }

        public override void send()
        {
            throw new NotImplementedException();
        }
    }
}
