using AdministrationApp.ViewModels.AllViewModel;
using AdministrationApp.ViewModels.NewViewModel.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdministrationApp.Views.NewViews.Windows
{
    /// <summary>
    /// Interaction logic for NewPrinterTypeWindow.xaml
    /// </summary>
    public partial class NewPrinterTypeWindow : Window
    {
        public NewPrinterTypeWindow()
        {
            InitializeComponent();
            NewPrinterTypeWindowViewModel viewmodel = new NewPrinterTypeWindowViewModel();
            DataContext = viewmodel;
            viewmodel.OnRequestClose += (s, e) => this.Close();
        }
    }
}
