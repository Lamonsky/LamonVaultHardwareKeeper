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

namespace AdministrationApp.ViewModels.AllViewModel
{
    class AllSimCardViewModel : WszystkieViewModel<SimCardsVM>
    {
        private SimCardsVM _ChosenItem;
        public SimCardsVM ChosenItem
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
        public AllSimCardViewModel() : base("Karty Sim")
        {
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            if (name == "SimCardRefresh")
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
            throw new NotImplementedException();
        }

        public override List<string> GetComboBoxFilterList()
        {
            return new List<string>();
        }

        public override List<string> GetComboBoxSortList()
        {
            return new List<string>();
        }

        public async override void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<SimCardsVM>>(URLs.SIMCARD, HttpMethod.Get, null, null);
        }

        public async override void Remove()
        {
            await RequestHelper.SendRequestAsync(URLs.SIMCARD_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, null);
            load();
        }

        public override void Sort()
        {
            throw new NotImplementedException();
        }

        public override void send()
        {
            throw new NotImplementedException();
        }
    }
}
