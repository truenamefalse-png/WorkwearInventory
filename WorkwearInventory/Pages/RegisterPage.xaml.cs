using System.Windows;
using System.Windows.Controls;
using WorkwearInventory.Services;

namespace WorkwearInventory.Pages
{
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string user = RegUsernameBox.Text.Trim();
            string pass = RegPasswordBox.Password;
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            if (DataService.RegisterUser(user, pass))
            {
                MessageBox.Show("Регистрация успешна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                // Вернуться на вход
                if (Application.Current.MainWindow is MainWindow mw)
                    mw.MainFrame.Navigate(new LoginPage());
            }
            else
            {
                MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mw)
                mw.MainFrame.Navigate(new LoginPage());
        }
    }
}