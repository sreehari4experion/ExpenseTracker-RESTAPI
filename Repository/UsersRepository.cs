using ExpenseTracker.Models;
using ExpenseTracker.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Repository
{
    public class UsersRepository: IUsersRepository
    {
        private readonly ExpenseTrackerDBContext _context;

        //constructor based dependency injection
        public UsersRepository(ExpenseTrackerDBContext context)
        {
            _context = context;
        }

        //get all users 
        public async Task<List<UsersView>> GetUsers()
        {
            if (_context != null)
            {
                return await (from u in _context.Users
                              select new UsersView
                              {
                                  UserId = u.UserId,
                                  Name = u.Name,
                                  UserName = u.UserName,

                              }
                              ).ToListAsync();
            }
            return null;
        }

        //get user by id
        public async Task<ActionResult<UsersView>> GetUserByID(int? id)
        {
            if (_context != null)
            {
                var user = await _context.Users.FindAsync(id);    //primary key

                return await (from u in _context.Users
                              where u.UserId == id
                              select new UsersView
                              {
                                  UserId = u.UserId,
                                  Name = u.Name,
                                  UserName = u.UserName,

                              }
                              ).FirstOrDefaultAsync();
            }
            return null;
        }

        //add a user
        public async Task<ActionResult<Users>> AddUser(Users user)
        {
            if (_context != null)
            {

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            return null;
        }

        //update a user
        #region update user
        public async Task UpdateUser(Users user)
        {
            if (_context != null)
            {
                _context.Entry(user).State = EntityState.Modified;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            //throw new NotImplementedException();
        }



        #endregion

        //delete a user
        #region delete User
        public async Task<int> DeleteUserByID(int? id)
        {
            // declare result
            int result = 0;
            if (_context != null)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
                if (user != null)
                {
                    // perform delete
                    _context.Users.Remove(user);
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
