
using AdministrationApp.Helpers;
using Data;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Windows;

namespace AdministrationApp.ViewModels.AllViewModel
{
    class AllDeviceTypeViewModelWindow : WszystkieViewModel<DeviceTypeVM>
    {
        private Window _window;
        public AllDeviceTypeViewModelWindow(Window window) : base("Rodzaje urz�dze�")
        {
            Messenger.Default.Register<string>(this, open);
            _window = window;
        }
        private void open(string name)
        {
            if (name == "DeviceTypeRefresh")
            {
                load();
            }
        }
        public override void Edit()
        {
            Messenger.Default.Send(DisplayName + "Edit/" + ChosenItem.Id.ToString());
        }

        public override void Filter()
        {
            switch (FilterField)
            {
                default:
                    List = new List<DeviceTypeVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Comment?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Nazwa":
                    List = new List<DeviceTypeVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Opis":
                    List = new List<DeviceTypeVM>(
                        List.Where(item =>
                            (item.Comment?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Status":
                    List = new List<DeviceTypeVM>(
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
            List = await RequestHelper.SendRequestAsync<object, List<DeviceTypeVM>>(URLs.DEVICETYPE, HttpMethod.Get, null, GlobalData.AccessToken);
        }

        public async override void Remove()
        {

            if (ChosenItem != null) await RequestHelper.SendRequestAsync(URLs.DEVICETYPE_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, GlobalData.AccessToken);
            load();
        }

        public override void send()
        {
            if(ChosenItem != null) Messenger.Default.Send(ChosenItem);
            _window.Close();
        }
    }
}

