
using AdministrationApp.Helpers;
using Data;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Windows;

namespace AdministrationApp.ViewModels.AllViewModel
{
    class AllOperatingSystemViewModelWindow : WszystkieViewModel<OperatingSystemVM>
    {
        private Window _window;

        private OperatingSystemVM _ChosenItem;
        public OperatingSystemVM ChosenItem
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

        public AllOperatingSystemViewModelWindow(Window window) : base("OperatingSystem")
        {
            Messenger.Default.Register<string>(this, open);
            _window = window;
        }
        private void open(string name)
        {
            if (name == "OperatingSystemRefresh")
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
            List = await RequestHelper.SendRequestAsync<object, List<OperatingSystemVM>>(URLs.OPERATINGSYSTEM, HttpMethod.Get, null, null);
        }

        public async override void Remove()
        {
            await RequestHelper.SendRequestAsync(URLs.OPERATINGSYSTEM_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, null);
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

