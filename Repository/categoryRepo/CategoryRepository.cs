using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Repository.categoryRepo
{
    public class CategoryRepository : IcategoryRepo
    {
        private readonly ExpenseTrackerDBContext _contextone;
        public CategoryRepository(ExpenseTrackerDBContext contextone)
        {
            _contextone = contextone;
        }


        //get all categories
        public async Task<List<Category>> GetAllCategories()
        {
            if (_contextone != null)
            {
                var category = await _contextone.Category.ToListAsync();
                return category;

            }
            return null;
        }


        //get category by id
        public async Task<Category> GetCategorybyId(int id)
        {
            if (_contextone != null)
            {
                var category = await _contextone.Category.FindAsync(id);
                return category;
            }
            return null;
        }

        //add a category
        public async Task<int> AddCategory(Category category)
        {
            if (_contextone != null)
            {
                await _contextone.Category.AddAsync(category);
                await _contextone.SaveChangesAsync();
                return category.CategoryId;
            }
            return 0;
        }





        //update category
        public async Task UpdateCategory(Category category)
        {
            if (_contextone != null)
            {
                _contextone.Entry(category).State = EntityState.Modified;
                _contextone.Category.Update(category);
                await _contextone.SaveChangesAsync();


            }


        }


        //delete a category
        #region delete category
        public async Task<int> DeleteCategoryByID(int? id)
        {
            // declare result
            int result = 0;
            if (_contextone != null)
            {
                var item = await _contextone.Category.FirstOrDefaultAsync(u => u.CategoryId == id);
                if (item != null)
                {
                    // perform delete
                    _contextone.Category.Remove(item);
                    result = await _contextone.SaveChangesAsync(); // commit 
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