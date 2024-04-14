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

namespace AdministrationApp.ViewModels
{
    class AllComputerViewModel : WszystkieViewModel<ComputersVM>
    {
        public AllComputerViewModel() : base("Komputery")
        {
            //load();
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
    }
}
