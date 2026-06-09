using System;
using System.Collections.Generic;

namespace WorkwearInventory.Models
{
    /// <summary>
    /// Документ выдачи спецодежды сотруднику
    /// </summary>
    public class IssueReceipt
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; }
        public string EmployeeName { get; set; }          // Кому выдано
        public decimal TotalAmount { get; set; }          // Общая стоимость выданного (для учёта)

        public virtual ICollection<IssueItem> Items { get; set; } = new List<IssueItem>();
    }

    public class IssueItem
    {
        public int Id { get; set; }
        public int IssueReceiptId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Вычисляемое свойство (не хранится в БД)
        public decimal LineTotal => Quantity * UnitPrice;

        public virtual IssueReceipt IssueReceipt { get; set; }
    }
}