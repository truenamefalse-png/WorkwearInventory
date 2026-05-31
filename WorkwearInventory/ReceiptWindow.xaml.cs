using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WorkwearInventory.Models;

namespace WorkwearInventory
{
    public partial class ReceiptWindow : Window
    {
        private SaleReceipt currentReceipt;

        public ReceiptWindow(SaleReceipt receipt)
        {
            InitializeComponent();
            currentReceipt = receipt;
            ReceiptList.ItemsSource = receipt.Items;
            TotalText.Text = receipt.TotalAmount.ToString("N2") + " руб.";
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(this, "Чек продажи");
            }
        }

        private void SaveToFile_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog
            {
                Filter = "Текстовый файл (*.txt)|*.txt",
                FileName = $"Чек_{currentReceipt.Id}.txt"
            };
            if (dlg.ShowDialog() == true)
            {
                var sb = new StringBuilder();
                sb.AppendLine("ЧЕК ПРОДАЖИ");
                sb.AppendLine($"Дата: {currentReceipt.SaleDate:dd.MM.yyyy HH:mm}");
                sb.AppendLine("------------------------------");
                foreach (var item in currentReceipt.Items)
                {
                    sb.AppendLine($"{item.ProductName}  x{item.Quantity}  {item.UnitPrice:N2} = {item.LineTotal:N2}");
                }
                sb.AppendLine("------------------------------");
                sb.AppendLine($"ИТОГО: {currentReceipt.TotalAmount:N2} руб.");
                File.WriteAllText(dlg.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Чек сохранён.", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}