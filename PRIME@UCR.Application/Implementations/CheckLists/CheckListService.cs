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

namespace PRIME_UCR.Application.Implementations.CheckLists
{

    public class CheckListService : ICheckListService
    {
        private readonly ICheckListRepository _repo;
        public CheckListService(ICheckListRepository repo) 
        {
            _repo = repo;
        }
        public async Task<IEnumerable<CheckList>> GetAll()
        {
            IEnumerable<CheckList> lists = await _repo.GetAllAsync();
            return lists.OrderBy(checklist => checklist.Orden);
        }
        public async Task<CheckList> InsertCheckList(CheckList list) 
        {
            return await _repo.InsertAsync(list);
        }

        public async Task<CheckList> GetById(int id)
        {
            return await _repo.GetByKeyAsync(id);
        }

        // User Story PIG01IIC20-267 LG - Agregar imagen descriptiva a lista de chequeo
        // Set NombreImagen from the list to imageName, and updates the database
        public async Task<CheckList> SaveImage(string imageName, CheckList list)
        {
            list.NombreImagen = imageName;
            await _repo.UpdateAsync(list);
            return list;
        }
        public async Task<CheckList> UpdateCheckList(CheckList list)
        {
            await _repo.UpdateAsync(list);
            return list;
        }
    }
}
