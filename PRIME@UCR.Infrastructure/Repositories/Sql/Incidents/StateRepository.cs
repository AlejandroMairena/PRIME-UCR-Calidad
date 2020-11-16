using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Application.Services.UserAdministration;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;
using PRIME_UCR.Infrastructure.DataProviders;
using PRIME_UCR.Infrastructure.Permissions.Incidents;
using RepoDb;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Incidents
{
    public partial class StateRepository : RepoDbRepository<Estado, string>, IStateRepository
    {
        private readonly IPrimeSecurityService _primeSecurityService;
        public StateRepository(ISqlDataProvider dataProvider, IPrimeSecurityService primeSecurityService) : base(dataProvider)
        {
            _primeSecurityService = primeSecurityService;
        }

        public async Task<IEnumerable<Estado>> GetAllStates()
        {
            await _primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
            using (var connection = new SqlConnection(_db.ConnectionString))
            {
                return await connection.QueryAllAsync<Estado>();
            }
        }
    }
    [MetadataType(typeof(StateRepositoryPermissions))]
    public partial class StateRepository
    {
    }

}