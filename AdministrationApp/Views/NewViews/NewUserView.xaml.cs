using AdministrationApp.ViewModels.NewViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdministrationApp.Views.NewViews
{
    /// <summary>
    /// Logika interakcji dla klasy NewUserView.xaml
    /// </summary>
    public partial class NewUserView : JedenViewBase
    {
        public NewUserView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(DataContext is NewUserViewModel viewModel)
            {
                viewModel.Password = (sender as PasswordBox).Password;
            }
        }
    }
}
