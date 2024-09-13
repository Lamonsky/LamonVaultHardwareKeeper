﻿using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;

namespace WebInterface.Models
{
    public class TicketCreateNewViewModel
    {
        public List<TicketCategoryVM> TicketCategories { get; set; }
        public List<TicketStatusVM> TicketStatuses { get; set; }
        public List<TicketTypeVM> TicketTypes { get; set; }
        public List<LocationVM> Location { get; set; }

        public TicketCreateEditVM Ticket { get; set; }



    }
}