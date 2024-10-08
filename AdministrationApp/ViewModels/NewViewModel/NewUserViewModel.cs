﻿using AdministrationApp.Helpers;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using Data;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Windows.Input;
using Data.Helpers;
using System.Windows;

namespace AdministrationApp.ViewModels.NewViewModel
{
    public class NewUserViewModel : JedenViewModel<UserCreateEditVM>
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
            Messenger.Default.Register<PositionVM>(this, getPosition);
        }
        private void ValidatePassword()
        {
            ErrorMessage = string.Empty;
            IsValid = PasswordValidator.ValidatePassword(Password);
            if(!IsValid)
            {
                ErrorMessage = ("Nie wystarczająco silne hasło. Wymagania: conajmniej 8 znaków, conajmniej 1 mała litera, conajmniej 1 wielka litera, conajmniej 1 znak specjalny");
            }
            else
            {
                ErrorMessage = null;
            }
        }
        public override async void Save()
        {
            if (!IsValid)
            {
                MessageBox.Show("Uzupełnij błędnie wypełnione pola");
                return;
            }
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = GlobalData.UserId;
            NewSaveLogs(item);
            RestApiUsers user = new();
            user.Email = item.Email;
            user.Password = item.Password;
            try
            {
                await RequestHelper.SendRequestAsync(URLs.REGISTER, HttpMethod.Post, user, GlobalData.AccessToken);
                await RequestHelper.SendRequestAsync<object>(URLs.IDENTITY_ADD_USER_TO_MEMBER_ROLE.Replace("{email}", user.Email), HttpMethod.Get, null, GlobalData.AccessToken);
                await RequestHelper.SendRequestAsync(URLs.USER, HttpMethod.Post, item, GlobalData.AccessToken);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Messenger.Default.Send("UsersRefresh");
        }
        #endregion
        #region CommandsFunctions

        private void getPosition(PositionVM vM)
        {
            item.Position = vM.Id;
            PositionName = vM.Name;
        }

        private void getChosenLokacja(LocationVM vM)
        {
            item.LocationId = vM.Id;
            LokacjaName = vM.Name;
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
                if (_PositionName != value)
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
                ValidatePassword();
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
    }
}
