using AdministrationApp.Helpers;
using Data;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdministrationApp.ViewModels.AllViewModel
{
    class AllSoftwareViewModel : WszystkieViewModel<SoftwaresVM>
    {
        private Window _window;
        public AllSoftwareViewModel() : base("Oprogramowanie")
        {
            Messenger.Default.Register<string>(this, open);
        }
        public AllSoftwareViewModel(Window window) : base("Oprogramowanie")
        {
            _window = window;
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            if (name == "SoftwareRefresh")
            {
                load();
            }
        }
        public override void Edit()
        {
            Messenger.Default.Send(DisplayName + "Edit/" + ChosenItem.Id);
        }

        public override void Filter()
        {
            switch(FilterField)
            {
                default:
                    List = new List<SoftwaresVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Location?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Publisher?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Users?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Nazwa":
                    List = new List<SoftwaresVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Lokalizacja":
                    List = new List<SoftwaresVM>(
                        List.Where(item =>
                            (item.Location?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Dostawca":
                    List = new List<SoftwaresVM>(
                        List.Where(item =>
                            (item.Publisher?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Osoba odpowiedzialna":
                    List = new List<SoftwaresVM>(
                        List.Where(item =>
                            (item.Users?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Status":
                    List = new List<SoftwaresVM>(
                        List.Where(item =>
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
            }
        }

        public override List<string> GetComboBoxFilterList()
        {
            return new List<string>
            {
                
                "Nazwa",
                "Lokalizacja",
                "Dostawca",
                "Osoba odpowiedzialna",
                "Status"
            };
        }

        public async override void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<SoftwaresVM>>(URLs.SOFTWARE, HttpMethod.Get, null, GlobalData.AccessToken);
        }

        public async override void Remove()
        {
            RemoveSaveLogs(ChosenItem);
            await RequestHelper.SendRequestAsync(URLs.SOFTWARE_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, null);
            load();
        }

        public override void send()
        {
            Messenger.Default.Send(ChosenItem);
            _window.Close();
        }
    }
}
