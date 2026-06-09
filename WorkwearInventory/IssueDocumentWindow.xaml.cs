using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WorkwearInventory.Models;

namespace WorkwearInventory
{
    public partial class IssueDocumentWindow : Window
    {
        private IssueReceipt receipt;

        public IssueDocumentWindow(IssueReceipt receipt)
        {
            InitializeComponent();
            this.receipt = receipt;
            EmployeeText.Text = $"Сотрудник: {receipt.EmployeeName}";
            DateText.Text = $"Дата: {receipt.IssueDate:dd.MM.yyyy HH:mm}";
            ItemsList.ItemsSource = receipt.Items;
            TotalText.Text = receipt.TotalAmount.ToString("N2") + " руб.";
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(this, "Наряд на выдачу");
            }
        }

        private void SaveToFile_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog
            {
                Filter = "Текстовый файл (*.txt)|*.txt",
                FileName = $"Наряд_{receipt.Id}_{receipt.EmployeeName}.txt"
            };
            if (dlg.ShowDialog() == true)
            {
                var sb = new StringBuilder();
                sb.AppendLine("НАРЯД НА ВЫДАЧУ СПЕЦОДЕЖДЫ");
                sb.AppendLine($"Сотрудник: {receipt.EmployeeName}");
                sb.AppendLine($"Дата: {receipt.IssueDate:dd.MM.yyyy HH:mm}");
                sb.AppendLine("--------------------------------");
                foreach (var item in receipt.Items)
                {
                    sb.AppendLine($"{item.ProductName}  x{item.Quantity}  {item.UnitPrice:N2} = {item.LineTotal:N2}");
                }
                sb.AppendLine("--------------------------------");
                sb.AppendLine($"Общая стоимость: {receipt.TotalAmount:N2} руб.");
                File.WriteAllText(dlg.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Наряд сохранён.", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}