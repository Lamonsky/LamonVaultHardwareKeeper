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
using Data.Helpers;

namespace AdministrationApp.ViewModels.NewViewModel
{
    public class NewTicketViewModel : JedenViewModel<TicketCreateEditVM>
    {
        #region Commands
        private BaseCommand _ChooseLocationCommand;
        private BaseCommand _ChooseUserCommand;
        private BaseCommand _ChooseTicketStatusCommand;
        private BaseCommand _ChooseTypeCommand;
        private BaseCommand _ChooseCategoryCommand;
        private BaseCommand _ChooseOwnerCommand;
        private BaseCommand _ChooseUserDeviceCommand;

        public ICommand ChooseUserDeviceCommand
        {
            get
            {
                if (_ChooseUserDeviceCommand == null)
                {
                    _ChooseUserDeviceCommand = new BaseCommand(() => Messenger.Default.Send("ChooseUserDevice"));
                }
                return _ChooseUserDeviceCommand;
            }
        }

        public ICommand ChooseTypeCommand
        {
            get
            {
                if (_ChooseTypeCommand == null)
                {
                    _ChooseTypeCommand = new BaseCommand(() => Messenger.Default.Send("ChooseTicketType"));
                }
                return _ChooseTypeCommand;
            }
        }
        public ICommand ChooseCategoryCommand
        {
            get
            {
                if (_ChooseCategoryCommand == null)
                {
                    _ChooseCategoryCommand = new BaseCommand(() => Messenger.Default.Send("ChooseTicketCategory"));
                }
                return _ChooseCategoryCommand;
            }
        }
        public ICommand ChooseOwnerCommand
        {
            get
            {
                if (_ChooseOwnerCommand == null)
                {
                    _ChooseOwnerCommand = new BaseCommand(() => Messenger.Default.Send("ChooseTicketOwner"));
                }
                return _ChooseOwnerCommand;
            }
        }
        public ICommand ChooseTicketStatusCommand
        {
            get
            {
                if (_ChooseTicketStatusCommand == null)
                {
                    _ChooseTicketStatusCommand = new BaseCommand(() => Messenger.Default.Send("ChooseTicketStatus"));
                }
                return _ChooseTicketStatusCommand;
            }
        }
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
        public NewTicketViewModel() : base("Nowe zgłoszenie")
        {
            item = new TicketCreateEditVM();
            Messenger.Default.Register<UserVM>(this, getChosenUser);
            Messenger.Default.Register<TicketStatusVM>(this, getStatus);
            Messenger.Default.Register<TicketCategoryVM>(this, getTicketCategory);
            Messenger.Default.Register<TicketTypeVM>(this, getTicketType);
            Messenger.Default.Register<TechnicianVM>(this, getTechnichian);
            Messenger.Default.Register<UserDevicesModel>(this, getUserDevices);
             
        }
        public override async void Save()
        {
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = GlobalData.UserId;
            NewSaveLogs(item);
            await RequestHelper.SendRequestAsync(URLs.TICKET, HttpMethod.Post, item, GlobalData.AccessToken);
            Messenger.Default.Send("TicketsRefresh");
        }
        #endregion
        #region CommandsFunctions
        private void getStatus(TicketStatusVM vm)
        {
            item.StatusId = vm.Id;
            StatusName = vm.Name;
        }
        private void getUserDevices(UserDevicesModel vm)
        {
            item.Device = vm.Name;
            DeviceName = vm.Name;
        }
        private void getChosenUser(UserVM vM)
        {
            item.User = vM.Id;
            UserName = vM.FirstName + " " + vM.LastName + " " + vM.InternalNumber + " " + vM.Position;
            item.LocationId = vM.LocationId;
            LocationName = vM.Location;
        }

        private void getTicketCategory(TicketCategoryVM vM)
        {
            item.Category = vM.Id;
            CategoryName = vM.Name;
        }
        private void getTicketType(TicketTypeVM vm)
        {
            item.Type = vm.Id;
            TicketTypeName = vm.Name;
        }
        private void getTechnichian(TechnicianVM vm)
        {
            item.Owner = vm.Id;
            OwnerName = vm.Users;
        }
        #endregion
        #region Dane
        private string _LocationName;
        private string _OwnerName;
        private string _TicketTypeName;
        private string _CategoryName;
        private string _UserName;
        private string _StatusName;
        private string _DeviceName;

        public string? DeviceName
        {
            get
            {
                return _DeviceName;
            }
            set
            {
                if (value != _DeviceName)
                {
                    _DeviceName = value;
                    OnPropertyChanged(() => DeviceName);
                }
            }
        }
        public string? LocationName
        {
            get
            {
                return _LocationName;
            }
            set
            {
                if (value != _LocationName)
                {
                    _LocationName = value;
                    OnPropertyChanged(() => LocationName);
                }
            }
        }

        public string? OwnerName
        {
            get
            {
                return _OwnerName;
            }
            set
            {
                if (value != _OwnerName)
                {
                    _OwnerName = value;
                    OnPropertyChanged(() => OwnerName);
                }
            }
        }

        public string? TicketTypeName
        {
            get
            {
                return _TicketTypeName;
            }
            set
            {
                if (value != _TicketTypeName)
                {
                    _TicketTypeName = value;
                    OnPropertyChanged(() => TicketTypeName);
                }
            }
        }

        public string? CategoryName
        {
            get
            {
                return _CategoryName;
            }
            set
            {
                if (value != _CategoryName)
                {
                    _CategoryName = value;
                    OnPropertyChanged(() => CategoryName);
                }
            }
        }

        public string? UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                if (value != _UserName)
                {
                    _UserName = value;
                    OnPropertyChanged(() => UserName);
                }
            }
        }

        public string? StatusName
        {
            get
            {
                return _StatusName;
            }
            set
            {
                if (value != _StatusName)
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
        public string? Name {
            get
            {
                return item.Name;
            }
            set
            {
                if(value != Name)
                {
                    item.Name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }
        public string? Device {
            get
            {
                return item.Device;
            }
            set
            {
                if(value != Device)
                {
                    item.Device = value;
                    OnPropertyChanged(() => Device);
                }
            }
        }

        public int? Type
        {
            get
            {
                return item.Type;
            }
            set
            {
                if (value != Type)
                {
                    item.Type = value;
                    OnPropertyChanged(() => Type);
                }
            }
        }

        public int? Category
        {
            get
            {
                return item.Category;
            }
            set
            {
                if (value != Category)
                {
                    item.Category = value;
                    OnPropertyChanged(() => Category);
                }
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
                if (value != StatusId)
                {
                    item.StatusId = value;
                    OnPropertyChanged(() => StatusId);
                }
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
                if (value != LocationId)
                {
                    item.LocationId = value;
                    OnPropertyChanged(() => LocationId);
                }
            }
        }

        public int? User
        {
            get
            {
                return item.User;
            }
            set
            {
                if (value != User)
                {
                    item.User = value;
                    OnPropertyChanged(() => User);
                }
            }
        }

        public int? Owner
        {
            get
            {
                return item.Owner;
            }
            set
            {
                if (value != Owner)
                {
                    item.Owner = value;
                    OnPropertyChanged(() => Owner);
                }
            }
        }
        #endregion
    }
}
