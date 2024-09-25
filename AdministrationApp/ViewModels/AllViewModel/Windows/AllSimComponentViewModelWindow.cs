
using AdministrationApp.Helpers;
using Data;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Windows;

namespace AdministrationApp.ViewModels.AllViewModel
{
    class AllSimComponentViewModelWindow : WszystkieViewModel<SimComponentVM>
    {
        private Window _window;

        public AllSimComponentViewModelWindow(Window window) : base("Komponent SIM")
        {
            Messenger.Default.Register<string>(this, open);
            _window = window;
        }
        private void open(string name)
        {
            if (name == "SimComponentRefresh")
            {
                load();
            }
        }
        public override void Edit()
        {
            Messenger.Default.Send(DisplayName+"Edit/"+ChosenItem.Id.ToString());
        }

        public override void Filter()
        {
            switch (FilterField)
            {
                default:
                    List = new List<SimComponentVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Type?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Manufacturer?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Nazwa":
                    List = new List<SimComponentVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Producent":
                    List = new List<SimComponentVM>(
                        List.Where(item =>
                            (item.Manufacturer?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Status":
                    List = new List<SimComponentVM>(
                        List.Where(item =>
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Typ":
                    List = new List<SimComponentVM>(
                        List.Where(item =>
                            (item.Type?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
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
                "Producent",
                "Status",
                "Typ"
            };
        }

        public override async void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<SimComponentVM>>(URLs.SIMCOMPONENT, HttpMethod.Get, null, GlobalData.AccessToken);
        }

        public async override void Remove()
        {

            if (ChosenItem != null) await RequestHelper.SendRequestAsync(URLs.SIMCOMPONENT_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, GlobalData.AccessToken);
            load();
        }
        public override void send()
        {
            if(ChosenItem != null) Messenger.Default.Send(ChosenItem);
            _window.Close();
        }
    }
}

