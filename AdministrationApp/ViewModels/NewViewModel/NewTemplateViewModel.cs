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

namespace AdministrationApp.ViewModels.NewViewModel
{
    public class NewTemplateViewModel : JedenViewModel<TemplatesVM>
    {
        #region Konstruktor
        public NewTemplateViewModel() : base("Szablony")
        {
            item = new TemplatesVM();
        }
        public override async void Save()
        {
            NewSaveLogs(item);
            await RequestHelper.SendRequestAsync(URLs.TEMPLATE, HttpMethod.Post, item, GlobalData.AccessToken);
            Messenger.Default.Send("SzablonyRefresh");
        }

        #endregion


        #region Dane
        public int Id
        {
            get
            {
                return item.Id;
            }
        }
        public string? Name
        {
            get
            {
                return item.Name;
            }
            set
            {
                item.Name = value;
                OnPropertyChanged(() => Name);
            }
        }
        public string? HTMLContent
        {
            get
            {
                return item.HTMLContent;
            }
            set
            {
                item.HTMLContent = value;
                OnPropertyChanged(() => HTMLContent);
            }
        }
        #endregion
    }
}
