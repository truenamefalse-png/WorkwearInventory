using System;

namespace WorkwearInventory.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; } = "Новая";   // Новая, Выполнена, Отклонена
    }
}