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

namespace AdministrationApp.ViewModels.AllViewModel
{
    internal class AllUserViewModel : WszystkieViewModel<UserVM>
    {
        public AllUserViewModel() : base("Użytkownicy")
        {
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            if (name == "UsersRefresh")
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
            List = await RequestHelper.SendRequestAsync<object, List<UserVM>>(URLs.USER, HttpMethod.Get, null, GlobalData.AccessToken);
        }
        private UserVM _ChosenUser;
        public UserVM ChosenUser
        {
            set
            {
                if (_ChosenUser != value)
                {
                    _ChosenUser = value;
                }
            }
            get
            {
                return _ChosenUser;
            }
        }
        public override void Edit()
        {
            Messenger.Default.Send(DisplayName + "Edit/" + ChosenUser.Id);
        }

        public override async void Remove()
        {
            await RequestHelper.SendRequestAsync(URLs.USER_ID.Replace("{id}", ChosenUser.Id.ToString()), HttpMethod.Delete, ChosenUser, null);
            load();
        }

        public override void send()
        {
            throw new NotImplementedException();
        }
    }
}
