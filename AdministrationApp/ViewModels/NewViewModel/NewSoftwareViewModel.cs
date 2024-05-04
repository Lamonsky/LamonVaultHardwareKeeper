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
    class NewSoftwareViewModel : JedenViewModel<SoftwareCreateEditVM>
    {
        #region Commands
        private BaseCommand _ChooseLocationCommand;
        private BaseCommand _ChooseUserCommand;

        public ICommand ChooseLocationCommand
        {
            get
            {
                if (_ChooseLocationCommand == null)
                {
                    _ChooseLocationCommand = new BaseCommand(() => Messenger.Default.Send("ChooseLocation"));
                }
                return _ChooseLocationCommand;
            }
        }
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
        public NewSoftwareViewModel() : base("Nowe oprogramowanie")
        {
            item = new SoftwareCreateEditVM();
            Messenger.Default.Register<LocationVM>(this, getChosenLokacja);
            Messenger.Default.Register<UserVM>(this, getChosenUser);
        }
        public override async void Save()
        {
            await RequestHelper.SendRequestAsync(URLs.SOFTWARE, HttpMethod.Post, item, null);
            Messenger.Default.Send("SoftwareRefresh");
        }
        #endregion

        #region CommandsFunctions
        private void getChosenUser(UserVM vM)
        {
            item.Users = vM.Id;
            UserName = vM.FirstName + " " + vM.LastName + " " + vM.InternalNumber + " " + vM.Position;
        }

        private async void getStatusItems()
        {
            statusComboBoxItems = await RequestHelper.SendRequestAsync<object, List<StatusVM>>(URLs.STATUS, HttpMethod.Get, null, null);
        }

        private async void getProducentItems()
        {
            manufacturerComboBoxItems = await RequestHelper.SendRequestAsync<object, List<ManufacturerVM>>(URLs.MANUFACTURER, HttpMethod.Get, null, null);
        }

        private void getChosenLokacja(LocationVM vM)
        {
            item.LocationId = vM.Id;
            LokacjaName = vM.Name;
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
        public string? Name
        {
            get
            {
                return item.Name;
            }
            set
            {
                item.Name = value;
                OnPropertyChanged(() => Name);
            }
        }
        public int? LocationId
        {
            get
            {
                return item.LocationId;
            }
            set
            {
                item.LocationId = value;
                OnPropertyChanged(() => LocationId);
            }
        }
        public int? Status
        {
            get
            {
                return item.Status;
            }
            set
            {
                item.Status = value;
                OnPropertyChanged(() => Status);
            }
        }
        public int? Publisher
        {
            get
            {
                return item.Publisher;
            }
            set
            {
                item.Publisher= value;
                OnPropertyChanged(() => Publisher);
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
        private string _LokacjaName;
        public string LokacjaName
        {
            get
            {
                return _LokacjaName;
            }
            set
            {
                if (_LokacjaName == null)
                {
                    _LokacjaName = value;
                    OnPropertyChanged(() => LokacjaName);
                }
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
        private List<ManufacturerVM> _manufacturerComboBoxItems;
        public List<ManufacturerVM> manufacturerComboBoxItems
        {
            get
            {
                //jak lista....
                if (_manufacturerComboBoxItems == null)
                {
                    getProducentItems();
                }
                return _manufacturerComboBoxItems;
            }
            set
            {
                _manufacturerComboBoxItems = value;
                OnPropertyChanged(() => manufacturerComboBoxItems);
            }
        }
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
        #endregion
    }
}
