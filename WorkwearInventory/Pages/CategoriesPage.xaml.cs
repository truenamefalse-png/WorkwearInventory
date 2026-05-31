using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using WorkwearInventory.Services;

namespace WorkwearInventory.Pages
{
    public partial class CategoriesPage : Page
    {
        public CategoriesPage()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void LoadCategories()
        {
            CategoryList.ItemsSource = null;
            CategoryList.ItemsSource = DataService.GetCategories();
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            string name = NewCategoryBox.Text.Trim();
            if (!string.IsNullOrEmpty(name))
            {
                DataService.AddCategory(name);
                NewCategoryBox.Clear();
                LoadCategories();
            }
        }

        private void EditCategory_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int catId)
            {
                var cat = DataService.GetCategories().FirstOrDefault(c => c.Id == catId);
                if (cat == null) return;
                string newName = Interaction.InputBox("Новое название:", "Редактирование категории", cat.Name);
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    DataService.UpdateCategory(catId, newName);
                    LoadCategories();
                }
            }
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int catId)
            {
                if (MessageBox.Show("Удалить категорию?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DataService.DeleteCategory(catId);
                    LoadCategories();
                }
            }
        }
    }
}