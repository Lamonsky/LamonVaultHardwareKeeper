using AdministrationApp.ViewModels;
using System.Windows;

namespace AdministrationApp.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            LoginWindowViewModel viewmodel = new LoginWindowViewModel();
            DataContext = viewmodel;
            viewmodel.OnRequestClose += (s, e) => this.Close();
        }
    }
}
