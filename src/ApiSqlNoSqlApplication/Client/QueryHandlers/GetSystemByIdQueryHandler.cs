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
using static ApiNoSqlApplication.Client.Queries.GetClientByIdQuery;

namespace ApiNoSqlApplication.Client.QueryHandlers
{
    internal class GetSystemByIdQueryHandler
    {
        #region HandlerClass
        public class HandlerClass : IRequestHandler<GetSystemById, ClientModels>
        {
            private readonly IClients _clientsRepository;
            public HandlerClass(IClients clientsRepository)
            {
                _clientsRepository = clientsRepository;
            }
            public async Task<ClientModels> Handle(GetSystemById request, CancellationToken cancellationToken)
            {
                if (request.ClientId == "0")
                {
                    throw new HandleException(HttpStatusCode.BadRequest, new { GySistemas = "Id del cliente no es valido, no puede ser (0)" });
                }
                if (request.ClientId == null)
                {
                    throw new HandleException(HttpStatusCode.BadRequest, new { GySistemas = "Id del cliente no es valido, no puede ser (0)" });
                }
                var result = await _clientsRepository.GetOne(request.ClientId);
                if (result == null)
                {
                    throw new HandleException(HttpStatusCode.NotFound, new { Cliente = "cliente no encontrado" });
                }
                return result;


            }


        }
        #endregion
    }
}
