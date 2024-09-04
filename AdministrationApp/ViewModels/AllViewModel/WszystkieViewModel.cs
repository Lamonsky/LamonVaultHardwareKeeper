using AdministrationApp.Helpers;
using Data.Computers.CreateEditVMs;
using Data;
using GalaSoft.MvvmLight.Messaging;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Input;
using ClosedXML.Excel;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Drawing;
using System.Reflection;

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
                    _RemoveCommand = new BaseCommand(() => RemoveAndSaveLogs());
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
        private BaseCommand _GenerateExcelCommand;
        public ICommand GenerateExcelCommand
        {
            get
            {
                if (_GenerateExcelCommand == null)
                {
                    _GenerateExcelCommand = new BaseCommand(() => GenerateExcel(List));
                }
                return _GenerateExcelCommand;
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
                IsLoading = true;
                OnPropertyChanged(() => List);
            }
        }
        private T _ChosenItem;
        public T ChosenItem
        {
            get
            {
                return _ChosenItem;
            }
            set
            {
                _ChosenItem = value;
                OnPropertyChanged(() => ChosenItem);
            }
        }
        #endregion
        #region Konstruktor
        public WszystkieViewModel(string displayName)
        {
            IsLoading = false;
            DisplayName = displayName;
            load();

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
        public void RemoveAndSaveLogs()
        {
            Remove();
            RemoveSaveLogs(ChosenItem);
        }
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
        #endregion
        public async void RemoveSaveLogs(T newvm)
        {
            string newitem = JsonSerializer.Serialize(newvm);
            LogCreateEditVM log = new();
            log.LogDate = DateTime.Now;
            log.Users = GlobalData.UserId;
            log.CreatedAt = DateTime.Now;
            log.CreatedBy = GlobalData.UserId;
            log.Description = $"Usunięto rekord w tabeli {DisplayName}. Rekord {newitem}.";
            await RequestHelper.SendRequestAsync(URLs.LOG, HttpMethod.Post, log, GlobalData.AccessToken);
        }
        private void GenerateExcel<T>(List<T> dataList)
        {
            string selectedPath = "C:\\";
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                selectedPath = folderBrowserDialog.SelectedPath;
            }
            string data = DateTime.Now.ToString("yyyy-MM-dd");
            string csvFilePath = $"{selectedPath}\\raport_{DisplayName}_{DateTime.Now.ToString("yyyy_mm_dd").Replace(" ","")}_{DateTime.Now.ToString("HH_mm_ss")}.xlsx";
            
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(DisplayName);

                // Pobierz właściwości typu T
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                // Utwórz nagłówki kolumn w Excelu na podstawie nazw właściwości
                for (int i = 0; i < properties.Length; i++)
                {
                    worksheet.Cell(1, i + 1).Value = properties[i].Name;
                }

                // Wypełnij arkusz danymi
                for (int i = 0; i < dataList.Count; i++)
                {
                    for (int j = 0; j < properties.Length; j++)
                    {
                        var value = properties[j].GetValue(dataList[i]);

                        // Obsługa różnych typów danych
                        if (value is int)
                        {
                            worksheet.Cell(i + 2, j + 1).Value = (int)value;
                        }
                        else if (value is double)
                        {
                            worksheet.Cell(i + 2, j + 1).Value = (double)value;
                        }
                        else if (value is DateTime)
                        {
                            worksheet.Cell(i + 2, j + 1).Value = (DateTime)value;
                        }
                        else if (value is bool)
                        {
                            worksheet.Cell(i + 2, j + 1).Value = (bool)value;
                        }
                        else
                        {
                            worksheet.Cell(i + 2, j + 1).Value = value?.ToString();
                        }
                    }
                }

                // Zapisz plik na dysku
                workbook.SaveAs(csvFilePath);
                MessageBox.Show($"Zapisano plik pod ścieżką {csvFilePath}");
            }
        }
    }
}
