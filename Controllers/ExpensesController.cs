using ExpenseTracker.Models;
using ExpenseTracker.Repository.ExpensesRepo;
using ExpenseTracker.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpensesRepo _expenseRepo;
        public ExpensesController(IExpensesRepo expenseRepo)
        {
            _expenseRepo = expenseRepo;
        }
        //get all expenses
        [HttpGet]
        public async Task<List<Expenses>> GetExpenses()
        {
            return await _expenseRepo.GetExpenses();
        }
        //get expense by id
        [HttpGet("{id}")]

        public async Task<ActionResult<Expenses>> GetExpensesByID(int? id)
        {
            //return await _userRepository.GetUserByID(id);
            try
            {
                var expOne = await _expenseRepo.GetExpensesByID(id);
                if (expOne == null)
                {
                    return NotFound();
                }
                return expOne;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //get expense by user id
        [HttpGet]
        [Route("getExpenseOfUser/{id}")]
        public async Task<ActionResult<ExpensesView>> GetExpensesByUserID(int? id)
        {
            //return await _userRepository.GetUserByID(id);
            try
            {
                var userOne = await _expenseRepo.GetExpensesByUserID(id);
                if (userOne == null)
                {
                    return NotFound();
                }
                return userOne;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //get category whwere a user spend maximum amount
        
        [HttpGet]
        [Route("getMaxCategoryOfUser/{id}")]
        public async Task<ActionResult<CategoryView>> UsersCostliestCategory(int? id)
        {
            //return await _userRepository.GetUserByID(id);
            try
            {
                var userOne = await _expenseRepo.UsersCostliestCategory(id);
                if (userOne == null)
                {
                    return NotFound();
                }
                return userOne;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }





       [HttpPost]
        public async Task<ActionResult<Expenses>> AddExpense(Expenses exp)
        {
            return await _expenseRepo.AddExpense(exp);
        }

        //update expense
        #region update an expense
        [HttpPut]
        public async Task<IActionResult> UpdateExpense([FromBody] Expenses exp)
        {
            //since it is frombody we need to check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    await _expenseRepo.UpdateExpense(exp);
                    return Ok(exp);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        #endregion

        //delete expense
        #region delete expense by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenseByID(int? id)
        {
            int result = 0;
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                result = await _expenseRepo.DeleteExpenseByID(id);
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
