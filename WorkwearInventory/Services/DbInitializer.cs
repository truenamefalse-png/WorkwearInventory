using System.Data.Entity;
using WorkwearInventory.Models;

namespace WorkwearInventory.Services
{
    // Изменено на DropCreateDatabaseIfModelChanges
    public class DbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            // Пользователи
            context.Users.Add(new User { Username = "admin", Password = "admin" });

            // Категории
            context.Categories.Add(new Category { Name = "Костюмы" });
            context.Categories.Add(new Category { Name = "Обувь" });
            context.Categories.Add(new Category { Name = "Головные уборы" });

            context.SaveChanges(); // чтобы получить Id

            // Товары
            context.Products.Add(new Product
            {
                Name = "Костюм сварщика",
                Description = "Огнестойкий костюм для сварочных работ",
                CategoryId = 1,
                Stock = 10,
                Price = 3500m,
                PhotoPath = ""
            });
            context.Products.Add(new Product
            {
                Name = "Ботинки кожаные",
                Description = "Защитные ботинки с металлическим подноском",
                CategoryId = 2,
                Stock = 25,
                Price = 2200m,
                PhotoPath = ""
            });

            base.Seed(context);
        }
    }
}