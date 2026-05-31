using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using WorkwearInventory.Services;

namespace WorkwearInventory.Pages
{
    public partial class ReportsPage : Page
    {
        public ReportsPage()
        {
            InitializeComponent();
            SalesGrid.ItemsSource = DataService.Sales;
        }

        private void ExportCsv_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = "Отчёт_продажи.csv"
            };
            if (dlg.ShowDialog() == true)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Id;Дата;Товар;Количество;Цена;Сумма");
                foreach (var sale in DataService.Sales)
                {
                    foreach (var item in sale.Items)
                    {
                        sb.AppendLine($"{sale.Id};{sale.SaleDate:dd.MM.yyyy HH:mm};{item.ProductName};{item.Quantity};{item.UnitPrice};{item.LineTotal}");
                    }
                }
                File.WriteAllText(dlg.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Отчёт сохранён.", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}