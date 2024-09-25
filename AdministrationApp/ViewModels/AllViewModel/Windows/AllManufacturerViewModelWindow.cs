
using AdministrationApp.Helpers;
using Data;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Windows;

namespace AdministrationApp.ViewModels.AllViewModel
{
    class AllManufacturerViewModelWindow : WszystkieViewModel<ManufacturerVM>
    {
        private Window _window;
        public AllManufacturerViewModelWindow(Window window) : base("Producenci")
        {
            Messenger.Default.Register<string>(this, open);
            _window = window;
        }
        private void open(string name)
        {
            if (name == "ManufacturerRefresh")
            {
                load();
            }
        }
        public override void Edit()
        {
            try
            {
                Messenger.Default.Send(DisplayName+"Edit/"+ChosenItem.Id.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show("Nie wybrano obiektu");
            }
        }

        public override void Filter()
        {
            switch (FilterField)
            {
                default:
                    List = new List<ManufacturerVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Comment?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Nazwa":
                    List = new List<ManufacturerVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Opis":
                    List = new List<ManufacturerVM>(
                        List.Where(item =>
                            (item.Comment?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Status":
                    List = new List<ManufacturerVM>(
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
                "Opis",
                "Status",
            };
        }

        public override async void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<ManufacturerVM>>(URLs.MANUFACTURER, HttpMethod.Get, null, GlobalData.AccessToken);
        }

        public async override void Remove()
        {

            if (ChosenItem != null) await RequestHelper.SendRequestAsync(URLs.MANUFACTURER_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, GlobalData.AccessToken);
            load();
        }

        public override void send()
        {
            if(ChosenItem != null) Messenger.Default.Send(ChosenItem);
            _window.Close();
        }
    }
}

