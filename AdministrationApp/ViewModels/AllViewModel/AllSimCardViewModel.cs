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
    class AllSimCardViewModel : WszystkieViewModel<SimCardsVM>
    {
        public AllSimCardViewModel() : base("Karty Sim")
        {
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            if (name == "SimCardRefresh")
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
            switch(FilterField)
            {
                default:
                    List = new List<SimCardsVM>(
                        List.Where(item =>
                            (item.PinCode?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.PukCode?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Component?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.SerialNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.InventoryNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.PhoneNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Users?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Kod PIN":
                    List = new List<SimCardsVM>(
                        List.Where(item =>
                            (item.PinCode?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Kod PUK":
                    List = new List<SimCardsVM>(
                        List.Where(item =>
                            (item.PukCode?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Komponent":
                    List = new List<SimCardsVM>(
                        List.Where(item =>
                            (item.Component?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Numer seryjny":
                    List = new List<SimCardsVM>(
                        List.Where(item =>
                            (item.SerialNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Numer wewnętrzny":
                    List = new List<SimCardsVM>(
                        List.Where(item =>
                            (item.InventoryNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Numer telefonu":
                    List = new List<SimCardsVM>(
                        List.Where(item =>
                            (item.PhoneNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Status":
                    List = new List<SimCardsVM>(
                        List.Where(item =>
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Użytkownik":
                    List = new List<SimCardsVM>(
                        List.Where(item =>
                            (item.Users?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
            }
        }

        public override List<string> GetComboBoxFilterList()
        {
            return new List<string>
            {
                
                "Kod PIN",
                "Kod PUK",
                "Komponent",
                "Numer seryjny",
                "Numer wewnętrzny",
                "Numer telefonu",
                "Status",
                "Użytkownik"
            };
        }



        public async override void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<SimCardsVM>>(URLs.SIMCARD, HttpMethod.Get, null, GlobalData.AccessToken);
        }

        public async override void Remove()
        {
            
            await RequestHelper.SendRequestAsync(URLs.SIMCARD_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, GlobalData.AccessToken);
            load();
        }

        public override void send()
        {
            throw new NotImplementedException();
        }
    }
}
