using AdministrationApp.ViewModels.AllViewModel;
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
    /// Logika interakcji dla klasy AllStatusWindow.xaml
    /// </summary>
    public partial class AllUsersWindow : Window
    {
        public AllUsersWindow()
        {
            InitializeComponent();
            AllUserViewModelWindow allUsersViewModelWindow = new AllUserViewModelWindow();
            DataContext = allUsersViewModelWindow;
            allUsersViewModelWindow.OnRequestClose += (s, e) => this.Close();
        }
    }
}
