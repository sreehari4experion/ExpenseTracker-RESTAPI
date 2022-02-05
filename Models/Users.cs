using System;
using System.Collections.Generic;

namespace ExpenseTracker.Models
{
    public partial class Users
    {
        public Users()
        {
            Expenses = new HashSet<Expenses>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }

        public virtual Login UserNameNavigation { get; set; }
        public virtual ICollection<Expenses> Expenses { get; set; }
    }
}
