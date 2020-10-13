using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.CheckLists;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Services.CheckLists
{
    public interface ICheckListService
    {
        Task<IEnumerable<CheckList>> GetAll();
        Task<CheckList> InsertCheckList(CheckList list);
        Task<CheckList> GetById(int id);
        Task<CheckList> SaveImage(string imageName, CheckList list);
        Task<CheckList> UpdateCheckList(CheckList list);
    }
}
