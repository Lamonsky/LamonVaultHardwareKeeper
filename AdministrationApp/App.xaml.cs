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


            LoginWindow window = new LoginWindow();
            var loginviewmodel = new LoginWindowViewModel(window);
            window.DataContext = loginviewmodel;
            window.Show();


        }
    }

}
