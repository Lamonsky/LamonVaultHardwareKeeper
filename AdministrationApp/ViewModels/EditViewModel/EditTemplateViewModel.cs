using AdministrationApp.Helpers;
using AdministrationApp.ViewModels.NewViewModel;
using Data;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdministrationApp.ViewModels.EditViewModel
{
    public class EditTemplateViewModel : JedenViewModel<TemplatesVM>
    {
        #region Konstruktor
        public EditTemplateViewModel(TemplatesVM vm) : base("Szablony")
        {
            item = vm;
            oldItem = vm;
        }
        public override async void Save()
        {
            EditSaveLogs(oldItem, item);
            await RequestHelper.SendRequestAsync(URLs.TEMPLATE_ID.Replace("{id}", item.Id.ToString()), HttpMethod.Put, item, GlobalData.AccessToken);
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
