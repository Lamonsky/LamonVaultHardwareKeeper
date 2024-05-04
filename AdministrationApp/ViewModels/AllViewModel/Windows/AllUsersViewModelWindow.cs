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
    class AllUserViewModelWindow : WszystkieViewModel<UserVM>
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
        private UserVM _ChosenUser;
        public UserVM ChosenUser
        {
            set
            {
                if (_ChosenUser != value)
                {
                    _ChosenUser = value;
                    Messenger.Default.Send(_ChosenUser);
                    OnRequestClose(this, new EventArgs());
                }
            }
            get
            {
                return _ChosenUser;
            }
        }
        #endregion
        public AllUserViewModelWindow() : base("User")
        {
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            if (name == "UserRefresh")
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
            List = await RequestHelper.SendRequestAsync<object, List<UserVM>>(URLs.USER, HttpMethod.Get, null, null);
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
