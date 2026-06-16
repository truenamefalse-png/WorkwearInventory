using System;
using System.Linq;
using System.Windows;
using WorkwearInventory.Models;
using WorkwearInventory.Services;

namespace WorkwearInventory
{
    public partial class RequestCreateWindow : Window
    {
        public RequestCreateWindow()
        {
            InitializeComponent();
            // Заполняем выпадающий список названиями существующих позиций
            var products = DataService.GetProducts().Select(p => p.Name).Distinct().ToList();
            ProductNameCombo.ItemsSource = products;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string employee = EmployeeNameBox.Text.Trim();
            string productName = ProductNameCombo.Text.Trim();
            string size = SizeBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(employee) || string.IsNullOrWhiteSpace(productName))
            {
                MessageBox.Show("Заполните ФИО и наименование.");
                return;
            }
            if (!int.TryParse(QuantityBox.Text, out int qty) || qty < 1)
            {
                MessageBox.Show("Некорректное количество.");
                return;
            }

            var request = new Request
            {
                EmployeeName = employee,
                ProductName = productName,
                Size = size,
                Quantity = qty,
                RequestDate = DateTime.Now,
                Status = "Новая"
            };

            DataService.CreateRequest(request);
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}