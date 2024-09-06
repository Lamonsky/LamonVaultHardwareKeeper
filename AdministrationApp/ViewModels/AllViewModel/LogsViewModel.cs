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
                            (item.Users.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase)) ||
                            (item.Description.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase))                            
                        ).ToList()
                    );
                    break;
                case "Użytkownik":
                    List = new List<LogVM>(
                        List.Where(item =>
                            (item.Users?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Opis":
                    List = new List<LogVM>(
                        List.Where(item =>
                            (item.Description?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
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
                "Opis"
            };
        }

        public async override void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<LogVM>>(URLs.LOG, HttpMethod.Get, null, GlobalData.AccessToken);
        }

        public async override void Remove()
        {

        }

        public override void send()
        {
        }
    }
}
