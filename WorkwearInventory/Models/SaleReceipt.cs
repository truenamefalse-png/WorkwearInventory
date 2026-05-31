using System;
using System.Collections.Generic;

namespace WorkwearInventory.Models
{
    public class SaleReceipt
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
    }

    public class SaleItem
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Вычисляемое свойство – не хранится в БД
        public decimal LineTotal => Quantity * UnitPrice;

        public virtual SaleReceipt Sale { get; set; }
    }
}