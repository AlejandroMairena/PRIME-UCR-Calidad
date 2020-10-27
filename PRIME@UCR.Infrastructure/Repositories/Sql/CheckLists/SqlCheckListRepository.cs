using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Application.Repositories.CheckLists;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.CheckLists;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.CheckLists
{
    class SqlCheckListRepository : GenericRepository<CheckList, int>, ICheckListRepository
    {
        public SqlCheckListRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        public async Task<CheckList> InsertCheckListAsync(CheckList list)
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
                        $"EXECUTE dbo.InsertarListaChequeo @nombre = '{list.Nombre}', @tipo = '{list.Tipo}', @descripcion = '{list.Descripcion}', @orden = '{list.Orden}', @imagenDescriptiva = '{list.ImagenDescriptiva}'";

                    list.Id = int.Parse(s: cmd.ExecuteScalar().ToString());
                }
                return list;
            });
        }

        public async Task<IEnumerable<CheckList>> GetByName(string name)
        {
            return await this.GetByConditionAsync(checkListModel => checkListModel.Nombre == name);
        }
    }
}
