using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using AdministrationApp.Helpers;
using AdministrationApp.ViewModels.AllViewModel;
using AdministrationApp.ViewModels.AllViewModel.Windows;
using AdministrationApp.ViewModels.EditViewModel;
using AdministrationApp.ViewModels.EditViewModel.Windows;
using AdministrationApp.ViewModels.NewViewModel;
using AdministrationApp.ViewModels.NewViewModel.Windows;
using AdministrationApp.Views.AllWindows;
using AdministrationApp.Views.NewViews.Windows;
using Data;
using Data.Computers.CreateEditVMs;
using Data.Helpers;
using GalaSoft.MvvmLight.Messaging;

namespace AdministrationApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {


        #region LoggedUser
        public string EMail
        {
            get
            {
                return GlobalData.Email;
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
        private string CutString(string text)
        {
            int index = text.LastIndexOf("/");

            if (index == -1)
            {
                return text;
            }

            return text.Substring(index + 1);
        }
        private void CreateWorkspace<T>() where T : WorkspaceViewModel, new()
        {
            T workspace = new T();
            Workspaces.Add(workspace);
            SetActiveWorkspace(workspace);
        }
        private void ShowAllWorkspace<T>() where T : WorkspaceViewModel, new()
        {
            T workspace = Workspaces.FirstOrDefault(vm => vm is T) as T;
            if (workspace == null)
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
                TViewModel vm = await RequestHelper.SendRequestAsync<object, TViewModel>(url, HttpMethod.Get, null, GlobalData.AccessToken);

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
        public async Task EditItemWindow<TViewModel, TEditViewModel, TWindow>(string id, string urlTemplate,
            Func<Window, TViewModel, TEditViewModel> createViewModel)
            where TViewModel : class
            where TEditViewModel : class
            where TWindow : Window, new()
        {
            try
            {
                string url = urlTemplate.Replace("{id}", id);
                TViewModel vm = await RequestHelper.SendRequestAsync<object, TViewModel>(url, HttpMethod.Get, null, GlobalData.AccessToken);

                if (vm != null)
                {
                    TWindow window = new TWindow(); // Można to zmienić na bardziej uniwersalne okno
                    TEditViewModel editViewModel = createViewModel(window, vm);
                    window.DataContext = editViewModel;
                    window.Show();
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

        }

        private void open(string name)
        {
            switch (name)
            {
                case "HardDriveModelAdd":
                    CreateHardDriveModelWindow();
                    break;
                case "LicenseTypeAdd":
                    CreateLicenseTypeWindow();
                    break;
                case "LocationsAdd":
                    CreateLocationsWindow();
                    break;
                case "ManufacturerAdd":
                    CreateManufacturerWindow();
                    break;
                case "OperatingSystemAdd":
                    CreateOperatingSystemWindow();
                    break;
                case "PositionAdd":
                    CreatePositionWindow();
                    break;
                case "PrinterModelAdd":
                    CreatePrinterModelWindow();
                    break;
                case "PrinterTypeAdd":
                    CreatePrinterTypeWindow();
                    break;
                case "TicketCategoryAdd":
                    CreateTicketCategoryWindow();
                    break;
                case "TicketStatuseAdd":
                    CreateTicketStatusWindow();
                    break;
                case "ChooseTicketStatus":
                    ShowTicketStatusWindow();
                    break;
                case "TicketTypeAdd":
                    CreateTicketTypeWindow();
                    break;
                case "TicketsAdd":
                    CreateTicket();
                    break;
                // Kategoria: Komputery
                case "KomputeryAdd":
                    CreateComputer();
                    break;
                case "ComputerModelAdd":
                    CreateComputerModelWindow();
                    break;
                case "ComputerTypeAdd":
                    CreateComputerTypeWindow();
                    break;
                case "ChooseComputerModel":
                    ShowComputerModelWindow();
                    break;
                case "ChooseComputerType":
                    ShowComputerTypeWindow();
                    break;
                case string n when n.StartsWith("KomputeryEdit"):
                    EditComputer(CutString(name));
                    break;
                case string n when n.StartsWith("ComputerModelEdit"):
                    EditComputerModel(CutString(name));
                    break;

                // Kategoria: Monitory
                case "MonitoryAdd":
                    CreateMonitor();
                    break;
                case "MonitorModelAdd":
                    CreateMonitorModelWindow();
                    break;
                case "MonitorTypeAdd":
                    CreateMonitorTypeWindow();
                    break;
                case "ChooseMonitorModel":
                    ShowMonitorModelWindow();
                    break;
                case "ChooseMonitorType":
                    ShowMonitorTypeWindow();
                    break;
                case string n when n.StartsWith("MonitoryEdit"):
                    EditMonitor(CutString(name));
                    break;

                // Kategoria: Użytkownicy
                case "UżytkownicyAdd":
                    CreateUser();
                    break;
                case "ChooseUser":
                    ShowUsersWindow();
                    break;
                case string n when n.StartsWith("UżytkownicyEdit"):
                    EditUser(CutString(name));
                    break;
                case string n when n.StartsWith("TicketsEdit"):
                    EditTicket(CutString(name));
                    break;

                // Kategoria: Oprogramowanie
                case "OprogramowanieAdd":
                    CreateSoftware();
                    break;
                case string n when n.StartsWith("OprogramowanieEdit"):
                    EditSoftware(CutString(name));
                    break;

                // Kategoria: Urządzenia sieciowe
                case "Urządzenia siecioweAdd":
                    CreateNetworkDevice();
                    break;
                case "NetworkDeviceModelAdd":
                    CreateNetworkDeviceModelWindow();
                    break;
                case "NetworkDeviceTypeAdd":
                    CreateNetworkDeviceTypeWindow();
                    break;
                case "ChooseNetworkDeviceModel":
                    ShowNetworkDeviceModelWindow();
                    break;
                case "ChooseNetworkDeviceType":
                    ShowNetworkDeviceTypeWindow();
                    break;
                case string n when n.StartsWith("Urządzenia siecioweEdit"):
                    EditNetworkDevice(CutString(name));
                    break;

                // Kategoria: Urządzenia
                case "UrządzeniaAdd":
                    CreateDevice();
                    break;
                case "DeviceModelAdd":
                    CreateDeviceModelWindow();
                    break;
                case "DeviceTypeAdd":
                    CreateDeviceTypeWindow();
                    break;
                case "ChooseDeviceModel":
                    ShowDeviceModelWindow();
                    break;
                case "ChooseDeviceType":
                    ShowDeviceTypeWindow();
                    break;
                case string n when n.StartsWith("UrządzeniaEdit"):
                    EditDevice(CutString(name));
                    break;
                case string n when n.StartsWith("LocationEdit"):
                    EditLocation(CutString(name));
                    break;

                // Kategoria: Drukarki
                case "DrukarkiAdd":
                    CreatePrinter();
                    break;
                case "Rodzaje drukarekAdd":
                    CreatePrinterTypeWindow();
                    break;
                case "Modele drukarekAdd":
                    CreatePrinterModelWindow();
                    break;
                case "ChoosePrinterType":
                    ShowPrinterTypeWindow();
                    break;
                case "ChoosePrinterModel":
                    ShowPrinterModelWindow();
                    break;
                case string n when n.StartsWith("DrukarkiEdit"):
                    EditPrinter(CutString(name));
                    break;

                // Kategoria: Telefony
                case "TelefonyAdd":
                    CreatePhone();
                    break;
                case "PhoneModelAdd":
                    CreatePhoneModelWindow();
                    break;
                case "PhoneTypeAdd":
                    CreatePhoneTypeWindow();
                    break;
                case "ChoosePhoneModel":
                    ShowPhoneModelWindow();
                    break;
                case "ChoosePhoneType":
                    ShowPhoneTypeWindow();
                    break;
                case string n when n.StartsWith("TelefonyEdit"):
                    EditPhone(CutString(name));
                    break;

                // Kategoria: Szafy Rack
                case "Szafy RackAdd":
                    CreateRackCabinet();
                    break;
                case "RackCabinetModelAdd":
                    CreateRackCabinetModelWindow();
                    break;
                case "RackCabinetTypeAdd":
                    CreateRackCabinetTypeWindow();
                    break;
                case "ChooseRackCabinetModel":
                    ShowRackCabinetModelWindow();
                    break;
                case "ChooseRackCabinetType":
                    ShowRackCabinetTypeWindow();
                    break;
                case string n when n.StartsWith("Szafy RackEdit"):
                    EditRackCabinet(CutString(name));
                    break;

                // Kategoria: Karty Sim
                case "Karty SimAdd":
                    CreateSimCard();
                    break;
                case "SimComponentTypeAdd":
                    CreateSimComponentTypeWindow();
                    break;
                case "ChooseSimCard":
                    ShowSimCardWindow();
                    break;
                case "ChooseSimComponentType":
                    ShowSimComponentTypeWindow();
                    break;
                case string n when n.StartsWith("Karty SimEdit"):
                    EditSimCard(CutString(name));
                    break;

                // Kategoria: Inne
                case "ChooseLocation":
                    ShowLocationWindow();
                    break;
                case "LocationAdd":
                    CreateLocationsWindow();
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
                case "ChooseTicketOwner":
                    ShowTechnicianWindow();
                    break;
                case "ChooseTicketType":
                    ShowTicketTypeWindow();
                    break;
                case "ChooseTicketCategory":
                    ShowTicketCategoryWindow();
                    break;
                case "TechnicianAdd":
                    CreateTechnicianWindow();
                    break;
                // Pozostałe edycje
                case string n when n.StartsWith("ComputerModelEdit"):
                    EditComputerModel(CutString(name));
                    break;
                case string n when n.StartsWith("ComputerTypeEdit"):
                    EditComputerType(CutString(name));
                    break;
                case string n when n.StartsWith("DeviceModelEdit"):
                    EditDeviceModel(CutString(name));
                    break;
                case string n when n.StartsWith("HardDriveModelEdit"):
                    EditHardDriveModel(CutString(name));
                    break;
                case string n when n.StartsWith("LicenseTypeEdit"):
                    EditLicenseType(CutString(name));
                    break;
                case string n when n.StartsWith("ManufacturerEdit"):
                    EditManufacturer(CutString(name));
                    break;
                case string n when n.StartsWith("MonitorModelEdit"):
                    EditMonitorModel(CutString(name));
                    break;
                case string n when n.StartsWith("MonitorTypeEdit"):
                    EditMonitorType(CutString(name));
                    break;
                case string n when n.StartsWith("NetworkDevicemodelEdit"):
                    EditNetworkDevicemodel(CutString(name));
                    break;
                case string n when n.StartsWith("NetworkDeviceTypeEdit"):
                    EditNetworkDeviceType(CutString(name));
                    break;
                case string n when n.StartsWith("OperatingSystemEdit"):
                    EditOperatingSystem(CutString(name));
                    break;
                case string n when n.StartsWith("PhonemodelEdit"):
                    EditPhonemodel(CutString(name));
                    break;
                case string n when n.StartsWith("PhoneTypeEdit"):
                    EditPhoneType(CutString(name));
                    break;
                case string n when n.StartsWith("PositionEdit"):
                    EditPosition(CutString(name));
                    break;
                case string n when n.StartsWith("PrintermodelEdit"):
                    EditPrintermodel(CutString(name));
                    break;
                case string n when n.StartsWith("PrintertypeEdit"):
                    EditPrintertype(CutString(name));
                    break;
                case string n when n.StartsWith("RackCabinetModelEdit"):
                    EditRackCabinetModel(CutString(name));
                    break;
                case string n when n.StartsWith("RackCabinetTypeEdit"):
                    EditRackCabinetType(CutString(name));
                    break;
                case string n when n.StartsWith("SimComponentTypeEdit"):
                    EditSimComponentType(CutString(name));
                    break;
                case string n when n.StartsWith("StatusEdit"):
                    EditStatus(CutString(name));
                    break;
                case string n when n.StartsWith("TicketCategoryEdit"):
                    EditTicketCategory(CutString(name));
                    break;
                case string n when n.StartsWith("TicketStatuseEdit"):
                    EditTicketStatuse(CutString(name));
                    break;
                case string n when n.StartsWith("TicketTypeEdit"):
                    EditTicketType(CutString(name));
                    break;
                case string n when n.StartsWith("DeviceTypeEdit"):
                    EditDeviceType(CutString(name));
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
                if (_ShowSummaryCommand == null)
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
                if (_ShowComputersCommand == null)
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
        private BaseCommand _ShowServersCommand;
        public ICommand ShowServersCommand
        {
            get
            {
                if (_ShowServersCommand == null)
                {
                    _ShowServersCommand = new BaseCommand(() => ShowServers());
                }
                return _ShowServersCommand;

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
        private BaseCommand _ShowLocationWindowCommand;
        public ICommand ShowLocationWindowCommand
        {
            get
            {
                if (_ShowLocationWindowCommand == null)
                {
                    _ShowLocationWindowCommand = new BaseCommand(() => ShowLocationWindow());
                }
                return _ShowLocationWindowCommand;
            }
        }
        private BaseCommand _AllTicketsCommand;

        public ICommand AllTicketsCommand
        {
            get
            {
                if (_AllTicketsCommand == null)
                {
                    _AllTicketsCommand = new BaseCommand(() => ShowAllTickets());
                }
                return _AllTicketsCommand;
            }
        }
        private BaseCommand _MyTicketsCommand;

        public ICommand MyTicketsCommand
        {
            get
            {
                if (_MyTicketsCommand == null)
                {
                    _MyTicketsCommand = new BaseCommand(() => ShowMyTickets());
                }
                return _MyTicketsCommand;
            }
        }


        #endregion
        #region Funkcje wywolujace okna
        private void CreateComputer()
        {
            CreateWorkspace<NewComputerViewModel>();
        }   
        private void CreateTicket()
        {
            CreateWorkspace<NewTicketViewModel>();
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
        private void ShowServers()
        {
            ShowAllWorkspace<AllServersViewModel>();
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
        private void ShowAllTickets()
        {
            ShowAllWorkspace<AllTicketsViewModel>();
        }
        private void ShowMyTickets()
        {
            ShowAllWorkspace<MyTicketsViewModel>();
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
        private void ShowTechnicianWindow()
        {
            CreateWindows<AllTechniciansWindow, AllTechniciansViewModelWindow>(window => new AllTechniciansViewModelWindow(window));
        }
        private void ShowTicketTypeWindow()
        {
            CreateWindows<AllDictionaryWindow, AllTicketTypeViewModelWindow>(window => new AllTicketTypeViewModelWindow(window));
        }
        private void ShowTicketStatusWindow()
        {
            CreateWindows<AllDictionaryWindow, AllTicketStatuseViewModelWindow>(window => new AllTicketStatuseViewModelWindow(window));
        }
        private void ShowTicketCategoryWindow()
        {
            CreateWindows<AllDictionaryWindow, AllTicketCategoryViewModelWindow>(window => new AllTicketCategoryViewModelWindow(window));
        }

        private void CreateTechnicianWindow()
        {
            CreateWindows<NewTechnicianWindow, NewTechnicianViewModelWindow>(window => new NewTechnicianViewModelWindow(window));
        }
        private void CreateHardDriveModelWindow()
        {
            CreateWindows<NewDictionaryWindow, NewHardDriveModelViewModel>(window => new NewHardDriveModelViewModel(window));
        }
        private void CreateLicenseTypeWindow()
        {
            CreateWindows<NewDictionaryWindow, NewLicenseTypeViewModel>(window => new NewLicenseTypeViewModel(window));
        }
        private void CreateLocationsWindow()
        {
            CreateWindows<NewLocationWindow, NewLocationViewModel>(window => new NewLocationViewModel(window));
        }
        private void CreateManufacturerWindow()
        {
            CreateWindows<NewDictionaryWindow, NewManufacturerViewModel>(window => new NewManufacturerViewModel(window));
        }
        private void CreatePositionWindow()
        {
            CreateWindows<NewDictionaryWindow, NewPositionViewModel>(window => new NewPositionViewModel(window));
        }
        private void CreateTicketCategoryWindow()
        {
            CreateWindows<NewDictionaryWindow, NewTicketCategoryViewModel>(window => new NewTicketCategoryViewModel(window));
        }
        private void CreateTicketStatusWindow()
        {
            CreateWindows<NewDictionaryWindow, NewTicketStatuseViewModel>(window => new NewTicketStatuseViewModel(window));
        }
        private void CreateTicketTypeWindow()
        {
            CreateWindows<NewDictionaryWindow, NewTicketTypeViewModel>(window => new NewTicketTypeViewModel(window));
        }

        private async void EditComputer(string id)
        {
            await EditItem<ComputersCreateEditVM, EditComputerViewModel>(id, URLs.COMPUTERS_CEVM_ID, vm => new EditComputerViewModel(vm));
        }
        private async void EditTicket(string id)
        {
            await EditItem<TicketCreateEditVM, EditTicketViewModel>(id, URLs.TICKET_CEVM_ID, vm => new EditTicketViewModel(vm));
        }
        private async void EditComputerModel(string id)
        {
            await EditItemWindow<ComputerModelCreateEditVM, EditComputerModelViewModel, NewDictionaryWindow>(
                id,
                URLs.COMPUTERMODEL_CEVM_ID,
                (window, vm) => new EditComputerModelViewModel(window, vm)
            );
        }
        private async void EditDevice(string id)
        {
            await EditItem<DevicesCreateEditVM, EditDeviceViewModel>(id, URLs.DEVICE_CEVM_ID, vm => new EditDeviceViewModel(vm));
        }

        private async void EditMonitor(string id)
        {
            await EditItem<MonitorsCreateEditVM, EditMonitorViewModel>(id, URLs.MONITORS_CEVM_ID, vm => new EditMonitorViewModel(vm));
        }

        private async void EditNetworkDevice(string id)
        {
            await EditItem<NetworkDeviceCreateEditVM, EditNetworkDeviceViewModel>(id, URLs.NETWORKDEVICE_CEVM_ID, vm => new EditNetworkDeviceViewModel(vm));
        }

        private async void EditPhone(string id)
        {
            await EditItem<PhonesCreateEditVM, EditPhoneViewModel>(id, URLs.PHONE_CEVM_ID, vm => new EditPhoneViewModel(vm));
        }

        private async void EditPrinter(string id)
        {
            await EditItem<PrintersCreateEditVM, EditPrinterViewModel>(id, URLs.PRINTER_CEVM_ID, vm => new EditPrinterViewModel(vm));
        }

        private async void EditRackCabinet(string id)
        {
            await EditItem<RackCabinetCreateEditVM, EditRackCabinetViewModel>(id, URLs.RACKCABINET_CEVM_ID, vm => new EditRackCabinetViewModel(vm));
        }

        private async void EditSimCard(string id)
        {
            await EditItem<SimCardsCreateEditVM, EditSimCardViewModel>(id, URLs.SIMCARD_CEVM_ID, vm => new EditSimCardViewModel(vm));
        }

        private async void EditSoftware(string id)
        {
            await EditItem<SoftwareCreateEditVM, EditSoftwareViewModel>(id, URLs.SOFTWARE_CEVM_ID, vm => new EditSoftwareViewModel(vm));
        }

        private async void EditUser(string id)
        {
            await EditItem<UserCreateEditVM, EditUserViewModel>(id, URLs.USER_CEVM_ID, vm => new EditUserViewModel(vm));
        }

        private async void EditComputerType(string id)
        {
            await EditItemWindow<ComputerTypeCreateEditVM, EditComputerTypeViewModel, NewDictionaryWindow>(
                id,
                URLs.COMPUTERTYPE_CEVM_ID,
                (window, vm) => new EditComputerTypeViewModel(window, vm)
            );
        }

        private async void EditDeviceModel(string id)
        {
            await EditItemWindow<DeviceModelCreateEditVM, EditDeviceModelViewModel, NewDictionaryWindow>(
                id,
                URLs.DEVICEMODEL_CEVM_ID,
                (window, vm) => new EditDeviceModelViewModel(window, vm)
            );
        }
        private async void EditLocation(string id)
        {
            await EditItemWindow<LocationCreateEditVM, EditLocationViewModel, NewLocationWindow>(
                id,
                URLs.LOCATION_CEVM_ID,
                (window, vm) => new EditLocationViewModel(window, vm)
            );
        }



        private async void EditDeviceType(string id)
        {
            await EditItemWindow<DeviceTypeCreateEditVM, EditDeviceTypeViewModel, NewDictionaryWindow>(
                id,
                URLs.DEVICETYPE_CEVM_ID,
                (window, vm) => new EditDeviceTypeViewModel(window, vm)
            );
        }



        private async void EditHardDriveModel(string id)
        {
            await EditItemWindow<HardDriveModelCreateEditVM, EditHardDriveModelViewModel, NewDictionaryWindow>(
                id,
                URLs.HARDDRIVEMODEL_CEVM_ID,
                (window, vm) => new EditHardDriveModelViewModel(window, vm)
            );
        }



        private async void EditLicenseType(string id)
        {
            await EditItemWindow<LicenseTypeCreateEditVM, EditLicenseTypeViewModel, NewDictionaryWindow>(
                id,
                URLs.LICENSETYPE_CEVM_ID,
                (window, vm) => new EditLicenseTypeViewModel(window, vm)
            );
        }


        private async void EditManufacturer(string id)
        {
            await EditItemWindow<ManufacturerCreateEditVM, EditManufacturerViewModel, NewDictionaryWindow>(
                id,
                URLs.MANUFACTURER_CEVM_ID,
                (window, vm) => new EditManufacturerViewModel(window, vm)
            );
        }



        private async void EditMonitorModel(string id)
        {
            await EditItemWindow<MonitorModelCreateEditVM, EditMonitorModelViewModel, NewDictionaryWindow>(
                id,
                URLs.MONITORMODEL_CEVM_ID,
                (window, vm) => new EditMonitorModelViewModel(window, vm)
            );
        }



        private async void EditMonitorType(string id)
        {
            await EditItemWindow<MonitorTypeCreateEditVM, EditMonitorTypeViewModel, NewDictionaryWindow >(
                id,
                URLs.MONITORTYPE_CEVM_ID,
                (window, vm) => new EditMonitorTypeViewModel(window, vm)
            );
        }



        private async void EditNetworkDevicemodel(string id)
        {
            await EditItemWindow<NetworkDeviceModelCreateEditVM, EditNetworkDevicemodelViewModel, NewDictionaryWindow>(
                id,
                URLs.NETWORKDEVICEMODEL_CEVM_ID,
                (window, vm) => new EditNetworkDevicemodelViewModel(window, vm)
            );
        }



        private async void EditNetworkDeviceType(string id)
        {
            await EditItemWindow<NetworkDeviceTypeCreateEditVM, EditNetworkDeviceTypeViewModel, NewDictionaryWindow>(
                id,
                URLs.NETWORKDEVICETYPE_CEVM_ID,
                (window, vm) => new EditNetworkDeviceTypeViewModel(window, vm)
            );
        }



        private async void EditOperatingSystem(string id)
        {
            await EditItemWindow<OperatingSystemCreateEditVM, EditOperatingSystemViewModel, NewDictionaryWindow>(
                id,
                URLs.OPERATINGSYSTEM_CEVM_ID,
                (window, vm) => new EditOperatingSystemViewModel(window, vm)
            );
        }



        private async void EditPhonemodel(string id)
        {
            await EditItemWindow<PhoneModelCreateEditVM, EditPhonemodelViewModel, NewDictionaryWindow>(
                id,
                URLs.PHONEMODEL_CEVM_ID,
                (window, vm) => new EditPhonemodelViewModel(window, vm)
            );
        }



        private async void EditPhoneType(string id)
        {
            await EditItemWindow<PhoneTypeCreateEditVM, EditPhoneTypeViewModel, NewDictionaryWindow>(
                id,
                URLs.PHONETYPE_CEVM_ID,
                (window, vm) => new EditPhoneTypeViewModel(window, vm)
            );
        }



        private async void EditPosition(string id)
        {
            await EditItemWindow<PositionCreateEditVM, EditPositionViewModel, NewDictionaryWindow>(
                id,
                URLs.POSITION_CEVM_ID,
                (window, vm) => new EditPositionViewModel(window, vm)
            );
        }



        private async void EditPrintermodel(string id)
        {
            await EditItemWindow<PrinterModelCreateEditVM, EditPrintermodelViewModel, NewDictionaryWindow>(
                id,
                URLs.PRINTERMODEL_CEVM_ID,
                (window, vm) => new EditPrintermodelViewModel(window, vm)
            );
        }



        private async void EditPrintertype(string id)
        {
            await EditItemWindow<PrinterTypeCreateEditVM, EditPrintertypeViewModel, NewDictionaryWindow>(
                id,
                URLs.PRINTERTYPE_CEVM_ID,
                (window, vm) => new EditPrintertypeViewModel(window, vm)
            );
        }



        private async void EditRackCabinetModel(string id)
        {
            await EditItemWindow<RackCabinetModelCreateEditVM, EditRackCabinetModelViewModel, NewDictionaryWindow>(
                id,
                URLs.RACKCABINETMODEL_CEVM_ID,
                (window, vm) => new EditRackCabinetModelViewModel(window, vm)
            );
        }



        private async void EditRackCabinetType(string id)
        {
            await EditItemWindow<RackCabinetTypeCreateEditVM, EditRackCabinetTypeViewModel, NewDictionaryWindow>(
                id,
                URLs.RACKCABINETTYPE_CEVM_ID,
                (window, vm) => new EditRackCabinetTypeViewModel(window, vm)
            );
        }



        private async void EditSimComponentType(string id)
        {
            await EditItemWindow<SimComponentTypeCreateEditVM, EditSimComponentTypeViewModel, NewDictionaryWindow>(
                id,
                URLs.SIMCOMPONENTTYPE_CEVM_ID,
                (window, vm) => new EditSimComponentTypeViewModel(window, vm)
            );
        }



        private async void EditStatus(string id)
        {
            await EditItemWindow<StatusCreateEditVM, EditStatusViewModel, NewDictionaryWindow>(
                id,
                URLs.STATUS_CEVM_ID,
                (window, vm) => new EditStatusViewModel(window, vm)
            );
        }



        private async void EditTicketCategory(string id)
        {
            await EditItemWindow<TicketCategoryCreateEditVM, EditTicketCategoryViewModel, NewDictionaryWindow>(
                id,
                URLs.TICKETCATEGORY_CEVM_ID,
                (window, vm) => new EditTicketCategoryViewModel(window, vm)
            );
        }



        private async void EditTicketStatuse(string id)
        {
            await EditItemWindow<TicketStatusCreateEditVM, EditTicketStatuseViewModel, NewDictionaryWindow>(
                id,
                URLs.TICKETSTATUS_CEVM_ID,
                (window, vm) => new EditTicketStatuseViewModel(window, vm)
            );
        }



        private async void EditTicketType(string id)
        {
            await EditItemWindow<TicketTypeCreateEditVM, EditTicketTypeViewModel, NewDictionaryWindow>(
                id,
                URLs.TICKETTYPE_CEVM_ID,
                (window, vm) => new EditTicketTypeViewModel(window, vm)
            );
        }





        #endregion
    }
}
