using AdministrationApp.ViewModels;
using AdministrationApp.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace AdministrationApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //łaczymy viewModel z View
            MainWindow window = new MainWindow();
            var viewModel = new MainWindowViewModel();
            window.DataContext = viewModel;
            window.Show();
        }
    }

}
