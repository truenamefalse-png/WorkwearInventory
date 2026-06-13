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
            LoadData();
        }

        private void LoadData()
        {
            SalesGrid.ItemsSource = null;
            SalesGrid.ItemsSource = DataService.GetIssueReceipts();
        }

        private void ExportCsv_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog { Filter = "CSV|*.csv", FileName = "Журнал_выдачи.csv" };
            if (dlg.ShowDialog() == true)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Id;Дата;Сотрудник;Позиция;Кол-во;Срок носки;Возврат");
                foreach (var r in DataService.GetIssueReceipts())
                    foreach (var i in r.Items)
                        sb.AppendLine($"{r.Id};{r.IssueDate:dd.MM.yyyy};{r.EmployeeName};{i.ProductName};{i.Quantity};{i.WearPeriodDays};{(r.DateReturned.HasValue ? r.DateReturned.Value.ToString("dd.MM.yyyy") : "Не возвращено")}");
                File.WriteAllText(dlg.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Экспортировано.");
            }
        }

        private void ReturnIssue_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int receiptId)
            {
                if (MessageBox.Show("Возврат спецодежды? Количество будет восстановлено на складе.", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (DataService.ReturnProduct(receiptId))
                    {
                        MessageBox.Show("Возврат оформлен.", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData();
                    }
                    else MessageBox.Show("Ошибка возврата.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}