using AdministrationApp.Helpers;
using AdministrationApp.ViewModels.AllViewModel;
using Data;
using Data.Helpers;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdministrationApp.ViewModels.AllViewModel.Windows
{
    public class AllUserDevicesViewModel : WszystkieViewModel<UserDevicesModel>
    {
        private Window _window;
        public AllUserDevicesViewModel(Window window) : base("UserDevices")
        {
            Messenger.Default.Register<string>(this, open);
            _window = window;
        }
        private void open(string name)
        {
            if (name == "UserDevicesRefresh")
            {
                load();
            }
        }
        public override void Edit()
        {
        }

        public override void Filter()
        {
            List = new List<UserDevicesModel>(
                List.Where(item =>
                    (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                    (item.User?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) 
                ).ToList()
            );                   
        }

        public override List<string> GetComboBoxFilterList()
        {
            return new List<string>();
        }
        public override async void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<UserDevicesModel>>(URLs.USER_DEVICES_ALL, HttpMethod.Get, null, GlobalData.AccessToken);
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
