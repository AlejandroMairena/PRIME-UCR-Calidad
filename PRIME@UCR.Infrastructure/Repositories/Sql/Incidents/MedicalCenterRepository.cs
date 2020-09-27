using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Incidents
{
    public class MedicalCenterRepository : GenericRepository<CentroMedico, int>, IMedicalCenterRepository
    {
        public MedicalCenterRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }
    }
}