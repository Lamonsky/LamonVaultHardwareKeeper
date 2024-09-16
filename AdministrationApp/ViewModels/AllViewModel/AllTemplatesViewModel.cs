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
    public class AllTemplatesViewModel : WszystkieViewModel<TemplatesVM>
    {
        public AllTemplatesViewModel() : base("Szablony")
        {
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            if (name == "SzablonyRefresh")
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
            List = new List<TemplatesVM>(
                List.Where(item =>
                    (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                    (item.HTMLContent?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                ).ToList()
            );
        }

        public override List<string> GetComboBoxFilterList()
        {
            return new List<string>();
        }

        public async override void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<TemplatesVM>>(URLs.TEMPLATE, HttpMethod.Get, null, GlobalData.AccessToken);
        }

        public async override void Remove()
        {

            if (ChosenItem != null) await RequestHelper.SendRequestAsync(URLs.TEMPLATE_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, GlobalData.AccessToken);
            load();
        }

        public override void send()
        {
        }
    }
}
