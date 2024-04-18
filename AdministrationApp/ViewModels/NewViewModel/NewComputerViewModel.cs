using AdministrationApp.Helpers;
using Data;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using ProjektPDAB.Models.EntitiesForView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Documents;
using System.Windows.Input;

namespace AdministrationApp.ViewModels.NewViewModel
{
    public class NewComputerViewModel : JedenViewModel<ComputersCreateEditVM>
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
        public NewComputerViewModel() : base("Nowy komputer")
        {
            item = new ComputersCreateEditVM();
            Messenger.Default.Register<LocationVM>(this, getChosenLokacja);
            Messenger.Default.Register<UserVM>(this, getChosenUser);
        }
        public override async void Save()
        {
            await RequestHelper.SendRequestAsync(URLs.COMPUTERS, HttpMethod.Post, item, null);
            Messenger.Default.Send("ComputersRefresh");
        }
        #endregion
        #region CommandsFunctions
        private void getChosenUser(UserVM vM)
        {
            item.UserId = vM.Id;
            UserName = vM.FirstName + " " + vM.LastName + " " + vM.InternalNumber + " " + vM.Position;
        }

        private async void getOSItems()
        {
            osComboBoxItems = await RequestHelper.SendRequestAsync<object, List<OperatingSystemVM>>(URLs.OS, HttpMethod.Get, null, null);
        }

        private async void getComputerModelItems()
        {
            computerModelComboBoxItems = await RequestHelper.SendRequestAsync<object, List<ComputerModelVM>>(URLs.COMPUTERMODEL, HttpMethod.Get, null, null);

        }

        private async void getComputerTypeItems()
        {
            computerTypeComboBoxItems = await RequestHelper.SendRequestAsync<object, List<ComputerTypeVM>>(URLs.COMPUTERTYPE, HttpMethod.Get, null, null);
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
        public int? ComputerTypeId
        {
            get
            {
                return item.ComputerTypeId;
            }
            set
            {
                item.ComputerTypeId = value;
                OnPropertyChanged(() => ComputerTypeId);
            }
        }
        public int? ManufacturerId
        {
            get
            {
                return item.ManufacturerId;
            }
            set
            {
                item.ManufacturerId = value;
                OnPropertyChanged(() => ManufacturerId);
            }
        }
        public int? ComputerModelId
        {
            get
            {
                return item.ComputerModelId;
            }
            set
            {
                item.ComputerModelId = value;
                OnPropertyChanged(() => ComputerModelId);
            }
        }
        public string? Processor
        {
            get
            {
                return item.Processor;
            }
            set
            {
                item.Processor = value;
                OnPropertyChanged(() => Processor);
            }
        }
        public string? Ram
        {
            get
            {
                return item.Ram;
            }
            set
            {
                item.Ram = value;
                OnPropertyChanged(() => Ram);
            }
        }
        public string? Disk
        {
            get
            {
                return item.Disk;
            }
            set
            {
                item.Disk = value;
                OnPropertyChanged(() => Disk);
            }
        }
        public string? GraphicsCard
        {
            get
            {
                return item.GraphicsCard;
            }
            set
            {
                item.GraphicsCard = value;
                OnPropertyChanged(() => GraphicsCard);
            }
        }
        public int? OperatingSystemId
        {
            get
            {
                return item.OperatingSystemId;
            }
            set
            {
                item.OperatingSystemId = value;
                OnPropertyChanged(() => OperatingSystemId);
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
        public int? UserId
        {
            get
            {
                return item.UserId;
            }
            set
            {
                item.UserId = value;
                OnPropertyChanged(() => UserId);
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
                if( _LokacjaName == null)
                {
                    _LokacjaName = value;
                    OnPropertyChanged(() =>  LokacjaName);
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
        private List<OperatingSystemVM> _osComboBoxItems;
        public List<OperatingSystemVM> osComboBoxItems
        {
            get
            {
                //jak lista....
                if (_osComboBoxItems == null)
                {
                    getOSItems();
                }
                return _osComboBoxItems;
            }
            set
            {
                _osComboBoxItems = value;
                OnPropertyChanged(() => osComboBoxItems);
            }
        }
        private List<ComputerTypeVM> _computerTypeComboBoxItems;
        public List<ComputerTypeVM> computerTypeComboBoxItems
        {
            get
            {
                //jak lista....
                if (_computerTypeComboBoxItems == null)
                {
                    getComputerTypeItems();
                }
                return _computerTypeComboBoxItems;
            }
            set
            {
                _computerTypeComboBoxItems = value;
                OnPropertyChanged(() => computerTypeComboBoxItems);
            }
        }
        private List<ComputerModelVM> _computerModelComboBoxItems;
        public List<ComputerModelVM> computerModelComboBoxItems
        {
            get
            {
                //jak lista....
                if (_computerModelComboBoxItems == null)
                {
                    getComputerModelItems();
                }
                return _computerModelComboBoxItems;
            }
            set
            {
                _computerModelComboBoxItems = value;
                OnPropertyChanged(() => computerModelComboBoxItems);
            }
        }
        #endregion
    }
}
