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
    public class LogsViewModel : WszystkieViewModel<LogVM>
    {
        public LogsViewModel() : base("Logi")
        {

        }

        public override void Edit()
        {
            
        }

        public override void Filter()
        {
            switch (FilterField)
            {
                default:
                    List = new List<LogVM>(
                        List.Where(item =>
                            (item.Users.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.LogDate.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Description.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)                            
                        ).ToList()
                    );
                    break;
                case "Użytkownik":
                    List = new List<DevicesVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Data Logu":
                    List = new List<DevicesVM>(
                        List.Where(item =>
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Opis":
                    List = new List<DevicesVM>(
                        List.Where(item =>
                            (item.DeviceType?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;                
            }
        }

        public override List<string> GetComboBoxFilterList()
        {
            return new List<string>
            {

                "Użytkownik",
                "Data Logu",
                "Opis"
            };
        }

        public async override void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<DevicesVM>>(URLs.DEVICE, HttpMethod.Get, null, GlobalData.AccessToken);
        }

        public async override void Remove()
        {
            await RequestHelper.SendRequestAsync(URLs.DEVICE_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, GlobalData.AccessToken);
            load();
        }

        public override void send()
        {
            throw new NotImplementedException();
        }
    }
}
