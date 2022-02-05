using System;
using System.Collections.Generic;

namespace ExpenseTracker.Models
{
    public partial class ItemsDescription
    {
        public int Id { get; set; }
        public int? ItemsId { get; set; }
        public int? ItemId { get; set; }
    }
}
