using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AdministrationApp.Helpers;
using Data;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;

namespace AdministrationApp.ViewModels.AllViewModel
{
    public class AllComputerViewModel : WszystkieViewModel<ComputersVM>
    {
        
        public AllComputerViewModel() : base("Komputery")
        {
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            if (name == "ComputersRefresh")
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

        public override List<string> GetComboBoxSortList()
        {
            //throw new NotImplementedException();
            return new List<string>();
        }

        public override async void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<ComputersVM>>(URLs.COMPUTERS, HttpMethod.Get, null, null);
        }

        public override void Sort()
        {
            //throw new NotImplementedException();
        }
        private ComputersVM _ChosenComputer;
        public ComputersVM ChosenComputer
        {
            set
            {
                if(_ChosenComputer != value)
                {
                    _ChosenComputer = value;
                }
            }
            get
            {
                return _ChosenComputer;
            }
        }
        public override void Edit()
        {
            Messenger.Default.Send(DisplayName + "Edit/" + ChosenComputer.Id);
        }

        public override async void Remove()
        {            
            await RequestHelper.SendRequestAsync(URLs.COMPUTERS_ID.Replace("{id}", ChosenComputer.Id.ToString()), HttpMethod.Delete, ChosenComputer, null);
            load();
        }

        public override void send()
        {
            throw new NotImplementedException();
        }
    }
}
