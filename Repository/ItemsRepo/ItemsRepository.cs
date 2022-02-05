using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Repository.ItemsRepo
{
    public class ItemsRepository:IItemRepository
    {
        private readonly ExpenseTrackerDBContext _context;

        //constructor based dependency injection
        public ItemsRepository(ExpenseTrackerDBContext context)
        {
            _context = context;
        }

        //get all Items 
        public async Task<List<Item>> GetItems()
        {
            if (_context != null)
            {
                return await _context.Item.ToListAsync();
                              
            }
            return null;
        }

        ////get item by id
        public async Task<ActionResult<Item>> GetItemByID(int? id)
        {
            if (_context != null)
            {
                return await _context.Item.FindAsync(id);    //primary key

                
            }
            return null;
        }

        //add an item
        public async Task<ActionResult<Item>> AddItem(Item item)
        {
            if (_context != null)
            {

                await _context.Item.AddAsync(item);
                await _context.SaveChangesAsync();
                return item;
            }
            return null;
        }

        //update an item
        #region update item
        public async Task UpdateItem(Item item)
        {
            if (_context != null)
            {
                _context.Entry(item).State = EntityState.Modified;
                _context.Item.Update(item);
                await _context.SaveChangesAsync();
            }
            //throw new NotImplementedException();
        }



        #endregion

        //delete a user
        #region delete item
        public async Task<int> DeleteItemByID(int? id)
        {
            // declare result
            int result = 0;
            if (_context != null)
            {
                var item = await _context.Item.FirstOrDefaultAsync(u => u.ItemId == id);
                if (item != null)
                {
                    // perform delete
                    _context.Item.Remove(item);
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
