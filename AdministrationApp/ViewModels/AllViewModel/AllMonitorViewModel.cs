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
using AdministrationApp.ViewModels.NewViewModel;

namespace AdministrationApp.ViewModels.AllViewModel
{
    class AllMonitorViewModel : WszystkieViewModel<MonitorsVM>
    {
        public AllMonitorViewModel() : base("Monitory")
        {
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            if (name == "MonitorRefresh")
            {
                load();
            }
        }
        public override void Filter()
        {
            //throw new NotImplementedException();
        }

        public override List<string> GetComboBoxFilterList()
        {
            //throw new NotImplementedException();
            return new List<string>();
        }


        public override async void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<MonitorsVM>>(URLs.MONITORS, HttpMethod.Get, null, null);
        }

        private MonitorsVM _ChosenMonitor;
        public MonitorsVM ChosenMonitor
        {
            set
            {
                if (_ChosenMonitor != value)
                {
                    _ChosenMonitor = value;
                }
            }
            get
            {
                return _ChosenMonitor;
            }
        }
        public override void Edit()
        {
            Messenger.Default.Send(DisplayName + "Edit/" + ChosenMonitor.Id);
        }

        public override async void Remove()
        {
            await RequestHelper.SendRequestAsync(URLs.MONITORS_ID.Replace("{id}", ChosenMonitor.Id.ToString()), HttpMethod.Delete, ChosenMonitor, null);
            load();
        }

        public override void send()
        {
            throw new NotImplementedException();
        }
    }
}
