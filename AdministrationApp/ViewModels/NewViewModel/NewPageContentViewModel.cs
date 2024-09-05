using AdministrationApp.Helpers;
using Data;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdministrationApp.ViewModels.NewViewModel
{
    public class NewPageContentViewModel : JedenViewModel<PageContentCreateEditVM>
    {
        #region Konstruktor
        public NewPageContentViewModel() : base("NewPageContent")
        {
            item = new PageContentCreateEditVM();

        }
        public override async void Save()
        {
            NewSaveLogs(item);
            await RequestHelper.SendRequestAsync(URLs.PAGECONTENT, HttpMethod.Post, item, GlobalData.AccessToken);
            Messenger.Default.Send("PageContentRefresh");
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
        public string Title
        {
            get
            {
                return item.Title;
            }
            set
            {
                item.Title = value;
                OnPropertyChanged(() => Title);
            }
        }
        public string? Content
        {
            get
            {
                return item.Content;
            }
            set
            {
                item.Content = value;
                OnPropertyChanged(() => Content);
            }
        }       
        #endregion
    }
}
