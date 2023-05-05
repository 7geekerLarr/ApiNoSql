using ApiNoSqlApplication.HandleError;
using ApiNoSqlDomain.Client;
using ApiNoSqlInfraestructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static ApiNoSqlApplication.Client.Queries.GetAllClientsQuery;

namespace ApiNoSqlApplication.Client.QueryHandlers
{
    public class GetAllClientsQueryHandler
    {
        #region HandlerClass
        public class HandlerClass : IRequestHandler<GetAllClients, List<ClientModels>>
        {
            private readonly IClients _clientsRepository;
            public HandlerClass(IClients clientsRepository)
            {
                _clientsRepository = clientsRepository;
            }

            public async Task<List<ClientModels>> Handle(GetAllClients request, CancellationToken cancellationToken)
            {
                var result = await _clientsRepository.GetAll();
                if (result == null)
                {
                    throw new HandleException(HttpStatusCode.NotFound, new { GySistemas = "No hay clientes cargados en estos momentos!" });
                }
                return result?.OrderBy(x => x.Name).ToList() ?? new List<ClientModels>();
            }
        }
        #endregion

    }
}
