using System.Windows;
using WorkwearInventory.Services;

namespace WorkwearInventory
{
    public partial class IssueWindow : Window
    {
        private int productId;

        public IssueWindow(int productId)
        {
            InitializeComponent();
            this.productId = productId;
        }

        private void IssueButton_Click(object sender, RoutedEventArgs e)
        {
            string employee = EmployeeNameBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(employee))
            {
                MessageBox.Show("Введите фамилию сотрудника.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(QuantityBox.Text, out int qty) || qty < 1)
            {
                MessageBox.Show("Введите корректное количество (целое число больше 0).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var receipt = DataService.IssueProduct(productId, employee, qty);
            if (receipt == null)
            {
                MessageBox.Show("Недостаточно товара на складе.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Показать сформированный документ выдачи
            var docWindow = new IssueDocumentWindow(receipt);
            docWindow.ShowDialog();

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}