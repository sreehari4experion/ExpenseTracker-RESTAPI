using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Views
{
    public class ExpensesView
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        public DateTime? Date { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<ItemDetailsView> ItemList { get; set; }
    }
}
