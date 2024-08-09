
using AdministrationApp.Helpers;
using Data;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Windows;

namespace AdministrationApp.ViewModels.AllViewModel
{
    class AllRackCabinetTypeViewModelWindow : WszystkieViewModel<RackCabinetTypeVM>
    {
        private Window _window;

        private RackCabinetTypeVM _ChosenItem;
        public RackCabinetTypeVM ChosenItem
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

        public AllRackCabinetTypeViewModelWindow(Window window) : base("RackCabinetType")
        {
            Messenger.Default.Register<string>(this, open);
            _window = window;
        }
        private void open(string name)
        {
            if (name == "RackCabinetTypeRefresh")
            {
                load();
            }
        }
        public override void Edit()
        {
            throw new NotImplementedException();
        }

        public override void Filter()
        {
            throw new NotImplementedException();
        }

        public override List<string> GetComboBoxFilterList()
        {
            throw new NotImplementedException();
        }

        public override List<string> GetComboBoxSortList()
        {
            throw new NotImplementedException();
        }

        public override async void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<RackCabinetTypeVM>>(URLs.RACKCABINETTYPE, HttpMethod.Get, null, null);
        }

        public async override void Remove()
        {
            await RequestHelper.SendRequestAsync(URLs.RACKCABINETTYPE_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, null);
            load();
        }

        public override void Sort()
        {
            throw new NotImplementedException();
        }

        public override void send()
        {
            Messenger.Default.Send(ChosenItem);
            _window.Close();
        }
    }
}
