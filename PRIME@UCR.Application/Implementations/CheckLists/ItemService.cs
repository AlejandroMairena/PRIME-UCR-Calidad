using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Repositories.CheckLists;
using PRIME_UCR.Domain.Models.CheckLists;

namespace PRIME_UCR.Application.Implementations.CheckLists
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repo;
        public ItemService(IItemRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<Item>> GetAll()
        {
            IEnumerable<Item> items = await _repo.GetAllAsync();
            return items.OrderBy(item => item.Orden);
        }
        public async Task<Item> InsertItem(Item item)
        {
            return await _repo.InsertAsync(item);
        }

        public async Task<Item> GetById(int id)
        {
            return await _repo.GetByKeyAsync(id);
        }

        public async Task<IEnumerable<Item>> GetByCheckListId(int checkListId)
        {
            IEnumerable<Item> items = await _repo.GetByCheckListId(checkListId);
            return items.OrderBy(item => item.Orden);
        }

        public async Task<Item> SaveImage(string imageName, Item item)
        {
            item.ImagenDescriptiva = imageName;
            await _repo.UpdateAsync(item);
            return item;
        }
        public async Task<Item> UpdateItem(Item item)
        {
            await _repo.UpdateAsync(item);
            return item;
        }
    }
}
