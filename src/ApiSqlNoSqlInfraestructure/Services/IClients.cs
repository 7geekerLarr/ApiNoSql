using ApiNoSqlDomain.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiNoSqlInfraestructure.Services
{
    public interface IClients
    {
        Task<List<ClientModels>?> GetAll();
        Task<bool> Add(ClientModels entity);
        Task<ClientModels?> GetOne(string Id);        
        Task<bool> Upd(ClientModels entity);
        Task<bool> Del(string Id);
    }
}
