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
    class AllSoftwareViewModel : WszystkieViewModel<SoftwaresVM>
    {
        private SoftwaresVM _ChosenSoftware;
        public SoftwaresVM ChosenSoftware
        {
            set
            {
                if (_ChosenSoftware != value)
                {
                    _ChosenSoftware = value;
                }
            }
            get
            {
                return _ChosenSoftware;
            }
        }
        public AllSoftwareViewModel() : base("Oprogramowanie")
        {
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            if (name == "SoftwareRefresh")
            {
                load();
            }
        }
        public override void Edit()
        {
            Messenger.Default.Send(DisplayName + "Edit/" + ChosenSoftware.Id);
        }

        public override void Filter()
        {
            throw new NotImplementedException();
        }

        public override List<string> GetComboBoxFilterList()
        {
            return new List<string>();
        }

        public async override void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<SoftwaresVM>>(URLs.SOFTWARE, HttpMethod.Get, null, GlobalData.AccessToken);
        }

        public async override void Remove()
        {
            await RequestHelper.SendRequestAsync(URLs.SOFTWARE_ID.Replace("{id}", ChosenSoftware.Id.ToString()), HttpMethod.Delete, ChosenSoftware, null);
            load();
        }

        public override void send()
        {
            throw new NotImplementedException();
        }
    }
}
