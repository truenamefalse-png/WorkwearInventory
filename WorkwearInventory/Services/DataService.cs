using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WorkwearInventory.Models;

namespace WorkwearInventory.Services
{
    public static class DataService
    {
        private static AppDbContext CreateContext() => new AppDbContext();

        // ----- Пользователи -----
        public static User Authenticate(string username, string password)
        {
            using (var db = CreateContext())
                return db.Users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.Password == password);
        }

        public static bool RegisterUser(string username, string password)
        {
            using (var db = CreateContext())
            {
                if (db.Users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
                    return false;
                db.Users.Add(new User { Username = username, Password = password });
                db.SaveChanges();
                return true;
            }
        }

        public static void DeleteUser(string username)
        {
            using (var db = CreateContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username);
                if (user != null) { db.Users.Remove(user); db.SaveChanges(); }
            }
        }

        public static List<User> GetUsers()
        {
            using (var db = CreateContext()) return db.Users.ToList();
        }

        // ----- Категории -----
        public static List<Category> GetCategories()
        {
            using (var db = CreateContext()) return db.Categories.ToList();
        }

        public static void AddCategory(string name)
        {
            using (var db = CreateContext()) { db.Categories.Add(new Category { Name = name }); db.SaveChanges(); }
        }

        public static void UpdateCategory(int id, string newName)
        {
            using (var db = CreateContext())
            {
                var cat = db.Categories.Find(id);
                if (cat != null) { cat.Name = newName; db.SaveChanges(); }
            }
        }

        public static void DeleteCategory(int id)
        {
            using (var db = CreateContext())
            {
                var cat = db.Categories.Find(id);
                if (cat != null) { db.Categories.Remove(cat); db.SaveChanges(); }
            }
        }

        // ----- Спецодежда -----
        public static List<Product> GetProducts()
        {
            using (var db = CreateContext()) return db.Products.Include("Category").ToList();
        }

        public static void AddProduct(Product product)
        {
            using (var db = CreateContext()) { db.Products.Add(product); db.SaveChanges(); }
        }

        public static void UpdateProduct(Product product)
        {
            using (var db = CreateContext())
            {
                var existing = db.Products.Find(product.Id);
                if (existing != null)
                {
                    existing.Name = product.Name;
                    existing.Description = product.Description;
                    existing.CategoryId = product.CategoryId;
                    existing.Stock = product.Stock;
                    existing.WearPeriodDays = product.WearPeriodDays;
                    existing.Size = product.Size;
                    existing.PhotoPath = product.PhotoPath;
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteProduct(int id)
        {
            using (var db = CreateContext())
            {
                var prod = db.Products.Find(id);
                if (prod != null) { db.Products.Remove(prod); db.SaveChanges(); }
            }
        }

        // ----- Выдача -----
        public static IssueReceipt IssueProduct(int productId, string employeeName, int quantity, int wearDays, string size)
        {
            using (var db = CreateContext())
            {
                var product = db.Products.Find(productId);
                if (product == null || product.Stock < quantity)
                    return null;

                product.Stock -= quantity;

                var receipt = new IssueReceipt
                {
                    IssueDate = DateTime.Now,
                    EmployeeName = employeeName,
                    Items = new List<IssueItem>
            {
                new IssueItem
                {
                    ProductName = product.Name,
                    Quantity = quantity,
                    WearPeriodDays = wearDays,
                    Size = size                           // сохраняем переданный размер
                }
            }
                };

                db.IssueReceipts.Add(receipt);
                db.SaveChanges();
                return receipt;
            }
        }

        public static bool ReturnProduct(int issueReceiptId)
        {
            using (var db = CreateContext())
            {
                var receipt = db.IssueReceipts.Include("Items").FirstOrDefault(r => r.Id == issueReceiptId);
                if (receipt == null || receipt.DateReturned != null) return false;

                foreach (var item in receipt.Items)
                {
                    var product = db.Products.FirstOrDefault(p => p.Name == item.ProductName);
                    if (product != null) product.Stock += item.Quantity;
                }
                receipt.DateReturned = DateTime.Now;
                db.SaveChanges();
                return true;
            }
        }

        public static List<IssueReceipt> GetIssueReceipts()
        {
            using (var db = CreateContext()) return db.IssueReceipts.Include("Items").ToList();
        }

        // ----- Заявки -----
        public static void CreateRequest(Request request)
        {
            using (var db = CreateContext())
            {
                db.Requests.Add(request);
                db.SaveChanges();
            }
        }

        public static List<Request> GetRequests()
        {
            using (var db = CreateContext()) return db.Requests.ToList();
        }

        public static void UpdateRequestStatus(int id, string status)
        {
            using (var db = CreateContext())
            {
                var req = db.Requests.Find(id);
                if (req != null) { req.Status = status; db.SaveChanges(); }
            }
        }

        // ----- Фото -----
        public static string CopyImageToAppFolder(string sourcePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sourcePath) || !File.Exists(sourcePath)) return "";
                string imagesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProductImages");
                Directory.CreateDirectory(imagesDir);
                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(sourcePath)}";
                File.Copy(sourcePath, Path.Combine(imagesDir, fileName), true);
                return fileName;
            }
            catch { return ""; }
        }
    }
}