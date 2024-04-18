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

namespace AdministrationApp.ViewModels.AllViewModel
{
    public class SummaryViewModel : WszystkieViewModel<MainWindowModel>
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
        }public ICommand ShowMonitorCommand
        {
            get
            {
                if (_ShowMonitorCommand == null)
                {
                    _ShowMonitorCommand = new BaseCommand(() => Messenger.Default.Send("ShowMonitors"));
                }
                return _ShowMonitorCommand;
            }
        }public ICommand ShowLocationCommand
        {
            get
            {
                if (_ShowLocationCommand == null)
                {
                    _ShowLocationCommand = new BaseCommand(() => Messenger.Default.Send("ShowLocations"));
                }
                return _ShowLocationCommand;
            }
        }public ICommand ShowUserCommand
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

        public SummaryViewModel() : base("Podsumowanie")
        {
            load();
        }

        public async Task<MainWindowModel> getItems()
        {
            return await RequestHelper.SendRequestAsync<object, MainWindowModel>(URLs.MAINWINDOW, HttpMethod.Get, null, null);
        }

        public async override void load()
        {
            model = await getItems();
            ComputerCount = model.ComputerCount;
            monitorCount = model.MonitorCount;
            usersCount = model.UserCount;
            locationCount = model.LocationCount;
        }
        #endregion
        #region Dane
        private int _computerCount;
        public int ComputerCount
        {
            get
            {
                return _computerCount;
            }
            set
            {
                if (_computerCount != value)
                {
                    _computerCount = value;
                    OnPropertyChanged(() => ComputerCount);
                }
            }
        }
        private int _monitorCount;
        public int monitorCount
        {
            get
            {
                return _monitorCount;
            }
            set
            {
                if (_monitorCount != value)
                {
                    _monitorCount = value;
                    OnPropertyChanged(() => monitorCount);
                }
            }
        }
        private int _locationCount;
        public int locationCount
        {
            get
            {
                return _locationCount;
            }
            set
            {
                if (_locationCount != value)
                {
                    _locationCount = value;
                    OnPropertyChanged(() => locationCount);
                }
            }
        }
        private int _usersCount;
        public int usersCount
        {
            get
            {
                return _usersCount;
            }
            set
            {
                if (_usersCount != value)
                {
                    _usersCount = value;
                    OnPropertyChanged(() => usersCount);
                }
            }
        }
        #endregion
        #region NECESSERY
        public override void Edit()
        {
            throw new NotImplementedException();
        }

        public override void Filter()
        {
            throw new NotImplementedException();
        }

        public override List<string> GetComboBoxFilterList()
        {
            return new List<string>();
        }

        public override List<string> GetComboBoxSortList()
        {
            return new List<string>();
        }

        public override void Remove()
        {
            throw new NotImplementedException();
        }

        public override void Sort()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
