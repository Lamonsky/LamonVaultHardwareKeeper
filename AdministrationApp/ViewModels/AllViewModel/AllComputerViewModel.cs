using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AdministrationApp.Helpers;
using Data;
using Data.Computers.SelectVMs;
using GalaSoft.MvvmLight.Messaging;

namespace AdministrationApp.ViewModels.AllViewModel
{
    public class AllComputerViewModel : WszystkieViewModel<ComputersVM>
    {
        
        public AllComputerViewModel() : base("Komputery")
        {
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            if (name == "ComputersRefresh")
            {
                load();
            }
        }
        public override void Filter()
        {
            switch(FilterField)
            {
                default:
                    List = new List<ComputersVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.ManufacturerName?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Location?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.ComputerType?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.ComputerModel?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Processor?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Ram?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Disk?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.GraphicsCard?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.OperatingSystem?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.SerialNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.InventoryNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.User?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Nazwa":
                    List = new List<ComputersVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Producent":
                    List = new List<ComputersVM>(
                        List.Where(item =>
                            (item.ManufacturerName?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Lokalizacja":
                    List = new List<ComputersVM>(
                        List.Where(item =>
                            (item.Location?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Status":
                    List = new List<ComputersVM>(
                        List.Where(item =>
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Typ Komputera":
                    List = new List<ComputersVM>(
                        List.Where(item =>
                            (item.ComputerType?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Model":
                    List = new List<ComputersVM>(
                        List.Where(item =>
                            (item.ComputerModel?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Procesor":
                    List = new List<ComputersVM>(
                        List.Where(item =>
                            (item.Processor?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "RAM":
                    List = new List<ComputersVM>(
                        List.Where(item =>
                            (item.Ram?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Dysk Twardy":
                    List = new List<ComputersVM>(
                        List.Where(item =>
                            (item.Disk?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Karta graficzna":
                    List = new List<ComputersVM>(
                        List.Where(item =>
                            (item.GraphicsCard?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "System operacyjny":
                    List = new List<ComputersVM>(
                        List.Where(item =>
                            (item.OperatingSystem?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Numer seryjny":
                    List = new List<ComputersVM>(
                        List.Where(item =>
                            (item.SerialNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Numer inwentarzowy":
                    List = new List<ComputersVM>(
                        List.Where(item =>
                            (item.InventoryNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Osoba odpowiedzialna":
                    List = new List<ComputersVM>(
                        List.Where(item =>
                            (item.User?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
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
                "Producent",
                "Lokalizacja",
                "Status",
                "Typ Komputera",
                "Model",
                "Procesor",
                "RAM",
                "Dysk Twardy",
                "Karta graficzna",
                "System operacyjny",
                "Numer seryjny",
                "Numer inwentarzowy",
                "Osoba odpowiedzialna"
            };
        }

        public override async void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<ComputersVM>>(URLs.COMPUTERS, HttpMethod.Get, null, GlobalData.AccessToken);
        }
        public override void Edit()
        {
            if (ChosenItem != null)
            {
                
                Messenger.Default.Send(DisplayName + "Edit/" + ChosenItem.Id);
            }
            else 
            {
                MessageBox.Show("Nie wybrano żadnego rekordu do edycji");
            }
            
        }

        public override async void Remove()
        {
            await RequestHelper.SendRequestAsync(URLs.COMPUTERS_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, GlobalData.AccessToken);
            load();
        }

        public override void send()
        {
        }
    }
}
