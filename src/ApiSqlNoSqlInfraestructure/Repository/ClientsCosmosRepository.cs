using ApiNoSqlDomain.Client;
using ApiNoSqlInfraestructure.Entitys;
using ApiNoSqlInfraestructure.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiNoSqlInfraestructure.Repository
{
    public class ClientsCosmosRepository : IClients
    {
        #region VARS        
        private readonly string EndpointUri;
        private readonly string PrimaryKey;
        private readonly string DatabaseName;
        private readonly string ContainerName;

        private CosmosClient cosmosClient;
        private Database? database;
        private Container? container;
        #endregion
        #region ClientsCosmosRepository        
        public ClientsCosmosRepository(IConfiguration config)
        {
            EndpointUri = config.GetSection("CosmosDB:EndpointUri").Value ?? throw new Exception("EndpointUri is not configured.");
            PrimaryKey = config.GetSection("CosmosDB:PrimaryKey").Value ?? throw new Exception("PrimaryKey is not configured.");
            DatabaseName = config.GetSection("CosmosDB:DatabaseName").Value ?? throw new Exception("DatabaseName is not configured.");
            ContainerName = config.GetSection("CosmosDB:ContainerName").Value ?? throw new Exception("ContainerName is not configured.");

            cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions() { ApplicationName = "API Clients" });
            database = cosmosClient.GetDatabase(DatabaseName);
            container = database.GetContainer(ContainerName);
        }
        #endregion        
        #region GetAll
        public async Task<List<ClientModels>?> GetAll()
        {
            QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c");
            FeedIterator<ClientModels> queryResultSetIterator = container!.GetItemQueryIterator<ClientModels>(queryDefinition);
            List<ClientModels> results = new List<ClientModels>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<ClientModels> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                results.AddRange(currentResultSet);
            }

            return results;
        }

        #endregion
        #region GetOne
        public async Task<ClientModels?> GetOne(string ClientId)
        {
            QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c WHERE c.ClientId = @ClientId")
                .WithParameter("@ClientId", ClientId);
            FeedIterator<ClientModels> queryResultSetIterator = container!.GetItemQueryIterator<ClientModels>(queryDefinition);

            if (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<ClientModels> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                return currentResultSet.FirstOrDefault();
            }

            return null;
        }

        #endregion
        #region Add       
        public async Task<bool> Add(ClientModels entity)
        {           

            var client = new ClientModelsCOSMODB
            {
                Id = Guid.NewGuid().ToString(),  // Asigna un nuevo ID único al documento
                ClientId = entity.ClientId,
                Level = entity.Level,
                Tipo = entity.Tipo,
                PartitionKey = entity.ClientId,
                Person = new PersonModelsCOSMODB
                {
                    Name = entity.Person?.Name,
                    Lastname = entity.Person?.Lastname,
                    Dni = entity.Person?.Dni,
                    ClientId = entity.Person?.ClientId,
                    Birthdate = entity.Person?.Birthdate ?? DateTime.MinValue
                }
            };

            ItemResponse<ClientModelsCOSMODB> response = await this.container!.CreateItemAsync(client);
            return response.StatusCode == System.Net.HttpStatusCode.Created;
        }

        #endregion       
        #region Upd
        public async Task<bool> Upd(ClientModels entity)
        {
            //await EnsureDatabaseAndContainerCreatedAsync();

            var client = new ClientModelsCOSMODB
            {
                Level = entity.Level,
                Tipo = entity.Tipo,
                ClientId = entity.ClientId,
                PartitionKey= entity.ClientId,
                Person = new PersonModelsCOSMODB
                {
                    Name = entity.Person?.Name,
                    Lastname = entity.Person?.Lastname,
                    Dni = entity.Person?.Dni,
                    ClientId = entity.ClientId,
                    Birthdate = entity.Person?.Birthdate ?? DateTime.MinValue
                }
            };

            // Consulta para encontrar el documento con el ClientId específico
            QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c WHERE c.ClientId = @ClientId")
                .WithParameter("@ClientId", entity.ClientId);
            FeedIterator<ClientModelsCOSMODB> queryResultSetIterator = this.container!.GetItemQueryIterator<ClientModelsCOSMODB>(queryDefinition);

            if (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<ClientModelsCOSMODB> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                ClientModelsCOSMODB existingClient = currentResultSet.FirstOrDefault() ?? new ClientModelsCOSMODB();

                if (existingClient != null)
                {
                    // Actualiza el documento con los nuevos valores
                    client.Id = existingClient.Id;
                    ItemResponse<ClientModelsCOSMODB> response = await this.container!.ReplaceItemAsync(client, client.Id);
                    return response.StatusCode == System.Net.HttpStatusCode.OK;
                }
            }

            return false;
        }
        #endregion
        #region Del        
        public async Task<bool> Del(string ClientId)
        {
            QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c WHERE c.ClientId = @ClientId")
                .WithParameter("@ClientId", ClientId);
            FeedIterator<ClientModelsCOSMODB> queryResultSetIterator = this.container!.GetItemQueryIterator<ClientModelsCOSMODB>(queryDefinition);

            if (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<ClientModelsCOSMODB> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                ClientModelsCOSMODB existingClient = currentResultSet.FirstOrDefault() ?? new ClientModelsCOSMODB();

                if (existingClient != null)
                {
                    string existingClientId = existingClient.Id!;
                    string existingPartitionKey = existingClient.ClientId!;

                    if (!string.IsNullOrEmpty(existingClientId) && !string.IsNullOrEmpty(existingPartitionKey))
                    {
                        ItemResponse<ClientModelsCOSMODB> response = await this.container!.DeleteItemAsync<ClientModelsCOSMODB>(existingClientId, new PartitionKey(existingPartitionKey));
                        return response.StatusCode == System.Net.HttpStatusCode.NoContent;
                    }
                }
            }

            return false;
        }



        #endregion
    }
}
