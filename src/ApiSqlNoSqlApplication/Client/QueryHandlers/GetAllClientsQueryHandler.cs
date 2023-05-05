using ApiNoSqlApplication.Client.Queries;
using ApiNoSqlApplication.HandleError;
using ApiNoSqlDomain.Client;
using ApiNoSqlInfraestructure.Services;
using MediatR;
using System.Net;
using IClients = ApiNoSqlInfraestructure.Services.IClients;

namespace ApiNoSqlApplication.Client.QueryHandlers
{
    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, List<ClientModels>>
    {
        #region GetAllClientsQueryHandler

        private readonly IClients _clientsRepository;
        public GetAllClientsQueryHandler(IClients clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }

        public async Task<List<ClientModels>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var result = await _clientsRepository.GetAll();
            if (result == null)
            {
                throw new HandleException(HttpStatusCode.NotFound, new { GySistemas = "No hay clientes cargados en estos momentos!" });
            }
            return result?.OrderBy(x => x.Name).ToList() ?? new List<ClientModels>();
        }

        #endregion

    }
}
