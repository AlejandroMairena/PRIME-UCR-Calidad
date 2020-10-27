using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Repositories.UserAdministration
{
    public interface IPermiteRepository : IGenericRepository<Permite,Tuple<string, int>>
    {
        Task DeletePermissionAsync(string idProfile, int idPermission);

        Task InsertPermissionAsync(string idProfile, int idPermission);
    }
}
