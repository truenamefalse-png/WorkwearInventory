using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
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
            DateText.Text = $"Дата выдачи: {receipt.IssueDate:dd.MM.yyyy HH:mm}";
            var item = receipt.Items.FirstOrDefault();
            PeriodText.Text = $"Срок носки: {item?.WearPeriodDays ?? 0} дн.";
            DateTime dueDate = receipt.IssueDate.AddDays(item?.WearPeriodDays ?? 0);
            DueDateText.Text = $"Окончание срока: {dueDate:dd.MM.yyyy}";
            DueDateText2.Text = dueDate.ToString("dd.MM.yyyy");
            ItemsList.ItemsSource = receipt.Items;
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true) pd.PrintVisual(this, "Наряд на выдачу");
        }

        private void SaveToFile_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog { Filter = "Текстовый файл|*.txt", FileName = $"Наряд_{receipt.Id}.txt" };
            if (dlg.ShowDialog() == true)
            {
                var sb = new StringBuilder();
                sb.AppendLine("НАРЯД НА ВЫДАЧУ");
                sb.AppendLine($"Сотрудник: {receipt.EmployeeName}");
                sb.AppendLine($"Дата выдачи: {receipt.IssueDate:dd.MM.yyyy}");
                var item = receipt.Items.FirstOrDefault();
                sb.AppendLine($"Срок носки: {item?.WearPeriodDays} дн.");
                sb.AppendLine($"Окончание срока: {receipt.IssueDate.AddDays(item?.WearPeriodDays ?? 0):dd.MM.yyyy}");
                sb.AppendLine("--------------------------------");
                foreach (var i in receipt.Items)
                {
                    sb.AppendLine($"{i.ProductName}, Размер: {i.Size}, Кол-во: {i.Quantity}");
                }
                File.WriteAllText(dlg.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Наряд сохранён.");
            }
        }
    }
}