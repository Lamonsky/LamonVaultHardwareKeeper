using AdministrationApp.Helpers;
using AdministrationApp.ViewModels.NewViewModel;
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

namespace AdministrationApp.ViewModels.EditViewModel
{
    public class EditPageContentViewModel : JedenViewModel<PageContentCreateEditVM>
    {
        #region Konstruktor
        public EditPageContentViewModel(PageContentCreateEditVM vm) : base("EditPageContent")
        {
            oldItem = vm;
            item = vm;

        }
        public override async void Save()
        {
            EditSaveLogs(oldItem, item);
            await RequestHelper.SendRequestAsync(URLs.PAGECONTENT_ID.Replace("{id}", item.Id.ToString()), HttpMethod.Put, item, GlobalData.AccessToken);
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
