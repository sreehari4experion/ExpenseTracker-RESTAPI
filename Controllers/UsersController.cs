using ExpenseTracker.Models;
using ExpenseTracker.Repository;
using ExpenseTracker.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _userRepository;
        public UsersController(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]

        public async Task<List<UsersView>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<UsersView>> GetUserByID(int? id)
        {
            //return await _userRepository.GetUserByID(id);
            try
            {
                var userOne = await _userRepository.GetUserByID(id);
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
        public async Task<ActionResult<Users>> AddUser(Users user)
        {
            return await _userRepository.AddUser(user);
        }

        //update a user
        #region update a user
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] Users user)
        {
            //since it is frombody we need to check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    await _userRepository.UpdateUser(user);
                    return Ok(user);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        #endregion

        //delete user
               #region delete user by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserByID(int? id)
        {
            int result = 0;
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                result = await _userRepository.DeleteUserByID(id);
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
