﻿using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.CheckLists;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Services.CheckLists
{
    public interface IInstanceChecklistService
    {
        Task<IEnumerable<InstanceChecklist>> GetAll();
        Task<InstanceChecklist> InsertInstanceChecklist(InstanceChecklist list);
        Task<InstanceChecklist> GetById(int id);
        Task<IEnumerable<InstanceChecklist>> GetByIncidentCod(string cod);
        Task<InstanceChecklist> UpdateInstanceChecklist(InstanceChecklist list);
        Task DeleteInstanceChecklist(int id, string cod);
        //Task<Item> InsertInstanceChecklistItem(Item item);
        //Task<IEnumerable<Item>> GetItemsByInstanceChecklistId(int checkListId);
        //Task<IEnumerable<Item>> GetItemsBySuperitemId(int superItemId);
        // Task<IEnumerable<Item>> GetCoreItems(int checkListId);
        //Task<Item> SaveImageItem(Item item);
        //Task<Item> UpdateItem(Item item);
    }
}
