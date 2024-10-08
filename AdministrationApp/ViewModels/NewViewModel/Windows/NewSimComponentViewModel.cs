
using AdministrationApp.Helpers;
using Data;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using Data.Computers.SelectVMs;
using Data.Computers.CreateEditVMs;

namespace AdministrationApp.ViewModels.NewViewModel.Windows
{
    public class NewSimComponentViewModel : JedenWindowViewModel<SimComponentCreateEditVM>
    {
        #region Commands
        private BaseCommand? _ChooseStatusCommand;
        private BaseCommand? _ChooseManufacturerCommand;
        private BaseCommand? _ChooseSimComponentTypeCommand;
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
        public ICommand ChooseManufacturerCommand
        {
            get
            {
                if (_ChooseManufacturerCommand == null)
                {
                    _ChooseManufacturerCommand = new BaseCommand(() => Messenger.Default.Send("ChooseManufacturer"));
                }
                return _ChooseManufacturerCommand;
            }
        }
        public ICommand ChooseSimComponentTypeCommand
        {
            get
            {
                if (_ChooseSimComponentTypeCommand == null)
                {
                    _ChooseSimComponentTypeCommand = new BaseCommand(() => Messenger.Default.Send("ChooseSimComponentType"));
                }
                return _ChooseSimComponentTypeCommand;
            }
        }
        #endregion
        
        #region Konstruktor
        public NewSimComponentViewModel(Window window) : base("Nowy komponent karty SIM")
        {
            item = new SimComponentCreateEditVM();
            Messenger.Default.Register<StatusVM>(this, getStatus);
            Messenger.Default.Register<SimComponentTypeVM>(this, getSimComponentType);
            Messenger.Default.Register<ManufacturerVM>(this, getManufacturer);
            _window = window;
        }
        public override async void Save()
        {
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = GlobalData.UserId;
            NewSaveLogs(item);
            await RequestHelper.SendRequestAsync(URLs.SIMCOMPONENT, HttpMethod.Post, item, GlobalData.AccessToken);
            Messenger.Default.Send("SimComponentRefresh");
            
        }
        #endregion
        #region CommandsFunctions

        private async void getStatus(StatusVM vm)
        {
            item.Status = vm.Id;
            StatusName = vm.Name;
        }
        private async void getSimComponentType(SimComponentTypeVM vm)
        {
            item.Type = vm.Id;
            TypeName = vm.Name;
        }
        private async void getManufacturer(ManufacturerVM vm)
        {
            item.Manufacturer = vm.Id;
            ManufacturerName = vm.Name;
        }

        #endregion
        #region Dane
        private string _ManufacturerName;
        private string _TypeName;
        private string _StatusName;
        public string ManufacturerName
        {
            get
            {
                return _ManufacturerName;
            }
            set
            {
                if(value != _ManufacturerName)
                {
                    _ManufacturerName = value;
                    OnPropertyChanged(() => ManufacturerName);
                }
            }
        }
        public string TypeName
        {
            get
            {
                return _TypeName;
            }
            set
            {
                if(value != _TypeName)
                {
                    _TypeName = value;
                    OnPropertyChanged(() => TypeName);
                }
            }
        }
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
        public int Status
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
        public int Manufacturer
        {
            get
            {
                return item.Manufacturer;
            }
            set
            {
                item.Manufacturer = value;
                OnPropertyChanged(() => Manufacturer);
            }
        }
        public int Type
        {
            get
            {
                return item.Type;
            }
            set
            {
                item.Type = value;
                OnPropertyChanged(() => Type);
            }
        }
        public string Name
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
        #endregion
    }
}
