using AdministrationApp.Helpers;
using Data.Computers.SelectVMs;
using Data;
using Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AdministrationApp.Views;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics;
using AdministrationApp.ViewModels.NewViewModel;

namespace AdministrationApp.ViewModels.AllViewModel
{
    public class SummaryViewModel : JedenViewModel<MainWindowModel>
    {
        #region Commands
        private BaseCommand _ShowComputersCommand;
        private BaseCommand _ShowMonitorCommand;
        private BaseCommand _ShowLocationCommand;
        private BaseCommand _ShowUserCommand;
        public ICommand ShowComputersCommand
        {
            get
            {
                if (_ShowComputersCommand == null)
                {
                    _ShowComputersCommand = new BaseCommand(() => Messenger.Default.Send("ShowComputers"));
                }
                return _ShowComputersCommand;
            }
        }
        public ICommand ShowMonitorCommand
        {
            get
            {
                if (_ShowMonitorCommand == null)
                {
                    _ShowMonitorCommand = new BaseCommand(() => Messenger.Default.Send("ShowMonitors"));
                }
                return _ShowMonitorCommand;
            }
        }
        public ICommand ShowLocationCommand
        {
            get
            {
                if (_ShowLocationCommand == null)
                {
                    _ShowLocationCommand = new BaseCommand(() => Messenger.Default.Send("ShowLocations"));
                }
                return _ShowLocationCommand;
            }
        }
        public ICommand ShowUserCommand
        {
            get
            {
                if (_ShowUserCommand == null)
                {
                    _ShowUserCommand = new BaseCommand(() => Messenger.Default.Send("ShowUsers"));
                }
                return _ShowUserCommand;
            }
        }
        #endregion
        #region Konstruktor
        private MainWindowModel _model;
        public MainWindowModel model
        {
            get
            {
                return _model;
            }
            set
            {
                if (value != _model)
                {
                    _model = value;
                }
            }
        }

        public SummaryViewModel(MainWindowModel vm) : base("Podsumowanie")
        {
            item = vm;
        }
        public override void Save()
        {

        }
        #endregion
        #region Dane

        public int ComputerCount
        {
            get
            {
                return item.ComputerCount;
            }
            set
            {
                if (item.ComputerCount != value)
                {
                    item.ComputerCount = value;
                    OnPropertyChanged(() => ComputerCount);
                }
            }
        }

        public int MonitorCount
        {
            get
            {
                return item.MonitorCount;
            }
            set
            {
                if (item.MonitorCount != value)
                {
                    item.MonitorCount = value;
                    OnPropertyChanged(() => MonitorCount);
                }
            }
        }

        public int SoftwareCount
        {
            get
            {
                return item.SoftwareCount;
            }
            set
            {
                if (item.SoftwareCount != value)
                {
                    item.SoftwareCount = value;
                    OnPropertyChanged(() => SoftwareCount);
                }
            }
        }

        public int LicenseCount
        {
            get
            {
                return item.LicenseCount;
            }
            set
            {
                if (item.LicenseCount != value)
                {
                    item.LicenseCount = value;
                    OnPropertyChanged(() => LicenseCount);
                }
            }
        }

        public int NetworkDeviceCount
        {
            get
            {
                return item.NetworkDeviceCount;
            }
            set
            {
                if (item.NetworkDeviceCount != value)
                {
                    item.NetworkDeviceCount = value;
                    OnPropertyChanged(() => NetworkDeviceCount);
                }
            }
        }

        public int DeviceCount
        {
            get
            {
                return item.DeviceCount;
            }
            set
            {
                if (item.DeviceCount != value)
                {
                    item.DeviceCount = value;
                    OnPropertyChanged(() => DeviceCount);
                }
            }
        }

        public int PrinterCount
        {
            get
            {
                return item.PrinterCount;
            }
            set
            {
                if (item.PrinterCount != value)
                {
                    item.PrinterCount = value;
                    OnPropertyChanged(() => PrinterCount);
                }
            }
        }

        public int PhoneCount
        {
            get
            {
                return item.PhoneCount;
            }
            set
            {
                if (item.PhoneCount != value)
                {
                    item.PhoneCount = value;
                    OnPropertyChanged(() => PhoneCount);
                }
            }
        }

        public int RackCabinetCount
        {
            get
            {
                return item.RackCabinetCount;
            }
            set
            {
                if (item.RackCabinetCount != value)
                {
                    item.RackCabinetCount = value;
                    OnPropertyChanged(() => RackCabinetCount);
                }
            }
        }

        public int HardDriveCount
        {
            get
            {
                return item.HardDriveCount;
            }
            set
            {
                if (item.HardDriveCount != value)
                {
                    item.HardDriveCount = value;
                    OnPropertyChanged(() => HardDriveCount);
                }
            }
        }

        public int ServerCount
        {
            get
            {
                return item.ServerCount;
            }
            set
            {
                if (item.ServerCount != value)
                {
                    item.ServerCount = value;
                    OnPropertyChanged(() => ServerCount);
                }
            }
        }

        public int SimCardCount
        {
            get
            {
                return item.SimCardCount;
            }
            set
            {
                if (item.SimCardCount != value)
                {
                    item.SimCardCount = value;
                    OnPropertyChanged(() => SimCardCount);
                }
            }
        }

        public int UsersCount
        {
            get
            {
                return item.UsersCount;
            }
            set
            {
                if (item.UsersCount != value)
                {
                    item.UsersCount = value;
                    OnPropertyChanged(() => UsersCount);
                }
            }
        }

        public int TicketsCount
        {
            get
            {
                return item.TicketsCount;
            }
            set
            {
                if (item.TicketsCount != value)
                {
                    item.TicketsCount = value;
                    OnPropertyChanged(() => TicketsCount);
                }
            }
        }

        public int LocationCount
        {
            get
            {
                return item.LocationCount;
            }
            set
            {
                if (item.LocationCount != value)
                {
                    item.LocationCount = value;
                    OnPropertyChanged(() => LocationCount);
                }
            }
        }

        #endregion
    }
}
