using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Repositories.UserAdministration
{
    public interface IUsuarioRepository : IGenericRepository<Usuario, string>
    {
        Task<Usuario> GetWithDetailsAsync(string id);

        Task<List<Usuario>> GetAllUsersWithDetailsAsync();
    }


}
