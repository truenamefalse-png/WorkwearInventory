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

            // 3. Спецодежда (20 позиций с размерами)
            context.Products.Add(new Product { Name = "Костюм сварщика", Description = "Огнестойкий", CategoryId = 1, Stock = 15, WearPeriodDays = 180, Size = "52-54" });
            context.Products.Add(new Product { Name = "Костюм электрика", Description = "Х/б, устойчивый к дуге", CategoryId = 1, Stock = 8, WearPeriodDays = 150, Size = "48-50" });
            context.Products.Add(new Product { Name = "Костюм маляра", Description = "Защита от краски", CategoryId = 1, Stock = 12, WearPeriodDays = 120, Size = "50-52" });
            context.Products.Add(new Product { Name = "Ботинки кожаные", Description = "Металлический подносок", CategoryId = 2, Stock = 30, WearPeriodDays = 365, Size = "42" });
            context.Products.Add(new Product { Name = "Сапоги резиновые", Description = "Кислотостойкие", CategoryId = 2, Stock = 20, WearPeriodDays = 240, Size = "43" });
            context.Products.Add(new Product { Name = "Кеды рабочие", Description = "Лёгкие, дышащие", CategoryId = 2, Stock = 18, WearPeriodDays = 180, Size = "41" });
            context.Products.Add(new Product { Name = "Каска строительная", Description = "Оранжевая, регулируемая", CategoryId = 3, Stock = 40, WearPeriodDays = 730, Size = "Универсальная" });
            context.Products.Add(new Product { Name = "Шапка зимняя", Description = "Утеплённая", CategoryId = 3, Stock = 25, WearPeriodDays = 270, Size = "56-58" });
            context.Products.Add(new Product { Name = "Бейсболка", Description = "Логотип компании", CategoryId = 3, Stock = 50, WearPeriodDays = 180, Size = "Универсальная" });
            context.Products.Add(new Product { Name = "Респиратор", Description = "Класс FFP2", CategoryId = 4, Stock = 200, WearPeriodDays = 7, Size = "Универсальный" });
            context.Products.Add(new Product { Name = "Защитные очки", Description = "Прозрачные", CategoryId = 4, Stock = 100, WearPeriodDays = 90, Size = "Универсальные" });
            context.Products.Add(new Product { Name = "Наушники противошумные", Description = "SNR 30 дБ", CategoryId = 4, Stock = 60, WearPeriodDays = 180, Size = "Универсальные" });
            context.Products.Add(new Product { Name = "Перчатки х/б", Description = "Универсальные", CategoryId = 5, Stock = 500, WearPeriodDays = 30, Size = "10" });
            context.Products.Add(new Product { Name = "Перчатки брезентовые", Description = "Повышенная прочность", CategoryId = 5, Stock = 300, WearPeriodDays = 45, Size = "10" });
            context.Products.Add(new Product { Name = "Перчатки нитриловые", Description = "Маслобензостойкие", CategoryId = 5, Stock = 250, WearPeriodDays = 20, Size = "9" });
            context.Products.Add(new Product { Name = "Костюм химической защиты", Description = "Одноразовый", CategoryId = 1, Stock = 40, WearPeriodDays = 1, Size = "52-54" });
            context.Products.Add(new Product { Name = "Подшлемник", Description = "Х/б", CategoryId = 3, Stock = 80, WearPeriodDays = 90, Size = "Универсальный" });
            context.Products.Add(new Product { Name = "Жилет сигнальный", Description = "Светоотражающий", CategoryId = 4, Stock = 70, WearPeriodDays = 180, Size = "48-50" });
            context.Products.Add(new Product { Name = "Рукавицы утеплённые", Description = "Зимние", CategoryId = 5, Stock = 120, WearPeriodDays = 120, Size = "10" });
            context.Products.Add(new Product { Name = "Боты диэлектрические", Description = "До 1000 В", CategoryId = 2, Stock = 15, WearPeriodDays = 365, Size = "43" });

            // 4. Пара заявок для примера
            context.Requests.Add(new Request { EmployeeName = "Иванов И.И.", ProductName = "Костюм сварщика", Size = "52-54", Quantity = 1, RequestDate = System.DateTime.Now.AddDays(-3), Status = "Новая" });
            context.Requests.Add(new Request { EmployeeName = "Петров П.П.", ProductName = "Ботинки кожаные", Size = "42", Quantity = 2, RequestDate = System.DateTime.Now.AddDays(-1), Status = "Выполнена" });

            base.Seed(context);
        }
    }
}