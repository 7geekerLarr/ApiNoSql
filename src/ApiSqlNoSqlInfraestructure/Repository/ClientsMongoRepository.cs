using ApiNoSqlDomain.Client;
using ApiNoSqlInfraestructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiNoSqlInfraestructure.Repository
{
    public class ClientsMongoRepository:IClients
    {      

        #region GetAll
        public  Task<List<ClientModels>?> GetAll()
        {
            List<ClientModels> ListClient = new List<ClientModels>
            {
                new ClientModels { id = 1, name = "name1", lastname = "Apellidos1", dni = 15001, birthdate = DateTime.Now, level = LevelClient.Mayorista, },
                new ClientModels { id = 2, name = "name2", lastname = "Apellidos2", dni = 15002, birthdate = DateTime.Now, level = LevelClient.Minorista }
            };

            return Task.FromResult<List<ClientModels>?>(ListClient);
        }
        #endregion
        #region GetOne
        public Task<ClientModels?> GetOne(string Id)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Add
        public Task<bool> Add(ClientModels entity)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Upd
        public Task<bool> Upd(ClientModels entity)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Del
        public Task<bool> Del(string Id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
