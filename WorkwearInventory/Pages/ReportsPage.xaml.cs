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
            SalesGrid.ItemsSource = DataService.GetIssueReceipts();  // загружаем выдачи
        }

        private void ExportCsv_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = "Журнал_выдачи.csv"
            };
            if (dlg.ShowDialog() == true)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Id;Дата;Сотрудник;Товар;Количество;Цена;Сумма");
                foreach (var receipt in DataService.GetIssueReceipts())
                {
                    foreach (var item in receipt.Items)
                    {
                        sb.AppendLine($"{receipt.Id};{receipt.IssueDate:dd.MM.yyyy HH:mm};{receipt.EmployeeName};{item.ProductName};{item.Quantity};{item.UnitPrice};{item.LineTotal}");
                    }
                }
                File.WriteAllText(dlg.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Журнал выдачи сохранён.", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}