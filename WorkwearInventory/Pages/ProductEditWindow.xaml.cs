using System.Windows;
using Microsoft.Win32;
using WorkwearInventory.Models;
using WorkwearInventory.Services;

namespace WorkwearInventory
{
    public partial class ProductEditWindow : Window
    {
        public Product ProductResult { get; private set; }
        public string PhotoSourcePath { get; private set; }
        private Product editingProduct;

        public ProductEditWindow(Product product = null)
        {
            InitializeComponent();
            CategoryCombo.ItemsSource = DataService.GetCategories();
            if (product != null)
            {
                editingProduct = product;
                NameBox.Text = product.Name;
                DescriptionBox.Text = product.Description;
                CategoryCombo.SelectedValue = product.CategoryId;
                SizeBox.Text = product.Size;
                WearPeriodBox.Text = product.WearPeriodDays.ToString();
                StockBox.Text = product.Stock.ToString();
                if (!string.IsNullOrEmpty(product.PhotoPath)) PhotoNameText.Text = product.PhotoPath;
                Title = "Редактирование позиции";
            }
        }

        private void ChoosePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp" };
            if (dlg.ShowDialog() == true)
            {
                PhotoSourcePath = dlg.FileName;
                PhotoNameText.Text = System.IO.Path.GetFileName(PhotoSourcePath);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text) || CategoryCombo.SelectedValue == null)
            { MessageBox.Show("Заполните наименование и категорию"); return; }
            if (!int.TryParse(WearPeriodBox.Text, out int wear))
            { MessageBox.Show("Некорректный срок носки"); return; }
            if (!int.TryParse(StockBox.Text, out int stock))
            { MessageBox.Show("Некорректное количество"); return; }

            ProductResult = new Product
            {
                Id = editingProduct?.Id ?? 0,
                Name = NameBox.Text,
                Description = DescriptionBox.Text,
                CategoryId = (int)CategoryCombo.SelectedValue,
                Size = SizeBox.Text,
                WearPeriodDays = wear,
                Stock = stock
            };
            DialogResult = true;
            Close();
        }
    }
}