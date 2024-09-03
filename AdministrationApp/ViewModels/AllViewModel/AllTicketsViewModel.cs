﻿using AdministrationApp.Helpers;
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
    public class AllTicketsViewModel : WszystkieViewModel<TicketVM>
    {
        public AllTicketsViewModel() : base("Tickets")
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
            //throw new NotImplementedException();
        }

        public override List<string> GetComboBoxFilterList()
        {
            return new List<string>
            {
                "Wszędzie",
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
            List = await RequestHelper.SendRequestAsync<object, List<TicketVM>>(URLs.TICKET, HttpMethod.Get, null, GlobalData.AccessToken);
        }

        private TicketVM _ChosenItem;
        public TicketVM ChosenItem
        {
            set
            {
                if (_ChosenItem != value)
                {
                    _ChosenItem = value;
                }
            }
            get
            {
                return _ChosenItem;
            }
        }
        public override void Edit()
        {
            Messenger.Default.Send(DisplayName + "Edit/" + ChosenItem.Id);
        }

        public override async void Remove()
        {
            await RequestHelper.SendRequestAsync(URLs.TICKET_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, GlobalData.AccessToken);
            load();
        }

        public override void send()
        {
            throw new NotImplementedException();
        }
    }
}
