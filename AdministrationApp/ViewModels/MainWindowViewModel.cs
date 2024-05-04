using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using AdministrationApp.Helpers;
using AdministrationApp.ViewModels.AllViewModel;
using AdministrationApp.ViewModels.NewViewModel;
using AdministrationApp.Views;
using AdministrationApp.Views.AllWindows;
using AdministrationApp.Views.NewViews.Windows;
using Data.Helpers;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;

namespace AdministrationApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {

        #region Workspaces
        //
        private ObservableCollection<WorkspaceViewModel> _Workspaces;
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.onWorkspacesChanged;
                }
                return _Workspaces;
            }
        }
        #endregion
        #region Zakładki
        private void onWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.onWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.onWorkspaceRequestClose;
        }
        private void onWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            //workspace.Dispos();
            this.Workspaces.Remove(workspace);
        }
        #endregion
        #region Funkcje
        private void CreateWorkspace<T>() where T : WorkspaceViewModel, new()
        {
            T workspace = new T();
            Workspaces.Add(workspace);
            SetActiveWorkspace(workspace);
        }
        private void ShowAllWorkspace<T>() where T : WorkspaceViewModel, new()
        {
            T workspace = Workspaces.FirstOrDefault(vm => vm is T) as T;
            if(workspace == null)
            {
                workspace = new T();
                Workspaces.Add(workspace);
            }
            SetActiveWorkspace(workspace);
        }
        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }
        #endregion
        #region Commands
        public MainWindowViewModel()
        {
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            switch (name)
            {
                case "KomputeryAdd":
                    CreateComputer(); 
                    break;
                case "MonitoryAdd":
                    CreateMonitor();
                    break;
                case "UżytkownicyAdd":
                    CreateUser();
                    break;
                case "OprogramowanieAdd":
                    CreateSoftware();
                    break;
                case "Urządzenia siecioweAdd":
                    CreateNetworkDevice();
                    break;
                case "UrządzeniaAdd":
                    CreateDevice();
                    break;
                case "DrukarkiAdd":
                    CreatePrinter();
                    break;
                case "TelefonyAdd":
                    CreatePhone();
                    break;
                case "Szafy RackAdd":
                    CreateRackCabinet();
                    break;
                case "Karty SimAdd":
                    CreateSimCard();
                    break;
                case "Rodzaje drukarekAdd":
                    CreatePrinterTypeWindow();
                    break;
                case "ChooseUser":
                    ShowUsersWindow();
                    break;
                case "ChooseLocation":
                    ShowLocationWindow();
                    break;
                case "ChooseSimCard":
                    ShowSimCardWindow();
                    break;
                case "ChoosePrinterType":
                    ShowPrinterTypeWindow();
                    break;

            }
        }
        #endregion

        #region Komendy do Buttonow
        private BaseCommand _ShowSummaryCommand;
        public ICommand ShowSummaryCommand
        {
            get
            {
                if(_ShowSummaryCommand == null)
                {
                    _ShowSummaryCommand = new BaseCommand(() => ShowSummary());
                }
                return _ShowSummaryCommand;
            }
        }
        private BaseCommand _ShowComputersCommand;
        public ICommand ShowComputersCommand
        {
            get
            {
                if(_ShowComputersCommand == null)
                {
                    _ShowComputersCommand = new BaseCommand(() => ShowComputers());
                }
                return _ShowComputersCommand;
            }
        }
        private BaseCommand _ShowMonitorsCommand;
        public ICommand ShowMonitorsCommand
        {
            get
            {
                if (_ShowMonitorsCommand == null)
                {
                    _ShowMonitorsCommand = new BaseCommand(() => ShowMonitors());
                }
                return _ShowMonitorsCommand;
            }
        }
        private BaseCommand _ShowUserCommand;
        public ICommand ShowUserCommand
        {
            get
            {
                if (_ShowUserCommand == null)
                {
                    _ShowUserCommand = new BaseCommand(() => ShowUsers());
                }
                return _ShowUserCommand;
            }
        }
        private BaseCommand _ShowSoftwareCommand;
        public ICommand ShowSoftwareCommand
        {
            get
            {
                if (_ShowSoftwareCommand == null)
                {
                    _ShowSoftwareCommand = new BaseCommand(() => ShowSoftware());
                }
                return _ShowSoftwareCommand;
            }
        }
        private BaseCommand _ShowNetworkDeviceCommand;
        public ICommand ShowNetworkDeviceCommand
        {
            get
            {
                if (_ShowNetworkDeviceCommand == null)
                {
                    _ShowNetworkDeviceCommand = new BaseCommand(() => ShowNetworkDevice());
                }
                return _ShowNetworkDeviceCommand;
            }
        }
        private BaseCommand _ShowDeviceCommand;
        public ICommand ShowDeviceCommand
        {
            get
            {
                if (_ShowDeviceCommand == null)
                {
                    _ShowDeviceCommand = new BaseCommand(() => ShowDevice());
                }
                return _ShowDeviceCommand;
            }
        }
        private BaseCommand _ShowPrinterCommand;
        public ICommand ShowPrinterCommand
        {
            get
            {
                if (_ShowPrinterCommand == null)
                {
                    _ShowPrinterCommand = new BaseCommand(() => ShowPrinter());
                }
                return _ShowPrinterCommand;
            }
        }
        private BaseCommand _ShowPhoneCommand;
        public ICommand ShowPhoneCommand
        {
            get
            {
                if (_ShowPhoneCommand == null)
                {
                    _ShowPhoneCommand = new BaseCommand(() => ShowPhone());
                }
                return _ShowPhoneCommand;
            }
        }
        private BaseCommand _ShowRackCabinetCommand;
        public ICommand ShowRackCabinetCommand
        {
            get
            {
                if (_ShowRackCabinetCommand == null)
                {
                    _ShowRackCabinetCommand = new BaseCommand(() => ShowRackCabinet());
                }
                return _ShowRackCabinetCommand;
            }
        }
        private BaseCommand _ShowSimCardCommand;
        public ICommand ShowSimCardCommand
        {
            get
            {
                if (_ShowSimCardCommand == null)
                {
                    _ShowSimCardCommand = new BaseCommand(() => ShowSimCard());
                }
                return _ShowSimCardCommand;
            }
        }


        private BaseCommand _ShowPrinterTypeCommand;
        public ICommand ShowPrinterTypeCommand
        {
            get
            {
                if (_ShowPrinterTypeCommand == null)
                {
                    _ShowPrinterTypeCommand = new BaseCommand(() => ShowPrinterTypeWindow());
                }
                return _ShowPrinterTypeCommand;
            }
        }


        #endregion
        #region Funkcje wywolujace okna
        private void CreateComputer()
        {
            CreateWorkspace<NewComputerViewModel>();
        }
        private void CreateMonitor()
        {
            CreateWorkspace<NewMonitorViewModel>();
        }
        private void CreateUser()
        {
            CreateWorkspace<NewUserViewModel>();
        }
        private void CreateSoftware()
        {
            CreateWorkspace<NewSoftwareViewModel>();
        }
        private void CreateDevice()
        {
            CreateWorkspace<NewDeviceViewModel>();
        }
        private void CreateNetworkDevice()
        {
            CreateWorkspace<NewNetworkDeviceViewModel>();
        }
        private void CreatePrinter()
        {
            CreateWorkspace<NewPrinterViewModel>();
        }
        private void CreatePhone()
        {
            CreateWorkspace<NewPhoneViewModel>();
        }
        private void CreateRackCabinet()
        {
            CreateWorkspace<NewRackCabinetViewModel>();
        }
        private void CreateSimCard()
        {
            CreateWorkspace<NewSimCardViewModel>();
        }
        private void ShowComputers()
        {
            ShowAllWorkspace<AllComputerViewModel>();
        }
        private void ShowNetworkDevice()
        {
            ShowAllWorkspace<AllNetworkDeviceViewModel>();
        }
        private void ShowDevice()
        {
            ShowAllWorkspace<AllDeviceViewModel>();
        }
        private void ShowSummary()
        {
            ShowAllWorkspace<SummaryViewModel>();
        }
        private void ShowMonitors()
        {
            ShowAllWorkspace<AllMonitorViewModel>();
        }
        private void ShowUsers()
        {
            ShowAllWorkspace<AllUserViewModel>();
        }
        private void ShowSoftware()
        {
            ShowAllWorkspace<AllSoftwareViewModel>();
        }
        private void ShowPrinter()
        {
            ShowAllWorkspace<AllPrinterViewModel>();
        }
        private void ShowPhone()
        {
            ShowAllWorkspace<AllPhoneViewModel>();
        }
        private void ShowRackCabinet()
        {
            ShowAllWorkspace<AllRackCabinetViewModel>();
        }
        private void ShowSimCard()
        {
            ShowAllWorkspace<AllSimCardViewModel>();
        }
        private void ShowPrinterTypeWindow()
        {
            AllPrinterTypeWindow allPrinterTypeWindow= new AllPrinterTypeWindow();
            allPrinterTypeWindow.Show();
        }
        private void CreatePrinterTypeWindow()
        {
            NewPrinterTypeWindow newPrinterTypeWindow = new NewPrinterTypeWindow();
            newPrinterTypeWindow.Show();
        }
        private void ShowStatusWindow()
        {
            AllStatusWindow allStatusWindow = new AllStatusWindow();
            allStatusWindow.Show();

        }
        private void ShowUsersWindow()
        {
            AllUsersWindow allUsersWindow = new AllUsersWindow();
            allUsersWindow.Show();
        }
        private void ShowLocationWindow()
        {
            AllLocationsWindow allLocationsWindow = new AllLocationsWindow();
            allLocationsWindow.Show();
        }
        private void ShowSimCardWindow()
        {
            AllSimCardWindow allSimCardWindow = new AllSimCardWindow();
            allSimCardWindow.Show();
        }

        
        
        #endregion
    }
}
