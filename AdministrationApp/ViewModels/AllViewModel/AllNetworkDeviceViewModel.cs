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
using System.Windows.Media;

namespace AdministrationApp.ViewModels.AllViewModel
{
    class AllNetworkDeviceViewModel : WszystkieViewModel<NetworkDeviceVM>
    {
        private NetworkDeviceVM _ChosenNetworkDevice;
        public NetworkDeviceVM ChosenNetworkDevice
        {
            set
            {
                if (_ChosenNetworkDevice != value)
                {
                    _ChosenNetworkDevice = value;
                }
            }
            get
            {
                return _ChosenNetworkDevice;
            }
        }
        public AllNetworkDeviceViewModel() : base("Urządzenia sieciowe") {
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            if (name == "NetworkDeviceRefresh")
            {
                load();
            }
        }
        public override void Edit()
        {
            Messenger.Default.Send(DisplayName + "Edit/" + ChosenNetworkDevice.Id);
        }
        public override void Filter()
        {
            throw new NotImplementedException();
        }
        public override List<string> GetComboBoxFilterList()
        {
            throw new NotImplementedException();
        }
        
        public async override void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<NetworkDeviceVM>>(URLs.NETWORKDEVICE, HttpMethod.Get, null, null);
        }
        public async override void Remove()
        {
            await RequestHelper.SendRequestAsync(URLs.NETWORKDEVICE_ID.Replace("{id}", ChosenNetworkDevice.Id.ToString()), HttpMethod.Delete, ChosenNetworkDevice, null);
            load();
        }
       
        public override void send()
        {
            throw new NotImplementedException();
        }
    }
}
