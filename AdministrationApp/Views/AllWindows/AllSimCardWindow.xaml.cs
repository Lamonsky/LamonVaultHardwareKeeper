using AdministrationApp.ViewModels.AllViewModel.Windows;
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

namespace AdministrationApp.Views.AllWindows
{
    /// <summary>
    /// Interaction logic for AllSimCardWindow.xaml
    /// </summary>
    public partial class AllSimCardWindow : Window
    {
        public AllSimCardWindow()
        {
            InitializeComponent();
            AllSimCardViewModelWindow allSimCardViewModelWindow= new AllSimCardViewModelWindow();
            DataContext = allSimCardViewModelWindow;
            allSimCardViewModelWindow.OnRequestClose += (s, e) => this.Close();
        }
    }
}
