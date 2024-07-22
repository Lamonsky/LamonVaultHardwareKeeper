using System.Windows;
using System.Windows.Controls;

namespace AdministrationApp.Login
{
    public class BindablePasswordBox : UserControl, IPasswordProvider
    {
        private PasswordBox _passwordBox;
        public BindablePasswordBox()
        {
            _passwordBox = new PasswordBox();
            Content = _passwordBox;
        }
        public string GetPassword()
        {
            return _passwordBox.Password;
        }

        public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register("Password", typeof(string), typeof(BindablePasswordBox),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PasswordPropertyChanged));

        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = (BindablePasswordBox)d;
            if (passwordBox._passwordBox.Password != (string)e.NewValue)
            {
                passwordBox._passwordBox.Password = (string)e.NewValue;
            }
        }
    }
}
