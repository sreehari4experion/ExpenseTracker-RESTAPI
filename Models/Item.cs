using System;
using System.Collections.Generic;

namespace ExpenseTracker.Models
{
    public partial class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int? ItemPrice { get; set; }
    }
}
