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
    class AllStatusViewModelWindow : WszystkieViewModel<StatusVM>
    {
        private Window _window;
        public AllStatusViewModelWindow(Window window) : base("Status")
        {
            Messenger.Default.Register<string>(this, open);
            _window=window;
        }
        private void open(string name)
        {
            if (name == "StatusRefresh")
            {
                load();
            }
        }
        public override void Filter()
        {
            switch (FilterField)
            {
                default:
                    List = new List<StatusVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Comment?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Nazwa":
                    List = new List<StatusVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Opis":
                    List = new List<StatusVM>(
                        List.Where(item =>
                            (item.Comment?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
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
                "Opis"
            };
        }

        public override async void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<StatusVM>>(URLs.STATUS, HttpMethod.Get, null, GlobalData.AccessToken);
        }

        public override void Edit()
        {
            Messenger.Default.Send(DisplayName+"Edit/"+ChosenItem.Id.ToString());
        }

        public override async void Remove()
        {
            
            await RequestHelper.SendRequestAsync(URLs.STATUS_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, GlobalData.AccessToken);
            load();
        }

        public override void send()
        {
            Messenger.Default.Send(ChosenItem);
            _window.Close();
        }
    }
}
