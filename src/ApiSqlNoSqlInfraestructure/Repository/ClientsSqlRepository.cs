using ApiNoSqlDomain.Client;
using ApiNoSqlInfraestructure.Data;
using ApiNoSqlInfraestructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiNoSqlInfraestructure.Repository
{
    public class ClientsSqlRepository:IClients
    {
        private readonly ClientsContext context;
        public ClientsSqlRepository(ClientsContext _context)
        {
            this.context = _context;
        }
        #region GetAll
        public async Task<List<ClientModels>?> GetAll()
        {
            return await context.Clients.ToListAsync();            
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
