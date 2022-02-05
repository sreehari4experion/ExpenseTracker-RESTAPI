using System;
using System.Collections.Generic;

namespace ExpenseTracker.Models
{
    public partial class Category
    {
        public Category()
        {
            Expenses = new HashSet<Expenses>();
        }

        public int CategoryId { get; set; }
        public string Category1 { get; set; }

        public virtual ICollection<Expenses> Expenses { get; set; }
    }
}
