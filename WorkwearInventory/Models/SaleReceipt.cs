using System;
using System.Collections.Generic;

namespace WorkwearInventory.Models
{
    public class SaleReceipt
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public List<SaleItem> Items { get; set; } = new List<SaleItem>();
        public decimal TotalAmount { get; set; }
    }

    public class SaleItem
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal => Quantity * UnitPrice;
    }
}