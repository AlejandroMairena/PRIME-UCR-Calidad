using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

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
    }
}
