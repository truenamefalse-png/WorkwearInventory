using System;
using System.Data.Entity;
using WorkwearInventory.Models;

namespace WorkwearInventory.Services
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            // ========== ПОЛЬЗОВАТЕЛИ (12 записей) ==========
            context.Users.Add(new User { Username = "admin", Password = "admin" });
            context.Users.Add(new User { Username = "sklad1", Password = "123" });
            context.Users.Add(new User { Username = "sklad2", Password = "123" });
            context.Users.Add(new User { Username = "ivanov", Password = "111" });
            context.Users.Add(new User { Username = "petrov", Password = "222" });
            context.Users.Add(new User { Username = "sidorov", Password = "333" });
            context.Users.Add(new User { Username = "smirnov", Password = "444" });
            context.Users.Add(new User { Username = "kuznecov", Password = "555" });
            context.Users.Add(new User { Username = "popov", Password = "666" });
            context.Users.Add(new User { Username = "vasilev", Password = "777" });
            context.Users.Add(new User { Username = "mihaylov", Password = "888" });
            context.Users.Add(new User { Username = "sokolov", Password = "999" });

            // ========== КАТЕГОРИИ (5) ==========
            context.Categories.Add(new Category { Name = "Костюмы" });
            context.Categories.Add(new Category { Name = "Обувь" });
            context.Categories.Add(new Category { Name = "Головные уборы" });
            context.Categories.Add(new Category { Name = "Средства защиты" });
            context.Categories.Add(new Category { Name = "Перчатки" });

            context.SaveChanges();

            // ========== ТОВАРЫ (20 позиций) ==========
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

            // ========== ЗАЯВКИ (все выполнены, 25 штук) ==========
            string[] employees = { "Иванов И.И.", "Петров П.П.", "Сидоров С.С.", "Смирнов С.М.", "Кузнецов К.К.",
                                   "Попов П.А.", "Васильев В.В.", "Михайлов М.М.", "Соколов С.А.", "Лебедев Л.Л." };
            string[] products = { "Костюм сварщика", "Ботинки кожаные", "Каска строительная", "Перчатки х/б", "Респиратор",
                                  "Защитные очки", "Шапка зимняя", "Перчатки брезентовые", "Сапоги резиновые", "Бейсболка" };
            string[] sizes = { "52-54", "42", "Универсальная", "10", "Универсальный", "Универсальные", "56-58", "10", "43", "Универсальная" };

            for (int i = 0; i < 25; i++)
            {
                int empIdx = i % employees.Length;
                int prodIdx = i % products.Length;
                context.Requests.Add(new Request
                {
                    EmployeeName = employees[empIdx],
                    ProductName = products[prodIdx],
                    Size = sizes[prodIdx],
                    Quantity = (i % 5) + 1,
                    RequestDate = DateTime.Now.AddDays(-i * 2),
                    Status = "Выполнена"
                });
            }

            // ========== ЖУРНАЛ ВЫДАЧИ (много записей) ==========
            // Создадим 40 записей о выдаче с разными статусами возврата
            string[] empForIssue = { "Иванов И.И.", "Петров П.П.", "Сидоров С.С.", "Смирнов С.М.", "Кузнецов К.К.",
                                     "Попов П.А.", "Васильев В.В.", "Михайлов М.М.", "Соколов С.А.", "Лебедев Л.Л.",
                                     "Фёдоров Ф.Ф.", "Николаев Н.Н.", "Алексеев А.А.", "Степанов С.С.", "Тимофеев Т.Т." };
            string[] prodForIssue = { "Костюм сварщика", "Ботинки кожаные", "Каска строительная", "Перчатки х/б",
                                      "Респиратор", "Защитные очки", "Шапка зимняя", "Перчатки брезентовые",
                                      "Сапоги резиновые", "Бейсболка", "Наушники противошумные", "Подшлемник",
                                      "Жилет сигнальный", "Рукавицы утеплённые", "Боты диэлектрические" };
            string[] szForIssue = { "52-54", "42", "Универсальная", "10", "Универсальный", "Универсальные", "56-58",
                                    "10", "43", "Универсальная", "Универсальные", "Универсальный", "48-50", "10", "43" };
            int[] wearDays = { 180, 365, 730, 30, 7, 90, 270, 45, 240, 180, 180, 90, 180, 120, 365 };

            Random rnd = new Random(42); // для воспроизводимости
            for (int i = 0; i < 40; i++)
            {
                int empIdx = i % empForIssue.Length;
                int prodIdx = i % prodForIssue.Length;
                var issue = new IssueReceipt
                {
                    IssueDate = DateTime.Now.AddDays(-rnd.Next(1, 180)),
                    EmployeeName = empForIssue[empIdx]
                };

                // Половина выдач возвращена
                if (i % 2 == 0)
                {
                    issue.DateReturned = issue.IssueDate.AddDays(rnd.Next(10, 60));
                    if (issue.DateReturned > DateTime.Now) issue.DateReturned = DateTime.Now.AddDays(-1);
                }

                issue.Items.Add(new IssueItem
                {
                    ProductName = prodForIssue[prodIdx],
                    Quantity = rnd.Next(1, 5),
                    WearPeriodDays = wearDays[prodIdx],
                    Size = szForIssue[prodIdx]
                });

                context.IssueReceipts.Add(issue);
            }

            base.Seed(context);
        }
    }
}