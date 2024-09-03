﻿using AdministrationApp.Helpers;
using Data;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Windows;

namespace AdministrationApp.ViewModels.AllViewModel
{
    class AllPrinterTypeViewModelWindow : WszystkieViewModel<PrinterTypeVM>
    {
        private Window _window;
        private PrinterTypeVM _ChosenItem;
        public PrinterTypeVM ChosenItem
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

        public AllPrinterTypeViewModelWindow(Window window) : base("Rodzaje drukarek")
        {
            Messenger.Default.Register<string>(this, open);
            _window = window;
            window.Title = "Rodzaje drukarek";
        }
        private void open(string name)
        {
            if (name == "PrinterTypeRefresh")
            {
                load();
            }
        }
        public override void Edit()
        {
            Messenger.Default.Send(DisplayName+"Edit/"+ChosenItem.Id.ToString());
        }

        public override void Filter()
        {
            throw new NotImplementedException();
        }

        public override List<string> GetComboBoxFilterList()
        {
            throw new NotImplementedException();
        }

        public override async void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<PrinterTypeVM>>(URLs.PRINTERTYPE, HttpMethod.Get, null, GlobalData.AccessToken);
        }

        public async override void Remove()
        {
            await RequestHelper.SendRequestAsync(URLs.PRINTERTYPE_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, GlobalData.AccessToken);
            load();
        }

        public override void send()
        {
            Messenger.Default.Send(ChosenItem);
            _window.Close();
        }
    }
}
