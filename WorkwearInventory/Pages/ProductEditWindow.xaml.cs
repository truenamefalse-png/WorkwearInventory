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
            CategoryCombo.ItemsSource = DataService.Categories;
            if (product != null)
            {
                editingProduct = product;
                NameBox.Text = product.Name;
                DescriptionBox.Text = product.Description;
                CategoryCombo.SelectedValue = product.CategoryId;
                PriceBox.Text = product.Price.ToString();
                StockBox.Text = product.Stock.ToString();
                if (!string.IsNullOrEmpty(product.PhotoPath))
                {
                    PhotoNameText.Text = product.PhotoPath;
                }
                Title = "Редактирование товара";
            }
        }

        private void ChoosePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp";
            if (dlg.ShowDialog() == true)
            {
                PhotoSourcePath = dlg.FileName;
                PhotoNameText.Text = System.IO.Path.GetFileName(PhotoSourcePath);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text) || CategoryCombo.SelectedValue == null)
            {
                MessageBox.Show("Заполните название и категорию");
                return;
            }
            if (!decimal.TryParse(PriceBox.Text, out decimal price))
            {
                MessageBox.Show("Некорректная цена");
                return;
            }
            if (!int.TryParse(StockBox.Text, out int stock))
            {
                MessageBox.Show("Некорректное количество");
                return;
            }

            ProductResult = new Product
            {
                Id = editingProduct?.Id ?? 0,
                Name = NameBox.Text,
                Description = DescriptionBox.Text,
                CategoryId = (int)CategoryCombo.SelectedValue,
                Price = price,
                Stock = stock
            };

            DialogResult = true;
            Close();
        }
    }
}