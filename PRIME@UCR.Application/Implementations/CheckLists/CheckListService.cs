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
using PRIME_UCR.Application.Services.UserAdministration;
using System.ComponentModel.DataAnnotations;
using PRIME_UCR.Application.Permissions.CheckLists;

namespace PRIME_UCR.Application.Implementations.CheckLists
{
    /**
     * Class used to manage checklists and their items
     */
    public partial class CheckListService : ICheckListService
    {
        private readonly ICheckListRepository _checklistRepository;
        private readonly ICheckListTypeRepository _checkListTypeRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IPrimeSecurityService _primeSecurityService;

        public CheckListService(ICheckListRepository checklistRepository,
                                ICheckListTypeRepository checkListTypeRepository,
                                IItemRepository itemRepository,
                                IPrimeSecurityService primeSecurityService)
        {
            _checklistRepository = checklistRepository;
            _checkListTypeRepository = checkListTypeRepository;
            _itemRepository = itemRepository;
            _primeSecurityService = primeSecurityService;
        }

        public async Task<IEnumerable<CheckList>> GetAll()
        {
            IEnumerable<CheckList> lists = await _checklistRepository.GetAllAsync();
            return lists.OrderBy(checklist => checklist.Orden);
        }

        public async Task<IEnumerable<TipoListaChequeo>> GetTypes()
        {
            return await _checkListTypeRepository.GetAllAsync();
        }

        public async Task<CheckList> InsertCheckList(CheckList list)
        {
            await _primeSecurityService.CheckIfIsAuthorizedAsync(GetType());
            return await _checklistRepository.InsertCheckListAsync(list);
        }

        public async Task<CheckList> GetById(int id)
        {
            return await _checklistRepository.GetByKeyAsync(id);
        }

        public async Task<Item> GetItemById(int Id)
        {
            return await _itemRepository.GetByKeyAsync(Id);
        }

        public async Task<CheckList> UpdateCheckList(CheckList list)
        {
            await _primeSecurityService.CheckIfIsAuthorizedAsync(GetType());
            await _checklistRepository.UpdateAsync(list);
            return list;
        }

        public async Task<Item> InsertCheckListItem(Item item)
        {
            await _primeSecurityService.CheckIfIsAuthorizedAsync(GetType());
            return await _itemRepository.InsertCheckItemAsync(item);
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
        public async Task<IEnumerable<Item>> GetCoreItems(int checkListId)
        {
            IEnumerable<Item> items = await _itemRepository.GetCoreItems(checkListId);
            return items.OrderBy(item => item.Orden);
        }
        public async Task<Item> SaveImageItem(Item item)
        {
            await _primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
            return await _itemRepository.InsertAsync(item);
        }
        public async Task<Item> UpdateItem(Item item)
        {
            await _primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
            await _itemRepository.UpdateAsync(item);
            return item;
        }
    }

    [MetadataType(typeof(CheckListServiceAuthorization))]
    public partial class CheckListService { }
}
