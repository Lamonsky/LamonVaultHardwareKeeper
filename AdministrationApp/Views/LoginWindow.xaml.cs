using AdministrationApp.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace AdministrationApp.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginWindowViewModel viewModel)
            {
                viewModel.Password = (sender as PasswordBox)?.Password;
            }
        }
    }
}
