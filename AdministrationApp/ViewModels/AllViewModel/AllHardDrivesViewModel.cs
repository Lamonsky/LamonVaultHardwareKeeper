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
using AdministrationApp.ViewModels.NewViewModel;

namespace AdministrationApp.ViewModels.AllViewModel
{
    class AllHardDrivesViewModel : WszystkieViewModel<HardDriveVM>
    {
        public AllHardDrivesViewModel() : base("HardDrives")
        {
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            if (name == "HardDrivesRefresh")
            {
                load();
            }
        }
        public override void Filter()
        {
            switch(FilterField)
            {
                default:
                    List = new List<HardDriveVM>(
                        List.Where(item =>
                            (item.Manufacturer?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Model?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Capacity?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Server?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Serwer":
                    List = new List<HardDriveVM>(
                        List.Where(item =>
                            (item.Server?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Pojemność":
                    List = new List<HardDriveVM>(
                        List.Where(item =>
                            (item.Capacity?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Status":
                    List = new List<HardDriveVM>(
                        List.Where(item =>
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Model":
                    List = new List<HardDriveVM>(
                        List.Where(item =>
                            (item.Model?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Producent":
                    List = new List<HardDriveVM>(
                        List.Where(item =>
                            (item.Manufacturer?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;                
            }
        }

        public override List<string> GetComboBoxFilterList()
        {
            return new List<string>
            {
                
                "Producent",
                "Model",
                "Pojemność",
                "Serwer",
                "Status"
            };
        }


        public override async void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<HardDriveVM>>(URLs.HARDDRIVE, HttpMethod.Get, null, GlobalData.AccessToken);
        }
        public override void Edit()
        {
            Messenger.Default.Send(DisplayName + "Edit/" + ChosenItem.Id);
        }

        public override async void Remove()
        {
            
            await RequestHelper.SendRequestAsync(URLs.HARDDRIVE_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, null);
            load();
        }

        public override void send()
        {
            throw new NotImplementedException();
        }
    }
}
