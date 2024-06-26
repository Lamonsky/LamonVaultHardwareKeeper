﻿using AdministrationApp.Helpers;
using Data.Computers.SelectVMs;
using Data;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Windows;

namespace AdministrationApp.ViewModels.AllViewModel.Windows
{
    class AllSimCardViewModelWindow : WszystkieViewModel<SimCardsVM>
    {
        public event EventHandler OnRequestClose;
        public void CloseWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
        #region Commands
        private SimCardsVM _ChosenItem;
        public SimCardsVM ChosenItem
        {
            set
            {
                if (_ChosenItem != value)
                {
                    _ChosenItem = value;
                    Messenger.Default.Send(_ChosenItem);
                    OnRequestClose(this, new EventArgs());
                }
            }
            get
            {
                return _ChosenItem;
            }
        }
        #endregion
        public AllSimCardViewModelWindow() : base("Karty SIM")
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
        public override void Filter()
        {
            //throw new NotImplementedException();
        }

        public override List<string> GetComboBoxFilterList()
        {
            //throw new NotImplementedException();
            return new List<string>();
        }

        public override List<string> GetComboBoxSortList()
        {
            //throw new NotImplementedException();
            return new List<string>();
        }

        public override async void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<SimCardsVM>>(URLs.SIMCARD, HttpMethod.Get, null, null);
        }

        public override void Sort()
        {
            //throw new NotImplementedException();
        }

        public override void Edit()
        {
            throw new NotImplementedException();
        }

        public override void Remove()
        {
            throw new NotImplementedException();
        }

        public override void send()
        {
            throw new NotImplementedException();
        }
    }
}
