
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

        private ManufacturerVM _ChosenItem;
        public ManufacturerVM ChosenItem
        {
            set
            {
                if (_ChosenItem != value)
                {
                    _ChosenItem = value;
                }
            }
            get
            {
                return _ChosenItem;
            }
        }

        public AllManufacturerViewModelWindow(Window window) : base("Manufacturer")
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
            throw new NotImplementedException();
        }

        public override List<string> GetComboBoxFilterList()
        {
            throw new NotImplementedException();
        }

        public override async void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<ManufacturerVM>>(URLs.MANUFACTURER, HttpMethod.Get, null, null);
        }

        public async override void Remove()
        {
            await RequestHelper.SendRequestAsync(URLs.MANUFACTURER_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, null);
            load();
        }

        public override void send()
        {
            Messenger.Default.Send(ChosenItem);
            _window.Close();
        }
    }
}

