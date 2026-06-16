using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WorkwearInventory.Services;

namespace WorkwearInventory.Pages
{
    public partial class RequestsPage : Page
    {
        public RequestsPage()
        {
            InitializeComponent();
            LoadRequests();
        }

        private void LoadRequests()
        {
            RequestsGrid.ItemsSource = null;
            RequestsGrid.ItemsSource = DataService.GetRequests();
        }

        private void NewRequest_Click(object sender, RoutedEventArgs e)
        {
            var window = new RequestCreateWindow();
            if (window.ShowDialog() == true)
                LoadRequests();
        }

        private void CompleteRequest_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int requestId)
            {
                DataService.UpdateRequestStatus(requestId, "Выполнена");
                LoadRequests();
            }
        }
    }
}