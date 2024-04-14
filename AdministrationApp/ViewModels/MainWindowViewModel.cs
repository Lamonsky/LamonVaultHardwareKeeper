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
using AdministrationApp.Views;
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
        private List<CommandViewModel> CreateCommand()
        {
            Messenger.Default.Register<string>(this, open);
            return new List<CommandViewModel>();
        }
        private void open(string name)
        {
            switch (name)
            {
                case "ShowComputers":
                    ShowComputers();
                    break;
                
            }
        }
        #endregion

        #region Komendy do Buttonow
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
        private BaseCommand _DodajKategorieCommand;
        public ICommand DodajKategorieCommand
        {
            get
            {
                if (_DodajKategorieCommand == null)
                {
                    //_DodajKategorieCommand = new BaseCommand(() => Messenger.Default.Send("KategorieAdd"));
                }
                return _DodajKategorieCommand;
            }
        }
        
        #endregion
        #region Funkcje wywolujace okna
        private void CreateDostawca()
        {
            //CreateWorkspace<NowyDostawcaViewModel>();
        }
        private void ShowComputers()
        {
            ShowAllWorkspace<AllComputerViewModel>();
        }

        #endregion
    }
}
