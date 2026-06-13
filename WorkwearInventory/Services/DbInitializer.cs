using System.Data.Entity;
using WorkwearInventory.Models;

namespace WorkwearInventory.Services
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            // 1. Пользователи (3)
            context.Users.Add(new User { Username = "admin", Password = "admin" });
            context.Users.Add(new User { Username = "sklad1", Password = "123" });
            context.Users.Add(new User { Username = "sklad2", Password = "123" });

            // 2. Категории (5)
            context.Categories.Add(new Category { Name = "Костюмы" });
            context.Categories.Add(new Category { Name = "Обувь" });
            context.Categories.Add(new Category { Name = "Головные уборы" });
            context.Categories.Add(new Category { Name = "Средства защиты" });
            context.Categories.Add(new Category { Name = "Перчатки" });

            context.SaveChanges();

            // 3. Спецодежда (20 записей)
            context.Products.Add(new Product { Name = "Костюм сварщика", Description = "Огнестойкий", CategoryId = 1, Stock = 15, WearPeriodDays = 180, PhotoPath = "" });
            context.Products.Add(new Product { Name = "Костюм электрика", Description = "Х/б, устойчивый к дуге", CategoryId = 1, Stock = 8, WearPeriodDays = 150, PhotoPath = "" });
            context.Products.Add(new Product { Name = "Костюм маляра", Description = "Защита от краски", CategoryId = 1, Stock = 12, WearPeriodDays = 120, PhotoPath = "" });
            context.Products.Add(new Product { Name = "Ботинки кожаные", Description = "Металлический подносок", CategoryId = 2, Stock = 30, WearPeriodDays = 365, PhotoPath = "" });
            context.Products.Add(new Product { Name = "Сапоги резиновые", Description = "Кислотостойкие", CategoryId = 2, Stock = 20, WearPeriodDays = 240, PhotoPath = "" });
            context.Products.Add(new Product { Name = "Кеды рабочие", Description = "Легкие, дышащие", CategoryId = 2, Stock = 18, WearPeriodDays = 180, PhotoPath = "" });
            context.Products.Add(new Product { Name = "Каска строительная", Description = "Оранжевая, регулируемая", CategoryId = 3, Stock = 40, WearPeriodDays = 730, PhotoPath = "" });
            context.Products.Add(new Product { Name = "Шапка зимняя", Description = "Утеплённая", CategoryId = 3, Stock = 25, WearPeriodDays = 270, PhotoPath = "" });
            context.Products.Add(new Product { Name = "Бейсболка", Description = "Логотип компании", CategoryId = 3, Stock = 50, WearPeriodDays = 180, PhotoPath = "" });
            context.Products.Add(new Product { Name = "Респиратор", Description = "Класс FFP2", CategoryId = 4, Stock = 200, WearPeriodDays = 7, PhotoPath = "" });
            context.Products.Add(new Product { Name = "Защитные очки", Description = "Прозрачные", CategoryId = 4, Stock = 100, WearPeriodDays = 90, PhotoPath = "" });
            context.Products.Add(new Product { Name = "Наушники противошумные", Description = "SNR 30 дБ", CategoryId = 4, Stock = 60, WearPeriodDays = 180, PhotoPath = "" });
            context.Products.Add(new Product { Name = "Перчатки х/б", Description = "Универсальные", CategoryId = 5, Stock = 500, WearPeriodDays = 30, PhotoPath = "" });
            context.Products.Add(new Product { Name = "Перчатки брезентовые", Description = "Повышенная прочность", CategoryId = 5, Stock = 300, WearPeriodDays = 45, PhotoPath = "" });
            context.Products.Add(new Product { Name = "Перчатки нитриловые", Description = "Маслобензостойкие", CategoryId = 5, Stock = 250, WearPeriodDays = 20, PhotoPath = "" });
            context.Products.Add(new Product { Name = "Костюм химической защиты", Description = "Одноразовый", CategoryId = 1, Stock = 40, WearPeriodDays = 1, PhotoPath = "" });
            context.Products.Add(new Product { Name = "Подшлемник", Description = "Х/б", CategoryId = 3, Stock = 80, WearPeriodDays = 90, PhotoPath = "" });
            context.Products.Add(new Product { Name = "Жилет сигнальный", Description = "Светоотражающий", CategoryId = 4, Stock = 70, WearPeriodDays = 180, PhotoPath = "" });
            context.Products.Add(new Product { Name = "Рукавицы утепленные", Description = "Зимние", CategoryId = 5, Stock = 120, WearPeriodDays = 120, PhotoPath = "" });
            context.Products.Add(new Product { Name = "Боты диэлектрические", Description = "До 1000В", CategoryId = 2, Stock = 15, WearPeriodDays = 365, PhotoPath = "" });

            // 4. Несколько выдач (3)
            var issue1 = new IssueReceipt { IssueDate = System.DateTime.Now.AddDays(-10), EmployeeName = "Иванов И.И.", DateReturned = null };
            issue1.Items.Add(new IssueItem { ProductName = "Костюм сварщика", Quantity = 1, WearPeriodDays = 180 });
            context.IssueReceipts.Add(issue1);

            var issue2 = new IssueReceipt { IssueDate = System.DateTime.Now.AddDays(-5), EmployeeName = "Петров П.П.", DateReturned = null };
            issue2.Items.Add(new IssueItem { ProductName = "Ботинки кожаные", Quantity = 1, WearPeriodDays = 365 });
            context.IssueReceipts.Add(issue2);

            var issue3 = new IssueReceipt { IssueDate = System.DateTime.Now.AddDays(-20), EmployeeName = "Сидоров С.С.", DateReturned = System.DateTime.Now.AddDays(-2) };
            issue3.Items.Add(new IssueItem { ProductName = "Перчатки х/б", Quantity = 3, WearPeriodDays = 30 });
            context.IssueReceipts.Add(issue3);

            base.Seed(context);
        }
    }
}