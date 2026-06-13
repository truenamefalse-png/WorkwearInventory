using System;
using System.Collections.Generic;

namespace WorkwearInventory.Models
{
    public class IssueReceipt
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? DateReturned { get; set; }         // дата возврата (null = не возвращено)
        public virtual ICollection<IssueItem> Items { get; set; } = new List<IssueItem>();
    }

    public class IssueItem
    {
        public int Id { get; set; }
        public int IssueReceiptId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int WearPeriodDays { get; set; }            // срок носки на момент выдачи

        public virtual IssueReceipt IssueReceipt { get; set; }
    }
}