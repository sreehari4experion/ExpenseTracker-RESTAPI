using ExpenseTracker.Models;
using ExpenseTracker.Repository.ItemsRepo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace ExpenseTracker.Controllers
{
   
        [Route("api/[controller]")]
        [ApiController]
        public class ItemController : ControllerBase
        {
            private readonly IItemRepository _itemRepo;
            public ItemController(IItemRepository itemRepo)
            {
            _itemRepo = itemRepo;
            }
            [HttpGet]
            public async Task<List<Item>> GetItems()
            {
                return await _itemRepo.GetItems();
            }

        //get item by id
        [HttpGet("{id}")]

        public async Task<ActionResult<Item>> GetItemByID(int? id)
        {
            //return await _userRepository.GetUserByID(id);
            try
            {
                var itemOne = await _itemRepo.GetItemByID(id);
                if (itemOne == null)
                {
                    return NotFound();
                }
                return itemOne;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //add an item
        [HttpPost]
        public async Task<ActionResult<Item>> AddItem(Item item)
        {
            return await _itemRepo.AddItem(item);
        }

        //update an item

        #region update an item
        [HttpPut]
        public async Task<IActionResult> UpdateItem([FromBody] Item item)
        {
            //since it is frombody we need to check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    await _itemRepo.UpdateItem(item);
                    return Ok(item);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        #endregion

        //delete item
        #region delete item by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemByID(int? id)
        {
            int result = 0;
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                result = await _itemRepo.DeleteItemByID(id);
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
