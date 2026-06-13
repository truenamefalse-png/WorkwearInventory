using System.Linq;
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

            // Подгружаем срок носки из базы и ставим в поле по умолчанию
            var product = DataService.GetProducts().FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                WearPeriodBox.Text = product.WearPeriodDays.ToString();
            }
            else
            {
                WearPeriodBox.Text = "30"; // запасное значение
            }
        }

        private void IssueButton_Click(object sender, RoutedEventArgs e)
        {
            string employee = EmployeeNameBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(employee))
            {
                MessageBox.Show("Введите ФИО сотрудника.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(QuantityBox.Text, out int qty) || qty < 1)
            {
                MessageBox.Show("Введите корректное количество (целое число больше 0).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(WearPeriodBox.Text, out int wearDays) || wearDays < 1)
            {
                MessageBox.Show("Введите корректный срок носки (целое число дней).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Вызов метода выдачи с новым параметром (обновлённый DataService ниже)
            var receipt = DataService.IssueProduct(productId, employee, qty, wearDays);
            if (receipt == null)
            {
                MessageBox.Show("Недостаточно на складе.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

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