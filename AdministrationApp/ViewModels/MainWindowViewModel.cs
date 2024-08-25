﻿using System;
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
using AdministrationApp.ViewModels.EditViewModel;
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
        private void CreateWindows<W, VM>(Func<W, VM> viewModelFactory)
        where W : Window, new()
        where VM : class
        {
            // Sprawdzenie, czy okno już istnieje w otwartych oknach aplikacji.
            W existingWindow = Application.Current.Windows.OfType<W>().FirstOrDefault();

            if (existingWindow != null)
            {
                // Jeśli okno już istnieje, aktywujemy je i ustawiamy na wierzchu.
                existingWindow.Activate();
                existingWindow.Focus();
            }
            else
            {
                // Tworzenie nowego okna.
                W window = new W();

                // Tworzenie ViewModelu z użyciem fabryki, która przyjmuje instancję okna.
                VM viewModel = viewModelFactory(window);

                // Przypisanie ViewModelu do DataContext okna.
                window.DataContext = viewModel;

                window.Show();
            }
        }
        private async Task EditItem<TViewModel, TWorkspace>(string id, string urlTemplate, Func<TViewModel, TWorkspace> createWorkspace)
        where TWorkspace : WorkspaceViewModel
        {
            try
            {
                string url = urlTemplate.Replace("{id}", id);
                TViewModel vm = await RequestHelper.SendRequestAsync<object, TViewModel>(url, HttpMethod.Get, null, null);

                if (vm != null)
                {
                    TWorkspace workspace = createWorkspace(vm);
                    Workspaces.Add(workspace);
                    SetActiveWorkspace(workspace);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.WriteLine(ex.Message);
            }
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
                case "StatusAdd":
                    CreateStatusWindow();
                    break;
                case string n when n.StartsWith("KomputeryEdit"):                 
                    EditComputer(CutString(name));
                    break;
                case string n when n.StartsWith("UrządzeniaEdit"):
                    EditDevice(CutString(name));
                    break;
                case string n when n.StartsWith("MonitoryEdit"):
                    EditMonitor(CutString(name));
                    break;
                case string n when n.StartsWith("Urządzenia siecioweEdit"):
                    EditNetworkDevice(CutString(name));
                    break;
                case string n when n.StartsWith("TelefonyEdit"):
                    EditPhone(CutString(name));
                    break;
                case string n when n.StartsWith("DrukarkiEdit"):
                    EditPrinter(CutString(name));
                    break;
                case string n when n.StartsWith("Szafy RackEdit"):
                    EditRackCabinet(CutString(name));
                    break;
                case string n when n.StartsWith("Karty SimEdit"):
                    EditSimCard(CutString(name));
                    break;
                case string n when n.StartsWith("OprogramowanieEdit"):
                    EditSoftware(CutString(name));
                    break;
                case string n when n.StartsWith("UżytkownicyEdit"):
                    EditUser(CutString(name));
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
            CreateWindows<AllDictionaryWindow, AllPrinterTypeViewModelWindow>(window => new AllPrinterTypeViewModelWindow(window));
        }
        
        private void CreateStatusWindow()
        {
            CreateWindows<NewDictionaryWindow, NewStatuseViewModel>(window => new NewStatuseViewModel(window));
        }

        private void CreatePrinterTypeWindow()
        {
            CreateWindows<NewDictionaryWindow, NewPrinterTypeWindowViewModel>(window => new NewPrinterTypeWindowViewModel(window));
        }

        private void ShowPrinterModelWindow()
        {
            CreateWindows<AllDictionaryWindow, AllPrinterModelViewModelWindow>(window => new AllPrinterModelViewModelWindow(window));
        }

        private void CreatePrinterModelWindow()
        {
            CreateWindows<NewDictionaryWindow, NewPrinterModelViewModel>(window => new NewPrinterModelViewModel(window));       
        }

        private void ShowStatusWindow()
        {
            CreateWindows<AllStatusWindow, AllStatusViewModelWindow>(window => new AllStatusViewModelWindow(window));
        }

        private void ShowManufacturerWindow()
        {
            CreateWindows<AllDictionaryWindow, AllManufacturerViewModelWindow>(window => new AllManufacturerViewModelWindow(window));
        }

        private void ShowUsersWindow()
        {
            CreateWindows<AllUsersWindow, AllUserViewModelWindow>(window => new AllUserViewModelWindow(window));
        }

        private void ShowLocationWindow()
        {
            CreateWindows<AllLocationsWindow, AllLocationsViewModelWindow>(window => new AllLocationsViewModelWindow(window));
        }

        private void ShowSimCardWindow()
        {
            CreateWindows<AllSimCardWindow, AllSimCardViewModelWindow>(window => new AllSimCardViewModelWindow(window));
        }

        private void ShowSimComponentTypeWindow()
        {
            CreateWindows<AllDictionaryWindow, AllSimComponentTypeViewModelWindow>(window => new AllSimComponentTypeViewModelWindow(window));
        }

        private void CreateSimComponentTypeWindow()
        {
            CreateWindows<NewDictionaryWindow, NewSimComponentTypeViewModel>(window => new NewSimComponentTypeViewModel(window));
        }

        private void ShowRackCabinetTypeWindow()
        {
            CreateWindows<AllDictionaryWindow, AllRackCabinetTypeViewModelWindow>(window => new AllRackCabinetTypeViewModelWindow(window));
        }

        private void CreateRackCabinetTypeWindow()
        {
            CreateWindows<NewDictionaryWindow, NewRackCabinetTypeViewModel>(window => new NewRackCabinetTypeViewModel(window));
        }

        private void ShowRackCabinetModelWindow()
        {
            CreateWindows<AllDictionaryWindow, AllRackCabinetModelViewModelWindow>(window => new AllRackCabinetModelViewModelWindow(window));
        }

        private void CreateRackCabinetModelWindow()
        {
            CreateWindows<NewDictionaryWindow, NewRackCabinetModelViewModel>(window => new NewRackCabinetModelViewModel(window));
        }

        private void ShowPhoneTypeWindow()
        {
            CreateWindows<AllDictionaryWindow, AllPhoneTypeViewModelWindow>(window => new AllPhoneTypeViewModelWindow(window));
        }

        private void CreatePhoneTypeWindow()
        {
            CreateWindows<NewDictionaryWindow, NewPhoneTypeViewModel>(window => new NewPhoneTypeViewModel(window));
        }

        private void ShowPhoneModelWindow()
        {
            CreateWindows<AllDictionaryWindow, AllPhoneModelViewModelWindow>(window => new AllPhoneModelViewModelWindow(window));
        }

        private void CreatePhoneModelWindow()
        {
            CreateWindows<NewDictionaryWindow, NewPhoneModelViewModel>(window => new NewPhoneModelViewModel(window));
        }

        private void ShowNetworkDeviceTypeWindow()
        {
            CreateWindows<AllDictionaryWindow, AllNetworkDeviceTypeViewModelWindow>(window => new AllNetworkDeviceTypeViewModelWindow(window));
        }

        private void CreateNetworkDeviceTypeWindow()
        {
            CreateWindows<NewDictionaryWindow, NewNetworkDeviceTypeViewModel>(window => new NewNetworkDeviceTypeViewModel(window));
        }

        private void ShowNetworkDeviceModelWindow()
        {
            CreateWindows<AllDictionaryWindow, AllNetworkDeviceModelViewModelWindow>(window => new AllNetworkDeviceModelViewModelWindow(window));
        }

        private void CreateNetworkDeviceModelWindow()
        {
            CreateWindows<NewDictionaryWindow, NewNetworkDeviceModelViewModel>(window => new NewNetworkDeviceModelViewModel(window));
        }

        private void ShowMonitorTypeWindow()
        {
            CreateWindows<AllDictionaryWindow, AllMonitorTypeViewModelWindow>(window => new AllMonitorTypeViewModelWindow(window));
        }

        private void CreateMonitorTypeWindow()
        {
            CreateWindows<NewDictionaryWindow, NewMonitorTypeViewModel>(window => new NewMonitorTypeViewModel(window));
        }

        private void ShowMonitorModelWindow()
        {
            CreateWindows<AllDictionaryWindow, AllMonitorModelViewModelWindow>(window => new AllMonitorModelViewModelWindow(window));
        }

        private void CreateMonitorModelWindow()
        {
            CreateWindows<NewDictionaryWindow, NewMonitorModelViewModel>(window => new NewMonitorModelViewModel(window));
        }

        private void ShowDeviceTypeWindow()
        {
            CreateWindows<AllDictionaryWindow, AllDeviceTypeViewModelWindow>(window => new AllDeviceTypeViewModelWindow(window));
        }

        private void CreateDeviceTypeWindow()
        {
            CreateWindows<NewDictionaryWindow, NewDeviceTypeViewModel>(window => new NewDeviceTypeViewModel(window));
        }

        private void ShowDeviceModelWindow()
        {
            CreateWindows<AllDictionaryWindow, AllDeviceModelViewModelWindow>(window => new AllDeviceModelViewModelWindow(window));
        }

        private void CreateDeviceModelWindow()
        {
            CreateWindows<NewDictionaryWindow, NewDeviceModelViewModel>(window => new NewDeviceModelViewModel(window));
        }

        private void ShowOperatingSystemWindow()
        {
            CreateWindows<AllDictionaryWindow, AllOperatingSystemViewModelWindow>(window => new AllOperatingSystemViewModelWindow(window));
        }

        private void CreateOperatingSystemWindow()
        {
            CreateWindows<NewDictionaryWindow, NewOperatingSystemViewModel>(window => new NewOperatingSystemViewModel(window));
        }

        private void ShowComputerTypeWindow()
        {
            CreateWindows<AllDictionaryWindow, AllComputerTypeViewModelWindow>(window => new AllComputerTypeViewModelWindow(window));
        }

        private void CreateComputerTypeWindow()
        {
            CreateWindows<NewDictionaryWindow, NewComputerTypeViewModel>(window => new NewComputerTypeViewModel(window));
        }

        private void ShowComputerModelWindow()
        {
            CreateWindows<AllDictionaryWindow, AllComputerModelViewModelWindow>(window => new AllComputerModelViewModelWindow(window));
        }

        private void CreateComputerModelWindow()
        {
            CreateWindows<NewDictionaryWindow, NewComputerModelViewModel>(window => new NewComputerModelViewModel(window));
        }

        private async void EditComputer(string id)
        {
            await EditItem<ComputersCreateEditVM, EditComputerViewModel>(id, URLs.COMPUTERS_CEVM_ID, vm => new EditComputerViewModel(vm));
        }

        private async void EditDevice(string id)
        {
            try
            {
                DevicesCreateEditVM vm = await RequestHelper.SendRequestAsync<object, DevicesCreateEditVM>(URLs.DEVICE_CEVM_ID.Replace("{id}", id), HttpMethod.Get, null, null);
                if (vm != null)
                {
                    EditDeviceViewModel workspace = new EditDeviceViewModel(vm);
                    Workspaces.Add(workspace);
                    SetActiveWorkspace(workspace);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.WriteLine(ex.Message);
            }

        }


        private async void EditMonitor(string id)
        {
            try
            {
                MonitorsCreateEditVM vm = await RequestHelper.SendRequestAsync<object, MonitorsCreateEditVM>(URLs.MONITORS_CEVM_ID.Replace("{id}", id), HttpMethod.Get, null, null);
                if (vm != null)
                {
                    EditMonitorViewModel workspace = new EditMonitorViewModel(vm);
                    Workspaces.Add(workspace);
                    SetActiveWorkspace(workspace);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.WriteLine(ex.Message);
            }

        }


        private async void EditNetworkDevice(string id)
        {
            try
            {
                NetworkDeviceCreateEditVM vm = await RequestHelper.SendRequestAsync<object, NetworkDeviceCreateEditVM>(URLs.NETWORKDEVICE_CEVM_ID.Replace("{id}", id), HttpMethod.Get, null, null);
                if (vm != null)
                {
                    EditNetworkDeviceViewModel workspace = new EditNetworkDeviceViewModel(vm);
                    Workspaces.Add(workspace);
                    SetActiveWorkspace(workspace);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.WriteLine(ex.Message);
            }

        }


        private async void EditPhone(string id)
        {
            try
            {
                PhonesCreateEditVM vm = await RequestHelper.SendRequestAsync<object, PhonesCreateEditVM>(URLs.PHONE_CEVM_ID.Replace("{id}", id), HttpMethod.Get, null, null);
                if (vm != null)
                {
                    EditPhoneViewModel workspace = new EditPhoneViewModel(vm);
                    Workspaces.Add(workspace);
                    SetActiveWorkspace(workspace);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.WriteLine(ex.Message);
            }

        }


        private async void EditPrinter(string id)
        {
            try
            {
                PrintersCreateEditVM vm = await RequestHelper.SendRequestAsync<object, PrintersCreateEditVM>(URLs.PRINTER_CEVM_ID.Replace("{id}", id), HttpMethod.Get, null, null);
                if (vm != null)
                {
                    EditPrinterViewModel workspace = new EditPrinterViewModel(vm);
                    Workspaces.Add(workspace);
                    SetActiveWorkspace(workspace);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.WriteLine(ex.Message);
            }

        }


        private async void EditRackCabinet(string id)
        {
            try
            {
                RackCabinetCreateEditVM vm = await RequestHelper.SendRequestAsync<object, RackCabinetCreateEditVM>(URLs.RACKCABINET_CEVM_ID.Replace("{id}", id), HttpMethod.Get, null, null);
                if (vm != null)
                {
                    EditRackCabinetViewModel workspace = new EditRackCabinetViewModel(vm);
                    Workspaces.Add(workspace);
                    SetActiveWorkspace(workspace);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.WriteLine(ex.Message);
            }

        }


        private async void EditSimCard(string id)
        {
            try
            {
                SimCardsCreateEditVM vm = await RequestHelper.SendRequestAsync<object, SimCardsCreateEditVM>(URLs.SIMCARD_CEVM_ID.Replace("{id}", id), HttpMethod.Get, null, null);
                if (vm != null)
                {
                    EditSimCardViewModel workspace = new EditSimCardViewModel(vm);
                    Workspaces.Add(workspace);
                    SetActiveWorkspace(workspace);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.WriteLine(ex.Message);
            }

        }


        private async void EditSoftware(string id)
        {
            try
            {
                SoftwareCreateEditVM vm = await RequestHelper.SendRequestAsync<object, SoftwareCreateEditVM>(URLs.SOFTWARE_CEVM_ID.Replace("{id}", id), HttpMethod.Get, null, null);
                if (vm != null)
                {
                    EditSoftwareViewModel workspace = new EditSoftwareViewModel(vm);
                    Workspaces.Add(workspace);
                    SetActiveWorkspace(workspace);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.WriteLine(ex.Message);
            }

        }


        private async void EditUser(string id)
        {
            try
            {
                UserCreateEditVM vm = await RequestHelper.SendRequestAsync<object, UserCreateEditVM>(URLs.USER_CEVM_ID.Replace("{id}", id), HttpMethod.Get, null, null);
                if (vm != null)
                {
                    EditUserViewModel workspace = new EditUserViewModel(vm);
                    Workspaces.Add(workspace);
                    SetActiveWorkspace(workspace);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.WriteLine(ex.Message);
            }

        }




        #endregion
    }
}
