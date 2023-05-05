using ApiNoSqlDomain.Client;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiNoSqlApplication.Client.Queries
{
    public class GetAllClientsQuery
    {
        #region ExecuteList       
        public class GetAllClients : IRequest<List<ClientModels>>
        {

        }
        #endregion
    }
}
