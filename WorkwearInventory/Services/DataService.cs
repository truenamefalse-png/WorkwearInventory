using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WorkwearInventory.Models;

namespace WorkwearInventory.Services
{
    public static class DataService
    {
        public static List<User> Users { get; } = new List<User>();
        public static List<Category> Categories { get; } = new List<Category>();
        public static List<Product> Products { get; } = new List<Product>();
        public static List<SaleReceipt> Sales { get; } = new List<SaleReceipt>();

        private static int nextCategoryId = 1;
        private static int nextProductId = 1;
        private static int nextSaleId = 1;

        static DataService()
        {
            Users.Add(new User { Username = "admin", Password = "admin" });
            Categories.Add(new Category { Id = nextCategoryId++, Name = "Костюмы" });
            Categories.Add(new Category { Id = nextCategoryId++, Name = "Обувь" });
            Categories.Add(new Category { Id = nextCategoryId++, Name = "Головные уборы" });

            Products.Add(new Product
            {
                Id = nextProductId++,
                Name = "Костюм сварщика",
                Description = "Огнестойкий костюм для сварочных работ",
                CategoryId = 1,
                Stock = 10,
                Price = 3500m,
                PhotoPath = ""
            });
            Products.Add(new Product
            {
                Id = nextProductId++,
                Name = "Ботинки кожаные",
                Description = "Защитные ботинки с металлическим подноском",
                CategoryId = 2,
                Stock = 25,
                Price = 2200m,
                PhotoPath = ""
            });
        }

        public static User Authenticate(string username, string password)
        {
            return Users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);
        }

        public static bool RegisterUser(string username, string password)
        {
            if (Users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
                return false;
            Users.Add(new User { Username = username, Password = password });
            return true;
        }

        public static void DeleteUser(string username)
        {
            Users.RemoveAll(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public static void AddCategory(string name)
        {
            Categories.Add(new Category { Id = nextCategoryId++, Name = name });
        }

        public static void UpdateCategory(int id, string newName)
        {
            var cat = Categories.FirstOrDefault(c => c.Id == id);
            if (cat != null) cat.Name = newName;
        }

        public static void DeleteCategory(int id)
        {
            Categories.RemoveAll(c => c.Id == id);
        }

        public static void AddProduct(Product product)
        {
            product.Id = nextProductId++;
            Products.Add(product);
        }

        public static void UpdateProduct(Product product)
        {
            var existing = Products.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Description = product.Description;
                existing.CategoryId = product.CategoryId;
                existing.Stock = product.Stock;
                existing.Price = product.Price;
                existing.PhotoPath = product.PhotoPath;
            }
        }

        public static void DeleteProduct(int id)
        {
            Products.RemoveAll(p => p.Id == id);
        }

        public static SaleReceipt SellProduct(int productId, int quantity = 1)
        {
            var product = Products.FirstOrDefault(p => p.Id == productId);
            if (product == null || product.Stock < quantity)
                return null;

            product.Stock -= quantity;

            var receipt = new SaleReceipt
            {
                Id = nextSaleId++,
                SaleDate = DateTime.Now,
                Items = new List<SaleItem>
                {
                    new SaleItem
                    {
                        ProductName = product.Name,
                        Quantity = quantity,
                        UnitPrice = product.Price
                    }
                },
                TotalAmount = product.Price * quantity
            };
            Sales.Add(receipt);
            return receipt;
        }

        public static string CopyImageToAppFolder(string sourcePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sourcePath) || !File.Exists(sourcePath))
                    return string.Empty;

                string imagesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProductImages");
                Directory.CreateDirectory(imagesDir);

                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(sourcePath)}";
                string destPath = Path.Combine(imagesDir, fileName);
                File.Copy(sourcePath, destPath, true);
                return fileName;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}