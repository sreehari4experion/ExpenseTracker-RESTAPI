using System;
using System.Collections.Generic;

namespace ExpenseTracker.Models
{
    public partial class Expenses
    {
        public int ExpId { get; set; }
        public int? UserId { get; set; }
        public DateTime? Date { get; set; }
        public int? ExpenseAmount { get; set; }
        public int? CategoryId { get; set; }
        public int? ItemsId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Users User { get; set; }
    }
}
