using AdministrationApp.Helpers;
using Data;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdministrationApp.ViewModels.NewViewModel
{
    class NewSimCardViewModel : JedenViewModel<SimCardsCreateEditVM>
    {
        #region Commands
        private BaseCommand _ChooseUserCommand;
        public ICommand ChooseUserCommand
        {
            get
            {
                if (_ChooseUserCommand == null)
                {
                    _ChooseUserCommand = new BaseCommand(() => Messenger.Default.Send("ChooseUser"));
                }
                return _ChooseUserCommand;
            }
        }
        #endregion
        #region Konstruktor
        public NewSimCardViewModel() : base("Nowa Karta Sim")
        {
            item = new SimCardsCreateEditVM();
            Messenger.Default.Register<UserVM>(this, getChosenUser);
        }
        public override async void Save()
        {
            await RequestHelper.SendRequestAsync(URLs.SIMCARD, HttpMethod.Post, item, null);
            Messenger.Default.Send("SimCardRefresh");
        }
        #endregion
        #region CommandsFunctions
        private void getChosenUser(UserVM vM)
        {
            item.Users = vM.Id;
            UserName = vM.FirstName + " " + vM.LastName + " " + vM.InternalNumber + " " + vM.Position;
        }
        private async void getComponentItems()
        {
            componentComboBoxItems = await RequestHelper.SendRequestAsync<object, List<SimComponentVM>>(URLs.SIMCOMPONENT, HttpMethod.Get, null, null);

        }

        private async void getStatusItems()
        {
            statusComboBoxItems = await RequestHelper.SendRequestAsync<object, List<StatusVM>>(URLs.STATUS, HttpMethod.Get, null, null);
        }

        #endregion
        #region Dane
        public int Id
        {
            get
            {
                return item.Id;
            }
        }
        public int? StatusId
        {
            get
            {
                return item.StatusId;
            }
            set
            {
                item.StatusId = value;
                OnPropertyChanged(() => StatusId);
            }
        }
        public int? Component
        {
            get
            {
                return item.Component;
            }
            set
            {
                item.Component = value;
                OnPropertyChanged(() => Component);
            }
        }
        public string? SerialNumber
        {
            get
            {
                return item.SerialNumber;
            }
            set
            {
                item.SerialNumber = value;
                OnPropertyChanged(() => SerialNumber);
            }
        }
        public string? PinCode
        {
            get
            {
                return item.PinCode;
            }
            set
            {
                item.PinCode = value;
                OnPropertyChanged(() => PinCode);
            }
        }
        public string? PukCode
        {
            get
            {
                return item.PukCode;
            }
            set
            {
                item.PukCode = value;
                OnPropertyChanged(() => PukCode);
            }
        }
        public string? InventoryNumber
        {
            get
            {
                return item.InventoryNumber;
            }
            set
            {
                item.InventoryNumber = value;
                OnPropertyChanged(() => InventoryNumber);
            }
        }
        public string? PhoneNumber
        {
            get
            {
                return item.PhoneNumber;
            }
            set
            {
                item.PhoneNumber = value;
                OnPropertyChanged(() => PhoneNumber);
            }
        }
        public int? Users
        {
            get
            {
                return item.Users;
            }
            set
            {
                item.Users = value;
                OnPropertyChanged(() => Users);
            }
        }
        private string _UserName;
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                if (_UserName == null)
                {
                    _UserName = value;
                    OnPropertyChanged(() => UserName);
                }
            }
        }
        #endregion
        #region Foreign Keys
        private List<StatusVM> _statusComboBoxItems;
        public List<StatusVM> statusComboBoxItems
        {
            get
            {
                //jak lista....
                if (_statusComboBoxItems == null)
                {
                    getStatusItems();
                }
                return _statusComboBoxItems;
            }
            set
            {
                _statusComboBoxItems = value;
                OnPropertyChanged(() => statusComboBoxItems);
            }
        }
        private List<SimComponentVM> _componentComboBoxItems;
        public List<SimComponentVM> componentComboBoxItems
        {
            get
            {
                //jak lista....
                if (_componentComboBoxItems == null)
                {
                    getComponentItems();
                }
                return _componentComboBoxItems;
            }
            set
            {
                _componentComboBoxItems = value;
                OnPropertyChanged(() => componentComboBoxItems);
            }
        }
        #endregion
    }
}
