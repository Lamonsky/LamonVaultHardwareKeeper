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
using AdministrationApp.Views.AllWindows;
using System.Windows;

namespace AdministrationApp.ViewModels.AllViewModel.Windows
{
    class AllStatusViewModelWindow : WszystkieViewModel<StatusVM>
    {
        private Window _window;
        public event EventHandler OnRequestClose;
        public void CloseWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
        #region Commands
        private StatusVM _ChosenItem;
        public StatusVM ChosenItem
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
        #endregion
        public AllStatusViewModelWindow(Window window) : base("Status")
        {
            Messenger.Default.Register<string>(this, open);
            _window=window;
        }
        private void open(string name)
        {
            if (name == "StatusRefresh")
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

        public override async void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<StatusVM>>(URLs.STATUS, HttpMethod.Get, null, null);
        }

        public override void Edit()
        {
            Messenger.Default.Send(DisplayName+"Edit/"+ChosenItem.Id.ToString());
        }

        public override async void Remove()
        {
            await RequestHelper.SendRequestAsync(URLs.STATUS_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, null);
            load();
        }

        public override void send()
        {
            Messenger.Default.Send(ChosenItem);
            _window.Close();
        }
    }
}
