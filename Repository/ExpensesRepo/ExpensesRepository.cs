using ExpenseTracker.Models;
using ExpenseTracker.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Repository.ExpensesRepo
{
    public class ExpensesRepository:IExpensesRepo
    {
        private readonly ExpenseTrackerDBContext _context;

        //constructor based dependency injection
        public ExpensesRepository(ExpenseTrackerDBContext context)
        {
            _context = context;
        }

        //get all users 
        public async Task<List<Expenses>> GetExpenses()
        {
            if (_context != null)
            {
                return await _context.Expenses.ToListAsync();
                //return await (from u in _context.Expenses
                //              select new UsersView
                //              {
                //                  UserId = u.UserId,
                //                  Name = u.Name,
                //                  UserName = u.UserName,

                //              }
                //              ).ToListAsync();
            }
            return null;
        }

        //get Expense by id
        public async Task<ActionResult<Expenses>> GetExpensesByID(int? id)
        {
            if (_context != null)
            {
                return await _context.Expenses.FindAsync(id);    //primary key

                //var user = await _context.Users.FindAsync(id);    //primary key

                //return await (from u in _context.Users
                //              where u.UserId == id
                //              select new UsersView
                //              {
                //                  UserId = u.UserId,
                //                  Name = u.Name,
                //                  UserName = u.UserName,

                //              }
                //              ).FirstOrDefaultAsync();
            }
            return null;
        }


        //get Expense by user id
        public async Task<ActionResult<ExpensesView>> GetExpensesByUserID(int? id)
        {
            if (_context != null)
            {
               

                var expense = await _context.Expenses.FindAsync(id);    //primary key

                return await (from
                              u in _context.Users
                              join
                              e in _context.Expenses
                              on u.UserId equals e.UserId
                              join
                              C in _context.Category
                              on e.CategoryId equals C.CategoryId
                              where e.UserId == id
                              select new ExpensesView
                              {

                                  UserId = u.UserId,
                                  Name = u.Name,
                                  Date = e.Date,
                                  CategoryId = C.CategoryId,
                                  CategoryName = C.Category1,
                                  ItemList = (from c in _context.Category
                                              join e in _context.Expenses on c.CategoryId equals e.CategoryId
                                              join Id in _context.ItemsDescription on e.ItemsId equals Id.ItemsId
                                              join it in _context.Item on Id.ItemId equals it.ItemId
                                              where e.UserId == id
                                              //where e.UserId == id
                                              select new ItemDetailsView
                                              {
                                                  ItemId = it.ItemId,
                                                  ItemName = it.ItemName,
                                                  Category = c.Category1
                                              }

                                  ).ToList()

                              }
                              ).FirstOrDefaultAsync();

            }
            return null;


        }

        //category whwere a user spend maximum
        public async Task<ActionResult<CategoryView>> UsersCostliestCategory(int? id)
        {
            if (_context != null)
            {
                //return await _context.Expenses.FindAsync(id);    //primary key

                var expense = await _context.Expenses.FindAsync(id);    //primary key

                return await (from u in _context.Users
                              join e in _context.Expenses
                              on u.UserId equals e.UserId
                              join c in _context.Category
                              on e.CategoryId equals c.CategoryId
                              where u.UserId == id && e.Date == DateTime.Today
                              select new CategoryView
                              {
                                  Name = u.UserName,
                                  Category = (from u in _context.Users
                                              join
                                              e in _context.Expenses
                                              on u.UserId equals e.UserId
                                              join
                                              c in _context.Category
                                              on c.CategoryId equals e.CategoryId
                                              //where u.UserId == id && e.Date == DateTime.Today
                                              group e.ExpenseAmount by c.Category1 into grp

                                              orderby grp.Max() descending
                                              select grp.Key
                                             ).FirstOrDefault()
                              }

                              ).FirstOrDefaultAsync();

            }
            return null;
        }


        //add an expense
        public async Task<ActionResult<Expenses>> AddExpense(Expenses exp)
        {
            if (_context != null)
            {

                await _context.Expenses.AddAsync(exp);
                await _context.SaveChangesAsync();
                return exp;
            }
            return null;
        }

        //update an expense
        #region update expense
        public async Task UpdateExpense(Expenses exp)
        {
            if (_context != null)
            {
                _context.Entry(exp).State = EntityState.Modified;
                _context.Expenses.Update(exp);
                await _context.SaveChangesAsync();
            }
            //throw new NotImplementedException();
        }



        #endregion

        //delete an expense
        #region delete an expense
        public async Task<int> DeleteExpenseByID(int? id)
        {
            // declare result
            int result = 0;
            if (_context != null)
            {
                var exp = await _context.Expenses.FirstOrDefaultAsync(u => u.ExpId == id);
                if (exp != null)
                {
                    // perform delete
                    _context.Expenses.Remove(exp);
                    result = await _context.SaveChangesAsync(); // commit 
                    //return succcess;
                    result = 1;

                }
                return result;
            }
            return result;

            //throw new NotImplementedException();
        }
        #endregion
    }
}
