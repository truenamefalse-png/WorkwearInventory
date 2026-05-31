using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WorkwearInventory.Models;
using WorkwearInventory.Services;

namespace WorkwearInventory.Pages
{
    public partial class ProductsPage : Page, INotifyPropertyChanged
    {
        public ObservableCollection<ProductViewModel> FilteredProducts { get; set; } = new ObservableCollection<ProductViewModel>();

        public ProductsPage()
        {
            InitializeComponent();
            LoadData();
            CategoryFilter.ItemsSource = DataService.GetCategories();
            CategoryFilter.DisplayMemberPath = "Name";
            CategoryFilter.SelectedValuePath = "Id";
            CategoryFilter.SelectedIndex = -1;
            ProductsGrid.ItemsSource = FilteredProducts;
        }

        private void LoadData()
        {
            FilteredProducts.Clear();
            var products = DataService.GetProducts(); // уже включает Category
            foreach (var p in products)
            {
                FilteredProducts.Add(new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category?.Name ?? "",
                    Stock = p.Stock,
                    Price = p.Price,
                    PhotoPath = p.PhotoPath
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private void FilterChanged(object sender, TextChangedEventArgs e) => ApplyFilter();
        private void FilterChanged(object sender, SelectionChangedEventArgs e) => ApplyFilter();

        private void ApplyFilter()
        {
            string search = SearchBox.Text?.ToLower() ?? "";
            int? categoryId = (CategoryFilter.SelectedValue as int?);

            var allProducts = DataService.GetProducts();
            var filtered = allProducts.Where(p =>
                (string.IsNullOrEmpty(search) || p.Name.ToLower().Contains(search)) &&
                (categoryId == null || p.CategoryId == categoryId)
            ).Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? "",
                Stock = p.Stock,
                Price = p.Price,
                PhotoPath = p.PhotoPath
            }).ToList();

            FilteredProducts.Clear();
            foreach (var vm in filtered)
                FilteredProducts.Add(vm);
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var window = new ProductEditWindow();
            if (window.ShowDialog() == true)
            {
                var newProduct = window.ProductResult;
                if (!string.IsNullOrEmpty(window.PhotoSourcePath))
                {
                    string fileName = DataService.CopyImageToAppFolder(window.PhotoSourcePath);
                    newProduct.PhotoPath = fileName;
                }
                DataService.AddProduct(newProduct);
                ApplyFilter();
            }
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int productId)
            {
                var product = DataService.GetProducts().FirstOrDefault(p => p.Id == productId);
                if (product == null) return;
                var window = new ProductEditWindow(product);
                if (window.ShowDialog() == true)
                {
                    var updated = window.ProductResult;
                    if (!string.IsNullOrEmpty(window.PhotoSourcePath))
                        updated.PhotoPath = DataService.CopyImageToAppFolder(window.PhotoSourcePath);
                    DataService.UpdateProduct(updated);
                    ApplyFilter();
                }
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int productId)
            {
                if (MessageBox.Show("Удалить товар?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DataService.DeleteProduct(productId);
                    ApplyFilter();
                }
            }
        }

        private void SellProduct_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int productId)
            {
                var receipt = DataService.SellProduct(productId);
                if (receipt != null)
                {
                    var receiptWindow = new ReceiptWindow(receipt);
                    receiptWindow.ShowDialog();
                    ApplyFilter();
                }
                else
                {
                    MessageBox.Show("Недостаточно товара на складе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }

    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string PhotoPath { get; set; }
    }
}