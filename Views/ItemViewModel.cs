using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Views
{
    public class ItemViewModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }

        public string Category { get; set; }
        public int? ItemPrice { get; set; }

    }
}
