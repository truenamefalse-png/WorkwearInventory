namespace WorkwearInventory.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int Stock { get; set; }
        public int WearPeriodDays { get; set; }
        public string Size { get; set; }           // <-- НОВОЕ ПОЛЕ
        public string PhotoPath { get; set; }

        public virtual Category Category { get; set; }
    }
}