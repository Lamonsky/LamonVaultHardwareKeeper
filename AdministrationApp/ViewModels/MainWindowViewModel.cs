using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using AdministrationApp.Helpers;
using AdministrationApp.ViewModels.AllViewModel;
using AdministrationApp.ViewModels.AllViewModel.Windows;
using AdministrationApp.ViewModels.NewViewModel;
using AdministrationApp.ViewModels.NewViewModel.Windows;
using AdministrationApp.Views;
using AdministrationApp.Views.AllWindows;
using AdministrationApp.Views.NewViews.Windows;
using Data;
using Data.Computers.CreateEditVMs;
using Data.Helpers;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;

namespace AdministrationApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {


        #region LoggedUser

        private LoggedUser _loggeduser;
        public LoggedUser LoggedUser
        {
            get
            {
                return _loggeduser;
            }
            set
            {
                if (value != _loggeduser)
                {
                    _loggeduser = value;
                }
            }
        }
        public string EMail
        {
            get
            {
                return _loggeduser.Email;
            }
        }
        #endregion


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
            Messenger.Default.Register<LoggedUser>(this, loggeduser);

        }
        private void loggeduser(LoggedUser user)
        {
            LoggedUser = user;
            GlobalData.AccessToken = user.AccessToken;
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
                case "Modele drukarekAdd":
                    CreatePrinterModelWindow();
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
                case "ChoosePrinterModel":
                    ShowPrinterModelWindow();
                    break;
                case "ComputerModelAdd":
                    CreateComputerModelWindow();
                    break;
                case "ChooseComputerModel":
                    ShowComputerModelWindow();
                    break;
                case "ComputerTypeAdd":
                    CreateComputerTypeWindow();
                    break;
                case "ChooseComputerType":
                    ShowComputerTypeWindow();
                    break;
                case "DeviceModelAdd":
                    CreateDeviceModelWindow();
                    break;
                case "ChooseDeviceModel":
                    ShowDeviceModelWindow();
                    break;
                case "DeviceTypeAdd":
                    CreateDeviceTypeWindow();
                    break;
                case "ChooseDeviceType":
                    ShowDeviceTypeWindow();
                    break;
                case "MonitorModelAdd":
                    CreateMonitorModelWindow();
                    break;
                case "ChooseMonitorModel":
                    ShowMonitorModelWindow();
                    break;
                case "MonitorTypeAdd":
                    CreateMonitorTypeWindow();
                    break;
                case "ChooseMonitorType":
                    ShowMonitorTypeWindow();
                    break;
                case "NetworkDeviceModelAdd":
                    CreateNetworkDeviceModelWindow();
                    break;
                case "ChooseNetworkDeviceModel":
                    ShowNetworkDeviceModelWindow();
                    break;
                case "NetworkDeviceTypeAdd":
                    CreateNetworkDeviceTypeWindow();
                    break;
                case "ChooseNetworkDeviceType":
                    ShowNetworkDeviceTypeWindow();
                    break;
                case "PhoneModelAdd":
                    CreatePhoneModelWindow();
                    break;
                case "ChoosePhoneModel":
                    ShowPhoneModelWindow();
                    break;
                case "PhoneTypeAdd":
                    CreatePhoneTypeWindow();
                    break;
                case "ChoosePhoneType":
                    ShowPhoneTypeWindow();
                    break;
                case "RackCabinetModelAdd":
                    CreateRackCabinetModelWindow();
                    break;
                case "ChooseRackCabinetModel":
                    ShowRackCabinetModelWindow();
                    break;
                case "RackCabinetTypeAdd":
                    CreateRackCabinetTypeWindow();
                    break;
                case "ChooseRackCabinetType":
                    ShowRackCabinetTypeWindow();
                    break;
                case "SimComponentTypeAdd":
                    CreateSimComponentTypeWindow();
                    break;
                case "ChooseSimComponentType":
                    ShowSimComponentTypeWindow();
                    break;
                case "ChooseStatus":
                    ShowStatusWindow(); 
                    break;
                case "ChooseManufacturer":
                    ShowManufacturerWindow();
                    break;
                case "ChooseOperatingSystem":
                    ShowOperatingSystemWindow();
                    break;
                case string n when n.StartsWith("KomputeryEdit"):                 
                    EditComputer(CutString(name));
                    break;
            }
        }
        #endregion
        private string CutString(string text)
        {
            int index = text.LastIndexOf("/");

            if (index == -1)
            {
                return text;
            }

            return text.Substring(index + 1);
        }

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


        #endregion
        #region Funkcje wywolujace okna
        private void CreateComputer()
        {
            CreateWorkspace<NewComputerViewModel>();
        }
        private async void EditComputer(string id)
        {
            try
            {
                string url = URLs.COMPUTERS_ID.Replace("{id}", id);
                ComputersCreateEditVM vm = await RequestHelper.SendRequestAsync<object, ComputersCreateEditVM>(url, HttpMethod.Get, null, null);
                if (vm != null)
                {
                    EditComputerViewModel workspace = new EditComputerViewModel(vm);
                    Workspaces.Add(workspace);
                    SetActiveWorkspace(workspace);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.WriteLine(ex.Message);
            }
            
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
            AllDictionaryWindow window = new AllDictionaryWindow();
            window.DataContext = new AllPrinterTypeViewModelWindow(window);
            window.Show();
        }
        private void CreatePrinterTypeWindow()
        {
            NewDictionaryWindow window = new NewDictionaryWindow();
            window.DataContext = new NewPrinterTypeWindowViewModel(window);
            window.Show();
        }
        private void ShowPrinterModelWindow()
        {
            AllDictionaryWindow window = new AllDictionaryWindow();
            window.DataContext = new AllPrinterModelViewModelWindow(window);
            window.Title="Modele drukarek";
            window.Show();
        }
        private void CreatePrinterModelWindow()
        {
            NewDictionaryWindow window = new NewDictionaryWindow();
            window.DataContext = new NewPrinterModelViewModel(window);
            window.Title="Modele drukarek";
            window.Show();
        }
        private void ShowStatusWindow()
        {
            AllStatusWindow allStatusWindow = new AllStatusWindow();
            allStatusWindow.DataContext = new AllStatusViewModelWindow(allStatusWindow);
            allStatusWindow.Show();

        }
        private void ShowManufacturerWindow()
        {
            AllDictionaryWindow window = new AllDictionaryWindow();
            window.DataContext = new AllManufacturerViewModelWindow(window);
            window.Show();

        }
        private void ShowUsersWindow()
        {
            AllUsersWindow window = new AllUsersWindow();
            window.DataContext = new AllUserViewModelWindow(window);
            window.Show();
        }
        private void ShowLocationWindow()
        {
            AllLocationsWindow window = new AllLocationsWindow();
            window.DataContext = new AllLocationsViewModelWindow(window);
            window.Show();
        }
        private void ShowSimCardWindow()
        {
            AllSimCardWindow window = new AllSimCardWindow();
            window.DataContext = new AllSimCardViewModelWindow(window);
            window.Show();
        }
        private void ShowSimComponentTypeWindow()
        {
            AllDictionaryWindow window = new AllDictionaryWindow();
            window.DataContext = new AllSimComponentTypeViewModelWindow(window);
            window.Show();
        }
        private void CreateSimComponentTypeWindow()
        {
            NewDictionaryWindow window = new NewDictionaryWindow();
            window.DataContext = new NewSimComponentTypeViewModel(window);
            window.Show();
        }
        private void ShowRackCabinetTypeWindow()
        {
            AllDictionaryWindow window = new AllDictionaryWindow();
            window.DataContext = new AllRackCabinetTypeViewModelWindow(window);
            window.Show();
        }
        private void CreateRackCabinetTypeWindow()
        {
            NewDictionaryWindow window = new NewDictionaryWindow();
            window.DataContext = new NewRackCabinetTypeViewModel(window);
            window.Show();
        }
        private void ShowRackCabinetModelWindow()
        {
            AllDictionaryWindow window = new AllDictionaryWindow();
            window.DataContext = new AllRackCabinetModelViewModelWindow(window);
            window.Show();
        }
        private void CreateRackCabinetModelWindow()
        {
            NewDictionaryWindow window = new NewDictionaryWindow();
            window.DataContext = new NewRackCabinetModelViewModel(window);
            window.Show();
        }
        private void ShowPhoneTypeWindow()
        {
            AllDictionaryWindow window = new AllDictionaryWindow();
            window.DataContext = new AllPhoneTypeViewModelWindow(window);
            window.Show();
        }
        private void CreatePhoneTypeWindow()
        {
            NewDictionaryWindow window = new NewDictionaryWindow();
            window.DataContext = new NewPhoneTypeViewModel(window);
            window.Show();
        }
        private void ShowPhoneModelWindow()
        {
            AllDictionaryWindow window = new AllDictionaryWindow();
            window.DataContext = new AllPhoneModelViewModelWindow(window);
            window.Show();
        }
        private void CreatePhoneModelWindow()
        {
            NewDictionaryWindow window = new NewDictionaryWindow();
            window.DataContext = new NewPhoneModelViewModel(window);
            window.Show();
        }
        private void ShowNetworkDeviceTypeWindow()
        {
            AllDictionaryWindow window = new AllDictionaryWindow();
            window.DataContext = new AllNetworkDeviceTypeViewModelWindow(window);
            window.Show();
        }
        private void CreateNetworkDeviceTypeWindow()
        {
            NewDictionaryWindow window = new NewDictionaryWindow();
            window.DataContext = new NewNetworkDeviceTypeViewModel(window);
            window.Show();
        }
        private void ShowNetworkDeviceModelWindow()
        {
            AllDictionaryWindow window = new AllDictionaryWindow();
            window.DataContext = new AllNetworkDeviceModelViewModelWindow(window);
            window.Show();
        }
        private void CreateNetworkDeviceModelWindow()
        {
            NewDictionaryWindow window = new NewDictionaryWindow();
            window.DataContext = new NewNetworkDeviceModelViewModel(window);
            window.Show();
        }
        private void ShowMonitorTypeWindow()
        {
            AllDictionaryWindow window = new AllDictionaryWindow();
            window.DataContext = new AllMonitorTypeViewModelWindow(window);
            window.Show();
        }
        private void CreateMonitorTypeWindow()
        {
            NewDictionaryWindow window = new NewDictionaryWindow();
            window.DataContext = new NewMonitorTypeViewModel(window);
            window.Show();
        }
        private void ShowMonitorModelWindow()
        {
            AllDictionaryWindow window = new AllDictionaryWindow();
            window.DataContext = new AllMonitorModelViewModelWindow(window);
            window.Show();
        }
        private void CreateMonitorModelWindow()
        {
            NewDictionaryWindow window = new NewDictionaryWindow();
            window.DataContext = new NewMonitorModelViewModel(window);
            window.Show();
        }
        private void ShowDeviceTypeWindow()
        {
            AllDictionaryWindow window = new AllDictionaryWindow();
            window.DataContext = new AllDeviceTypeViewModelWindow(window);
            window.Show();
        }
        private void CreateDeviceTypeWindow()
        {
            NewDictionaryWindow window = new NewDictionaryWindow();
            window.DataContext = new NewDeviceTypeViewModel(window);
            window.Show();
        }
        private void ShowDeviceModelWindow()
        {
            AllDictionaryWindow window = new AllDictionaryWindow();
            window.DataContext = new AllDeviceModelViewModelWindow(window);
            window.Show();
        }
        private void CreateDeviceModelWindow()
        {
            NewDictionaryWindow window = new NewDictionaryWindow();
            window.DataContext = new NewDeviceModelViewModel(window);
            window.Show();
        }
        private void ShowOperatingSystemWindow()
        {
            AllDictionaryWindow window = new AllDictionaryWindow();
            window.DataContext = new AllOperatingSystemViewModelWindow(window);
            window.Show();
        }
        private void CreateOperatingSystemWindow()
        {
            NewDictionaryWindow window = new NewDictionaryWindow();
            window.DataContext = new NewOperatingSystemViewModel(window);
            window.Show();
        }
        private void ShowComputerTypeWindow()
        {
            AllDictionaryWindow window = new AllDictionaryWindow();
            window.DataContext = new AllComputerTypeViewModelWindow(window);
            window.Show();
        }
        private void CreateComputerTypeWindow()
        {
            NewDictionaryWindow window = new NewDictionaryWindow();
            window.DataContext = new NewComputerTypeViewModel(window);
            window.Show();
        }
        private void ShowComputerModelWindow()
        {
            AllDictionaryWindow window = new AllDictionaryWindow();
            window.DataContext = new AllComputerModelViewModelWindow(window);
            window.Show();
        }
        private void CreateComputerModelWindow()
        {
            NewDictionaryWindow window = new NewDictionaryWindow();
            window.DataContext = new NewComputerModelViewModel(window);
            window.Show();
        }



        #endregion
    }
}
