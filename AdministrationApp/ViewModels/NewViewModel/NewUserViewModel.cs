using AdministrationApp.Helpers;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using Data;
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
    internal class NewUserViewModel : JedenViewModel<UserCreateEditVM>
    {
        #region Commands
        private BaseCommand _ChooseLocationCommand;
        private BaseCommand _ChoosePositionCommand;

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
        public ICommand ChoosePositionCommand
        {
            get
            {
                if (_ChoosePositionCommand == null)
                {
                    _ChoosePositionCommand = new BaseCommand(() => Messenger.Default.Send("ChoosePosition"));
                }
                return _ChoosePositionCommand;
            }
        }
        #endregion
        #region Konstruktor
        public NewUserViewModel() : base("Nowy użytkownik")
        {
            item = new UserCreateEditVM();
            Messenger.Default.Register<LocationVM>(this, getChosenLokacja);
        }
        public override async void Save()
        {
            await RequestHelper.SendRequestAsync(URLs.USER, HttpMethod.Post, item, null);
            Messenger.Default.Send("UsersRefresh");
        }
        #endregion
        #region CommandsFunctions

        private async void getPositionItems()
        {
            positionComboBoxItems = await RequestHelper.SendRequestAsync<object, List<PositionVM>>(URLs.POSITION, HttpMethod.Get, null, null);

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
        public string? Usersname
        {
            get
            {
                return item.Usersname;
            }
            set
            {
                item.Usersname = value;
                OnPropertyChanged(() => Usersname);
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
        public int? Position
        {
            get
            {
                return item.Position;
            }
            set
            {
                item.Position = value;
                OnPropertyChanged(() => Position);
            }
        }
        public string? FirstName
        {
            get
            {
                return item.FirstName;
            }
            set
            {
                item.FirstName = value;
                OnPropertyChanged(() => FirstName);
            }
        }
        public string? LastName
        {
            get
            {
                return item.LastName;
            }
            set
            {
                item.LastName = value;
                OnPropertyChanged(() => LastName);
            }
        }
        public string? Password
        {
            get
            {
                return item.Password;
            }
            set
            {
                item.Password = value;
                OnPropertyChanged(() => Password);
            }
        }
        public string? Email
        {
            get
            {
                return item.Email;
            }
            set
            {
                item.Email = value;
                OnPropertyChanged(() => Email);
            }
        }
        public bool? IsActive
        {
            get
            {
                return item.IsActive;
            }
            set
            {
                item.IsActive = value;
                OnPropertyChanged(() => IsActive);
            }
        }
        public string? Phone1
        {
            get
            {
                return item.Phone1;
            }
            set
            {
                item.Phone1 = value;
                OnPropertyChanged(() => Phone1);
            }
        }
        public string? Phone2
        {
            get
            {
                return item.Phone2;
            }
            set
            {
                item.Phone2 = value;
                OnPropertyChanged(() => Phone2);
            }
        }
        public string? InternalNumber
        {
            get
            {
                return item.InternalNumber;
            }
            set
            {
                item.InternalNumber = value;
                OnPropertyChanged(() => InternalNumber);
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
        #endregion
        #region Foreign Keys
        private List<PositionVM> _positionComboBoxItems;
        public List<PositionVM> positionComboBoxItems
        {
            get
            {
                //jak lista....
                if (_positionComboBoxItems == null)
                {
                    getPositionItems();
                }
                return _positionComboBoxItems;
            }
            set
            {
                _positionComboBoxItems = value;
                OnPropertyChanged(() => positionComboBoxItems);
            }
        }
        #endregion
    }
}
