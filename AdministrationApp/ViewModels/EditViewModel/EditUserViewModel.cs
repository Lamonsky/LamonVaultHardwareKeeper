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
using AdministrationApp.ViewModels.NewViewModel;

namespace AdministrationApp.ViewModels.EditViewModel
{
    internal class EditUserViewModel : JedenViewModel<UserCreateEditVM>
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
        public EditUserViewModel(UserCreateEditVM vm) : base("Nowy użytkownik")
        {
            item = vm;
            oldItem = vm;
            if (item != null) setForeignKeys();
            Messenger.Default.Register<LocationVM>(this, getChosenLokacja);
            Messenger.Default.Register<PositionVM>(this, getPosition);
        }
        public override async void Save()
        {
            item.ModifiedAt = DateTime.Now;
            item.ModifiedBy = GlobalData.UserId;
            EditSaveLogs(oldItem, item);
            await RequestHelper.SendRequestAsync(
                URLs.USER_ID.Replace("{id}", item.Id.ToString()), 
                HttpMethod.Put, 
                item, 
                GlobalData.AccessToken);
            Messenger.Default.Send("UsersRefresh");
        }
        #endregion
        #region CommandsFunctions
        public async void setForeignKeys()
        {
            if (item.LocationId != null)
            {
                try
                {
                    LocationVM locationVM = await RequestHelper.SendRequestAsync<object, LocationVM>(
                    URLs.LOCATION_ID.Replace("{id}", item.LocationId.ToString()),
                    HttpMethod.Get,
                    null,
                    GlobalData.AccessToken
                );
                    LokacjaName = locationVM.Name;
                }
                catch
                {
                    LokacjaName = "Nieaktywna lokalizacja";
                }
                
            }

            if (item.Position != null)
            {
                try
                {
                    PositionVM positionVM = await RequestHelper.SendRequestAsync<object, PositionVM>(
                                        URLs.POSITION_ID.Replace("{id}", item.Position.ToString()),
                                        HttpMethod.Get,
                                        null,
                                        GlobalData.AccessToken
                                    );
                    PositionName = positionVM.Name;
                }
                catch
                {
                    PositionName = "Nieaktywne stanowisko";
                }
                
            }

        }

        private void getChosenLokacja(LocationVM vM)
        {
            item.LocationId = vM.Id;
            LokacjaName = vM.Name;
        }
        private void getPosition(PositionVM vM)
        {
            item.Position = vM.Id;
            PositionName = vM.Name;
        }

        #endregion
        #region Dane
        private string _PositionName;
        public string PositionName
        {
            get
            {
                return _PositionName;
            }
            set
            {
                if(_PositionName != value)
                {
                    _PositionName = value;
                    OnPropertyChanged(() => PositionName);
                }
            }
        }
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
                if (_LokacjaName != value)
                {
                    _LokacjaName = value;
                    OnPropertyChanged(() => LokacjaName);
                }
            }
        }
        #endregion
    }
}
