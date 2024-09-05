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
    public class AllPageContentViewModel : WszystkieViewModel<PageContentVM>
    {
        public AllPageContentViewModel() : base("PageContent")
        {
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            if (name == "PageContentRefresh")
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
            switch (FilterField)
            {
                default:
                    List = new List<PageContentVM>(
                        List.Where(item =>
                            (item.Title.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase)) ||
                            (item.Content.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase))                            
                        ).ToList()
                    );
                    break;
                case "Tytuł":
                    List = new List<PageContentVM>(
                        List.Where(item =>
                            (item.Title?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Zawartość":
                    List = new List<PageContentVM>(
                        List.Where(item =>
                            (item.Content?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;                
            }
        }

        public override List<string> GetComboBoxFilterList()
        {
            return new List<string>
            {

                "Tytuł",
                "Zawartość"
            };
        }

        public async override void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<PageContentVM>>(URLs.PAGECONTENT, HttpMethod.Get, null, GlobalData.AccessToken);
        }

        public async override void Remove()
        {
            await RequestHelper.SendRequestAsync(URLs.PAGECONTENT_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, null);
            load();
        }

        public override void send()
        {
            throw new NotImplementedException();
        }
    }
}
