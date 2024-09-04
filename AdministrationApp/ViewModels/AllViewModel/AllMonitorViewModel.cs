using AdministrationApp.Helpers;
using Data.Computers.SelectVMs;
using Data;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using AdministrationApp.ViewModels.NewViewModel;

namespace AdministrationApp.ViewModels.AllViewModel
{
    class AllMonitorViewModel : WszystkieViewModel<MonitorsVM>
    {
        public AllMonitorViewModel() : base("Monitory")
        {
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            if (name == "MonitorRefresh")
            {
                load();
            }
        }
        public override void Filter()
        {
            switch(FilterField)
            {
                default:
                    List = new List<MonitorsVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Location?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.ManufacturerName?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Model?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.MonitorType?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.SerialNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.InventoryNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Users?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Nazwa":
                    List = new List<MonitorsVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Lokalizacja":
                    List = new List<MonitorsVM>(
                        List.Where(item =>
                            (item.Location?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Status":
                    List = new List<MonitorsVM>(
                        List.Where(item =>
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Typ monitora":
                    List = new List<MonitorsVM>(
                        List.Where(item =>
                            (item.MonitorType?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Producent":
                    List = new List<MonitorsVM>(
                        List.Where(item =>
                            (item.ManufacturerName?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Model":
                    List = new List<MonitorsVM>(
                        List.Where(item =>
                            (item.Model?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Numer seryjny":
                    List = new List<MonitorsVM>(
                        List.Where(item =>
                            (item.SerialNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Numer inwentarzowy":
                    List = new List<MonitorsVM>(
                        List.Where(item =>
                            (item.InventoryNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Osoba odpowiedzialna":
                    List = new List<MonitorsVM>(
                        List.Where(item =>
                            (item.Users?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
            }
        }

        public override List<string> GetComboBoxFilterList()
        {
            return new List<string>
            {
                
                "Nazwa",
                "Lokalizacja",
                "Status",
                "Typ monitora",
                "Producent",
                "Model",
                "Numer seryjny",
                "Numer inwentarzowy",
                "Osoba odpowiedzialna",
            };
        }


        public override async void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<MonitorsVM>>(URLs.MONITORS, HttpMethod.Get, null, GlobalData.AccessToken);
        }

        private MonitorsVM _ChosenMonitor;
        public MonitorsVM ChosenMonitor
        {
            set
            {
                if (_ChosenMonitor != value)
                {
                    _ChosenMonitor = value;
                }
            }
            get
            {
                return _ChosenMonitor;
            }
        }
        public override void Edit()
        {
            Messenger.Default.Send(DisplayName + "Edit/" + ChosenMonitor.Id);
        }

        public override async void Remove()
        {
            await RequestHelper.SendRequestAsync(URLs.MONITORS_ID.Replace("{id}", ChosenMonitor.Id.ToString()), HttpMethod.Delete, ChosenMonitor, null);
            load();
        }

        public override void send()
        {
            throw new NotImplementedException();
        }
    }
}
