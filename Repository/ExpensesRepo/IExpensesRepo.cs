using ExpenseTracker.Models;
using ExpenseTracker.Views;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Repository.ExpensesRepo
{
    public interface IExpensesRepo
    {
        Task<List<Expenses>> GetExpenses();

        Task<ActionResult<Expenses>> GetExpensesByID(int? id);

        Task<ActionResult<Expenses>> AddExpense(Expenses exp);

        Task UpdateExpense(Expenses exp);

        Task<int> DeleteExpenseByID(int? id);

        Task<ActionResult<ExpensesView>> GetExpensesByUserID(int? id);

        Task<ActionResult<CategoryView>> UsersCostliestCategory(int? id);
    }
}
