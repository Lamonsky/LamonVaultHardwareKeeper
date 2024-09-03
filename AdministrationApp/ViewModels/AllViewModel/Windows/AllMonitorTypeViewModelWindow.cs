
using AdministrationApp.Helpers;
using Data;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Windows;

namespace AdministrationApp.ViewModels.AllViewModel
{
    class AllMonitorTypeViewModelWindow : WszystkieViewModel<MonitorTypeVM>
    {
        private Window _window;

        private MonitorTypeVM _ChosenItem;
        public MonitorTypeVM ChosenItem
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

        public AllMonitorTypeViewModelWindow(Window window) : base("MonitorType")
        {
            Messenger.Default.Register<string>(this, open);
            _window = window;
        }
        private void open(string name)
        {
            if (name == "MonitorTypeRefresh")
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
            throw new NotImplementedException();
        }

        public override List<string> GetComboBoxFilterList()
        {
            throw new NotImplementedException();
        }

        public override async void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<MonitorTypeVM>>(URLs.MONITORTYPE, HttpMethod.Get, null, GlobalData.AccessToken);
        }

        public async override void Remove()
        {
            await RequestHelper.SendRequestAsync(URLs.MONITORTYPE_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, GlobalData.AccessToken);
            load();
        }

        public override void send()
        {
            Messenger.Default.Send(ChosenItem);
            _window.Close();
        }
    }
}

