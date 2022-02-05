using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Views
{
    public class ExpensesModel
    {
        public int ExpId { get; set; }
        public int? Userid { get; set; }
        public string Username { get; set; }
        public Int64 Phone { get; set; }

        public DateTime? ExpenseDate { get; set; }
        public List<ItemDetailsView> Itemlist { get; set; }
      
    }
}
