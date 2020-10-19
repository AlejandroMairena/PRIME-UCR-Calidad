using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.CheckLists;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Services.CheckLists
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetAll();
        Task<Item> InsertItem(Item item);
        Task<Item> GetById(int id);
        Task<IEnumerable<Item>> GetByCheckListId(int checkListId);
        Task<Item> SaveImage(string imageName, Item item);
        Task<Item> UpdateItem(Item item);
    }
}