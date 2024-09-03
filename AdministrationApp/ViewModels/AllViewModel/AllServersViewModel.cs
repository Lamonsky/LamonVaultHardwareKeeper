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

namespace AdministrationApp.ViewModels.AllViewModel
{
    public class AllServersViewModel : WszystkieViewModel<ServerVM>
    {

        public AllServersViewModel() : base("Server")
        {
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            if (name == "ServerRefresh")
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
            List = await RequestHelper.SendRequestAsync<object, List<ServerVM>>(URLs.SERVER, HttpMethod.Get, null, null);
        }
        private ServerVM _ChosenItem;
        public ServerVM ChosenItem
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
        public override void Edit()
        {
            Messenger.Default.Send(DisplayName + "Edit/" + ChosenItem.Id);
        }

        public override async void Remove()
        {
            await RequestHelper.SendRequestAsync(URLs.SERVER_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, null);
            load();
        }

        public override void send()
        {
            throw new NotImplementedException();
        }
    }
}
