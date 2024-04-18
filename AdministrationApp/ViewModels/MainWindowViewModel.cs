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
using GalaSoft.MvvmLight.Messaging;

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
                case "ShowComputers":
                    ShowComputers();
                    break;
                case "ShowMonitors":
                    ShowMonitors();
                    break;
                case "ShowLocations":
                    ShowLocationWindow();
                    break;
                case "ShowUsers":
                    ShowUsers();
                    break;
                case "ChooseStatus":
                    ShowStatusWindow();
                    break;
                case "ChooseUser":
                    ShowUsersWindow();
                    break;
                case "ChooseLocation":
                    ShowLocationWindow();
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
        }private BaseCommand _ShowComputersCommand;
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
        private BaseCommand _CreateComputersCommand;
        public ICommand CreateComputersCommand
        {
            get
            {
                if (_CreateComputersCommand == null)
                {
                    _CreateComputersCommand = new BaseCommand(() => CreateComputer());
                }
                return _CreateComputersCommand;
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
        private void ShowComputers()
        {
            ShowAllWorkspace<AllComputerViewModel>();
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
        #endregion
    }
}
