using System.Windows;
using System.Windows.Controls;
using WorkwearInventory.Services;

namespace WorkwearInventory.Pages
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var user = DataService.Authenticate(UsernameBox.Text, PasswordBox.Password);
            if (user != null)
            {
                if (Application.Current.MainWindow is MainWindow mw)
                {
                    mw.SetUser(user.Username);
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void GoToRegister_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mw)
            {
                mw.MainFrame.Navigate(new RegisterPage());
            }
        }
    }
}