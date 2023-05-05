using ApiNoSqlDomain.Client;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiNoSqlApplication.Client.Queries
{
    public class GetClientByIdQuery
    {
        #region Execute
        public class GetSystemById : IRequest<ClientModels>
        {
            public string? ClientId { get; set; }
        }
        #endregion
    }
}
