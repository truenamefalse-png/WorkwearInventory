using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WorkwearInventory.Services;

namespace WorkwearInventory.Pages
{
    public partial class UsersPage : Page
    {
        public UsersPage()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            UsersGrid.ItemsSource = null;
            UsersGrid.ItemsSource = DataService.Users;
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string username)
            {
                if (username == "admin")
                {
                    MessageBox.Show("Нельзя удалить администратора!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (MessageBox.Show($"Удалить пользователя {username}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DataService.DeleteUser(username);
                    LoadUsers();
                }
            }
        }
    }
}