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
    class SqlItemRepository : GenericRepository<Item, int>, IItemRepository
    {
        public SqlItemRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        public async Task<IEnumerable<Item>> GetByName(string name)
        {
            return await this.GetByConditionAsync(itemModel => itemModel.Nombre == name);
        }

        public async Task<Item> GetItemById(int Id)
        {
            return (Item)await this.GetByConditionAsync(itemModel => itemModel.Id == Id);
        }

        public async Task<IEnumerable<Item>> GetByCheckListId(int checkListId)
        {
            return await this.GetByConditionAsync(itemModel => itemModel.IDLista == checkListId);
        }
        public async Task<IEnumerable<Item>> GetBySuperitemId(int superitemId)
        {
            return await this.GetByConditionAsync(itemModel => itemModel.IDSuperItem == superitemId);
        }
        public async Task<IEnumerable<Item>> GetCoreItems(int listId)
        {
            return await this.GetByConditionAsync(itemModel => itemModel.IDLista == listId && itemModel.IDSuperItem == null);
        }

        public async Task<Item> InsertCheckItemAsync(Item tempitem)
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
                    if (tempitem.IDSuperItem == null)
                    {
                        cmd.CommandText =
                        $"EXECUTE dbo.InsertarItemEnListaDeChequeo @nombre = '{tempitem.Nombre}', @imagenDescriptiva = '{tempitem.ImagenDescriptiva}', @descripcion = '{tempitem.Descripcion}', @orden = '{tempitem.Orden}', @IdSuperItem = NULL, @IdLista = '{tempitem.IDLista}'";
                    }
                    else {
                        cmd.CommandText =
                        $"EXECUTE dbo.InsertarItemEnListaDeChequeo @nombre = '{tempitem.Nombre}', @imagenDescriptiva = '{tempitem.ImagenDescriptiva}', @descripcion = '{tempitem.Descripcion}', @orden = '{tempitem.Orden}', @IdSuperItem = '{tempitem.IDSuperItem}', @IdLista = '{tempitem.IDLista}'";

                    }

                    tempitem.Id = int.Parse(s: cmd.ExecuteScalar().ToString());
                }
                return tempitem;
            });
        }
    }
}
