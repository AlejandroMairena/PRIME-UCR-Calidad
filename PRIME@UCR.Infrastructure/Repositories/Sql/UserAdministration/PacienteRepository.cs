using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration
{
    public class PacienteRepository : GenericRepository<Paciente, string>, IPacienteRepository
    {
        public PacienteRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        public async Task<Paciente> InsertPatientOnlyAsync(Paciente entity)
        {
            return await Task.Run(() =>
            {
                // raw sql
                using (var cmd = _db.DbConnection.CreateCommand())
                {
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    
                    cmd.CommandText =
                        $"EXECUTE dbo.InsertarPacienteSolo '{entity.Cédula}'";
     
                    Console.WriteLine(new SqlDateTime(DateTime.Now).ToSqlString());
                    cmd.ExecuteNonQuery();
                }

                return entity;
            });
        }
    }
}
