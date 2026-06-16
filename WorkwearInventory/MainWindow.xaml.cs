using System.Windows;
using System.Windows.Controls;
using WorkwearInventory.Pages;

namespace WorkwearInventory
{
    public partial class MainWindow : Window
    {
        public static string LoggedInUser { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new LoginPage());
        }

        public void SetUser(string username)
        {
            LoggedInUser = username;
            CurrentUserText.Text = username;
            UsersButton.Visibility = (username == "admin") ? Visibility.Visible : Visibility.Collapsed;
            MainFrame.Navigate(new MainPage());
        }

        private void NavigateToProducts(object sender, RoutedEventArgs e) => MainFrame.Navigate(new ProductsPage());
        private void NavigateToCategories(object sender, RoutedEventArgs e) => MainFrame.Navigate(new CategoriesPage());
        private void NavigateToReports(object sender, RoutedEventArgs e) => MainFrame.Navigate(new ReportsPage());
        private void NavigateToRequests(object sender, RoutedEventArgs e) => MainFrame.Navigate(new RequestsPage());
        private void NavigateToUsers(object sender, RoutedEventArgs e) => MainFrame.Navigate(new UsersPage());
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoggedInUser = null;
            CurrentUserText.Text = "";
            MainFrame.Navigate(new LoginPage());
        }
    }
}