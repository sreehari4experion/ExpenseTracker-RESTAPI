using ExpenseTracker.Models;
using ExpenseTracker.Repository.categoryRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IcategoryRepo _category;
        public CategoryController(IcategoryRepo category)
        {
            _category = category;
        }

     

        //get a category
        [HttpGet]
        public async Task<List<Category>> GetAllCategories()
        {
            return await _category.GetAllCategories();
        }

        //get category by id
        [HttpGet("{id}")]
        public async Task<Category> GetCategorybyId(int id)
        {
            return await _category.GetCategorybyId(id);
        }

        //add a category
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var catID = await _category.AddCategory(category);
                    if (catID > 0)
                    {
                        return Ok(catID);
                    }
                    return NotFound();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();

        }

        //update a category
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _category.UpdateCategory(category);
                    return Ok();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();

        }


        //delete item
        #region delete category by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryByID(int? id)
        {
            int result = 0;
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                result = await _category.DeleteCategoryByID(id);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok("delete successfull");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
