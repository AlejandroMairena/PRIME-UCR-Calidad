using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.UserAdministration
{
    public class PerteneceRepository : GenericRepository<Pertenece, Tuple<string, string>>, IPerteneceRepository
    {
        public PerteneceRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        public async Task DeleteUserFromProfileAsync(string idUser, string idProfile)
        {
            await Task.Run(() =>
            {
                using (var cmd = _db.DbConnection.CreateCommand())
                {
                    while (cmd.Connection.State != System.Data.ConnectionState.Open)
                    {
                        cmd.Connection.Open();
                    }
                    if (cmd.Connection.State == System.Data.ConnectionState.Open)
                    {
                        cmd.CommandText =
                            $"EXECUTE dbo.DeleteUserFromProfile @idUser='{idUser}', @idProfile='{idProfile}'";

                        cmd.ExecuteNonQuery();
                    }

                }
            });
        }

        public async Task InsertUserToProfileAsync(string idUser, string idProfile)
        {
            await Task.Run(() =>
            {
                using (var cmd = _db.DbConnection.CreateCommand())
                {
                    while (cmd.Connection.State != System.Data.ConnectionState.Open)
                    {
                        cmd.Connection.Open();
                    }
                    if (cmd.Connection.State == System.Data.ConnectionState.Open)
                    {
                        cmd.CommandText =
                            $"EXECUTE dbo.InsertUserToProfile @idUsuario='{idUser}', @nombrePerfil='{idProfile}'";

                        cmd.ExecuteNonQuery();
                    }

                }
            });
        }
    }
}
