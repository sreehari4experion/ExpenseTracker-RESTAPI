using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Repository.ItemsRepo
{
    public interface IItemRepository
    {
        Task<List<Item>> GetItems();

        Task<ActionResult<Item>> GetItemByID(int? id);

        Task<ActionResult<Item>> AddItem(Item item);

        Task UpdateItem(Item item);

        Task<int> DeleteItemByID(int? id);
    }
}
