
using AdministrationApp.Helpers;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using Data;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using AdministrationApp.ViewModels.NewViewModel;

namespace AdministrationApp.ViewModels.EditViewModel.Windows
{
    public class EditLocationViewModel : JedenViewModel<LocationCreateEditVM>
    {
        private Window _window;
        private BaseCommand? _ChooseStatusCommand;
        public ICommand ChooseStatusCommand
        {
            get
            {
                if (_ChooseStatusCommand == null)
                {
                    _ChooseStatusCommand = new BaseCommand(() => Messenger.Default.Send("ChooseStatus"));
                }
                return _ChooseStatusCommand;
            }
        }
        #region Konstruktor
        public EditLocationViewModel(Window window, LocationCreateEditVM vm) : base("Location")
        {
            item = vm;
            oldItem = vm;
            setForeignKeys();
            Messenger.Default.Register<StatusVM>(this, getStatus);
            _window = window;
        }
        private async void setForeignKeys()
        {
            StatusVM vm = await RequestHelper.SendRequestAsync<object, StatusVM>(URLs.STATUS_ID.Replace("{id}", item.Status.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            StatusName = vm.Name;
        }
        public override async void Save()
        {
            item.ModifiedAt = DateTime.Now;
            item.ModifiedBy = GlobalData.UserId;
            EditSaveLogs(oldItem, item);
            await RequestHelper.SendRequestAsync(URLs.LOCATION_ID.Replace("{id}", item.Id.ToString()), HttpMethod.Put, item, GlobalData.AccessToken);
            Messenger.Default.Send("LocationRefresh");
            _window.Close();
        }
        #endregion
        #region CommandsFunctions

        private async void getStatus(StatusVM vm)
        {
            item.Status = vm.Id;
            StatusName = vm.Name;
        }

        #endregion
        #region Dane
        private string _StatusName;
        public string StatusName
        {
            get
            {
                return _StatusName;
            }
            set
            {
                if(value != _StatusName)
                {
                    _StatusName = value;
                    OnPropertyChanged(() => StatusName);
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
        public string? Address
        {
            get
            {
                return item.Address;
            }
            set
            {
                item.Address  = value;
                OnPropertyChanged(() => Address);
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
        public string? PostalCode
        {
            get
            {
                return item.PostalCode;
            }
            set
            {
                item.PostalCode    = value;
                OnPropertyChanged(() => PostalCode);
            }
        }
        public string? City
        {
            get
            {
                return item.City;
            }
            set
            {
                item.City  = value;
                OnPropertyChanged(() => City);
            }
        }
        public string? Country
        {
            get
            {
                return item.Country;
            }
            set
            {
                item.Country   = value;
                OnPropertyChanged(() => Country);
            }
        }
        public string? BuildingNumber
        {
            get
            {
                return item.BuildingNumber;
            }
            set
            {
                item.BuildingNumber  = value;
                OnPropertyChanged(() => BuildingNumber);
            }
        }public string? RoomNumber
        {
            get
            {
                return item.RoomNumber;
            }
            set
            {
                item.RoomNumber   = value;
                OnPropertyChanged(() => RoomNumber);
            }
        }
        #endregion
    }
}
