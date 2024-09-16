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
    public class MyTicketsViewModel : WszystkieViewModel<TicketVM>
    {
        public MyTicketsViewModel() : base("Moje zgłoszenia")
        {
            Messenger.Default.Register<string>(this, open);
        }        
        private void open(string name)
        {
            if (name == "TicketsRefresh")
            {
                load();
            }
        }
        public override void Filter()
        {
            switch (FilterField)
            {
                default:
                    List = new List<TicketVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Type?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Category?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Location?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.User?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Owner?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Email?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Treść zgłoszenia":
                    List = new List<TicketVM>(
                       List.Where(item =>
                           (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                       ).ToList()
                       );
                    break;
                case "Typ zgłoszenia":
                    List = new List<TicketVM>(
                       List.Where(item =>
                           (item.Type?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                       ).ToList()
                       );
                    break;
                case "Kategoria":
                    List = new List<TicketVM>(
                       List.Where(item =>
                           (item.Category?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                       ).ToList()
                       );
                    break;
                case "Status":
                    List = new List<TicketVM>(
                       List.Where(item =>
                           (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                       ).ToList()
                       );
                    break;
                case "Lokalizacja":
                    List = new List<TicketVM>(
                       List.Where(item =>
                           (item.Location?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                       ).ToList()
                       );
                    break;
                case "Użytkownik":
                    List = new List<TicketVM>(
                       List.Where(item =>
                           (item.User?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                       ).ToList()
                       );
                    break;
                case "E-Mail":
                    List = new List<TicketVM>(
                       List.Where(item =>
                           (item.Email?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                       ).ToList()
                       );
                    break;
                case "Właściciel":
                    List = new List<TicketVM>(
                       List.Where(item =>
                           (item.Owner?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                       ).ToList()
                       );
                    break;
            }
            
        }

        public override List<string> GetComboBoxFilterList()
        {
            return new List<string>
            {
                
                "Treść zgłoszenia",
                "Typ zgłoszenia",
                "Kategoria",
                "Status",
                "Lokalizacja",
                "Użytkownik",
                "E-Mail",
                "Właściciel"
            };
        }

        public override async void load()
        {
            TechnicianVM technician = await RequestHelper.SendRequestAsync<object, TechnicianVM>(URLs.TECHNICIAN_USER_ID.Replace("{id}", GlobalData.UserId.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            List = await RequestHelper.SendRequestAsync<object, List<TicketVM>>(URLs.TICKET_OWNER_ID.Replace("{id}", technician.Id.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
        }
        public override void Edit()
        {
            Messenger.Default.Send(DisplayName + "Edit/" + ChosenItem.Id);
        }

        public override async void Remove()
        {

            if (ChosenItem != null) await RequestHelper.SendRequestAsync(URLs.TICKET_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, GlobalData.AccessToken);
            load();
        }

        public override void send()
        {
        }
    }
}
