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
using System.Windows;
using System.Windows.Input;

namespace AdministrationApp.ViewModels.AllViewModel.Windows
{
    public class UsersRolesViewModel : WszystkieViewModel<UsersWithRolesVM>
    {
        private Window _window;
        private BaseCommand _AddToAdminCommand;
        private BaseCommand _RemoveFromAdminCommand;
        public ICommand AddToAdminCommand
        {
            get
            {
                if (_AddToAdminCommand == null)
                {
                    _AddToAdminCommand = new BaseCommand(() => AddToAdmin());
                }
                return _AddToAdminCommand;
            }
        }
        public ICommand RemoveFromAdminCommand
        {
            get
            {
                if (_RemoveFromAdminCommand == null)
                {
                    _RemoveFromAdminCommand = new BaseCommand(() => RemoveFromAdmin());
                }
                return _RemoveFromAdminCommand;
            }
        }


        public UsersRolesViewModel(Window window) : base("Role użytkowników")
        {
            Messenger.Default.Register<string>(this, open);
            _window = window;
        }
        private void open(string name)
        {
            if (name == "UsersWithRolesRefresh")
            {
                load();
            }
        }
        private async void AddToAdmin()
        {
            if(ChosenItem != null)
            {
                await RequestHelper.SendRequestAsync<object>(URLs.IDENTITY_ADD_USER_TO_ADMIN_ROLE.Replace("{email}", ChosenItem.Email), HttpMethod.Get, ChosenItem, GlobalData.AccessToken);
                load();
            }
        }
        private async void RemoveFromAdmin()
        {
            if(ChosenItem != null)
            {
                await RequestHelper.SendRequestAsync<object>(URLs.IDENTITY_REMOVE_USER_FROM_ADMIN_ROLE.Replace("{email}", ChosenItem.Email), HttpMethod.Get, ChosenItem, GlobalData.AccessToken);
                load();
            }
        }
        

        public override void Edit()
        {
        }

        public override void Filter()
        {
            List = new List<UsersWithRolesVM>(
                List.Where(item =>
                    (item.Email.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase))
                ).ToList());
        }

        public override List<string> GetComboBoxFilterList()
        {
            return new List<string>();
        }

        public override async void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<UsersWithRolesVM>>(URLs.IDENTITY_CHECK_USER_ROLE, HttpMethod.Get, null, GlobalData.AccessToken);
            foreach(var item in List)
            {
                foreach(var user in item.Roles) {
                    item.Fixedroles += user.ToString() + ", ";
                }
            }
        }

        public async override void Remove()
        {
        }

        public override void send()
        {
            Messenger.Default.Send(ChosenItem);
            _window.Close();
        }
    }
}
