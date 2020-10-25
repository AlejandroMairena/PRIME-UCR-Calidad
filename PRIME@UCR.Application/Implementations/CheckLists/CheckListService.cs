﻿using PRIME_UCR.Application.Services.CheckLists;
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
    /**
     * Class used to manage checklists and their items
     */
    public class CheckListService : ICheckListService
    {
        private readonly ICheckListRepository _checklistRepository;
        private readonly IItemRepository _itemRepository;

        public CheckListService(ICheckListRepository checklistRepository, IItemRepository itemRepository)
        {
            _checklistRepository = checklistRepository;
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<CheckList>> GetAll()
        {
            IEnumerable<CheckList> lists = await _checklistRepository.GetAllAsync();
            return lists.OrderBy(checklist => checklist.Orden);
        }
        public async Task<CheckList> InsertCheckList(CheckList list) 
        {
            return await _checklistRepository.InsertAsync(list);
        }

        public async Task<CheckList> GetById(int id)
        {
            return await _checklistRepository.GetByKeyAsync(id);
        }

        public async Task<CheckList> UpdateCheckList(CheckList list)
        {
            await _checklistRepository.UpdateAsync(list);
            return list;
        }

        public async Task<Item> InsertCheckListItem(Item item)
        {
            return await _itemRepository.InsertAsync(item);
        }
        public async Task<IEnumerable<Item>> GetItemsByCheckListId(int checkListId)
        {
            IEnumerable<Item> items = await _itemRepository.GetByCheckListId(checkListId);
            return items.OrderBy(item => item.Orden);
        }
        public async Task<IEnumerable<Item>> GetItemsBySuperitemId(int superitemId)
        {
            IEnumerable<Item> items = await _itemRepository.GetBySuperitemId(superitemId);
            return items.OrderBy(item => item.Orden);
        }
        public async Task<Item> SaveImageItem(Item item)
        {
            return await _itemRepository.InsertAsync(item);
        }
        public async Task<Item> UpdateItem(Item item)
        {
            await _itemRepository.UpdateAsync(item);
            return item;
        }
    }
}
