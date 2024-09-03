using AdministrationApp.Helpers;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AdministrationApp.ViewModels.AllViewModel
{
    public abstract class WszystkieViewModel<T> : WorkspaceViewModel
    {
        #region Command
        private BaseCommand _LoadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand == null)
                {
                    _LoadCommand = new BaseCommand(() => load());
                }
                return _LoadCommand;
            }
        }
        private BaseCommand _AddCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new BaseCommand(() => add());
                }
                return _AddCommand;
            }
        }
        private BaseCommand _RefreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null)
                {
                    _RefreshCommand = new BaseCommand(() => load());
                }
                return _RefreshCommand;
            }
        }
        private BaseCommand _EditCommand;
        public ICommand EditCommand
        {
            get
            {
                if (_EditCommand == null)
                {
                    _EditCommand = new BaseCommand(() => Edit());
                }
                return _EditCommand;
            }
        }
        private BaseCommand _RemoveCommand;
        public ICommand RemoveCommand
        {
            get
            {
                if (_RemoveCommand == null)
                {
                    _RemoveCommand = new BaseCommand(() => Remove());
                }
                return _RemoveCommand;
            }
        }
        private BaseCommand _FilterCommand;
        public ICommand FilterCommand
        {
            get
            {
                if (_FilterCommand == null)
                {
                    _FilterCommand = new BaseCommand(() => Filter());
                }
                return _FilterCommand;
            }
        }
        private BaseCommand _SendCommand;
        public ICommand SendCommand
        {
            get
            {
                if (_SendCommand == null)
                {
                    _SendCommand = new BaseCommand(() => send());
                }
                return _SendCommand;
            }
        }
        #endregion
        #region Kolekcja 
        private bool _IsLoading;
        public bool IsLoading
        {
            get
            {
                return _IsLoading;
            }
            set
            {
                _IsLoading = value;
                OnPropertyChanged(() => IsLoading);
            }
        }
        //tu ....
        private List<T> _List;
        public List<T> List
        {
            get
            {
                return _List;
            }
            set
            {
                _List = value;
                OnPropertyChanged(() => List);
            }
        }
        #endregion
        #region Konstruktor
        public WszystkieViewModel(string displayName)
        {
            IsLoading = false;
            DisplayName = displayName;
            load();
            IsLoading = true;
        }
        #endregion
        #region Pomocniczy
        public abstract void load();
        public abstract void send();
        private void add()
        {
            Messenger.Default.Send(DisplayName + "Add");
        }
        public abstract void Edit();
        public abstract void Remove();
        #endregion

        #region Sortowanie

        public abstract void Filter();
        public abstract List<string> GetComboBoxFilterList();
        public List<string> FilterComboBoxListItems
        {
            get
            {
                return GetComboBoxFilterList();
            }
        }
        public string FindTextBox { get; set; }
        public string FilterField { get; set; }
        public int ItemID { get; set; }
        #endregion
    }
}
