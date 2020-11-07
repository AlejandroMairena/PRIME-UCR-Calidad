using System;
using System.Threading.Tasks;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Application.Repositories.Appointments;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Appointments
{
    public class AppointmentRepository : RepoDbRepository<Cita, int>, IAppointmentRepository
    {
        public AppointmentRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }
    }
}