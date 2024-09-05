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

namespace AdministrationApp.ViewModels.AllViewModel
{
    class AllLicenseViewModel : WszystkieViewModel<LicenseVM>
    {
        public AllLicenseViewModel() : base("Licencje")
        {
            Messenger.Default.Register<string>(this, open);
        }
        private void open(string name)
        {
            if (name == "LicenseRefresh")
            {
                load();
            }
        }
        public override void Edit()
        {
            Messenger.Default.Send(DisplayName + "Edit/" + ChosenItem.Id);
        }

        public override void Filter()
        {
            switch (FilterField)
            {
                default:
                    List = new List<LicenseVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Software?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.LicenseType?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Publisher?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.Location?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.SerialNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.InventoryNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                            (item.User?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Nazwa":
                    List = new List<LicenseVM>(
                        List.Where(item =>
                            (item.Name?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Status":
                    List = new List<LicenseVM>(
                        List.Where(item =>
                            (item.Status?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Typ licencji":
                    List = new List<LicenseVM>(
                        List.Where(item =>
                            (item.LicenseType?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Producent":
                    List = new List<LicenseVM>(
                        List.Where(item =>
                            (item.Publisher?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Oprogramowanie":
                    List = new List<LicenseVM>(
                        List.Where(item =>
                            (item.Software?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Numer seryjny":
                    List = new List<LicenseVM>(
                        List.Where(item =>
                            (item.SerialNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Numer inwentarzowy":
                    List = new List<LicenseVM>(
                        List.Where(item =>
                            (item.InventoryNumber?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Lokalizacja":
                    List = new List<LicenseVM>(
                        List.Where(item =>
                            (item.Location?.Contains(FindTextBox, StringComparison.CurrentCultureIgnoreCase) ?? false)
                        ).ToList()
                    );
                    break;
                case "Osoba odpowiedzialna":
                    List = new List<LicenseVM>(
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
                "Oprogramowanie",
                "Typ licencji",
                "Producent",
                "Status",
                "Lokalizacja",
                "Numer seryjny",
                "Numer inwentarzowy",
                "Osoba odpowiedzialna"
            };
        }

        public async override void load()
        {
            List = await RequestHelper.SendRequestAsync<object, List<LicenseVM>>(URLs.LICENSE, HttpMethod.Get, null, GlobalData.AccessToken);
        }

        public async override void Remove()
        {
            
            await RequestHelper.SendRequestAsync(URLs.LICENSE_ID.Replace("{id}", ChosenItem.Id.ToString()), HttpMethod.Delete, ChosenItem, GlobalData.AccessToken);
            load();
        }

        public override void send()
        {
            throw new NotImplementedException();
        }
    }
}
