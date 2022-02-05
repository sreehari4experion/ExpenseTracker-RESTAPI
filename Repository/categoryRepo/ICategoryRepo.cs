using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Repository.categoryRepo
{
    public interface IcategoryRepo
    {
        Task<List<Category>> GetAllCategories();


        Task<Category> GetCategorybyId(int id);

        // Add category
        Task<int> AddCategory(Category category);

        //// Update a category
        Task UpdateCategory(Category category);

        //delete category

        Task<int> DeleteCategoryByID(int? id);


    }
}