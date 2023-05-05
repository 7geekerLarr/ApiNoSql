using ApiNoSqlDomain.Client;
using ApiNoSqlInfraestructure.Services;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ApiNoSqlInfraestructure.Repository
{
    public class ClientsMongoRepository:IClients
    {
        private readonly string _connectionString;
        private readonly string _databaseName;
        private readonly string _collectionName;
        private readonly IMongoCollection<ClientModels> _collection;

        public ClientsMongoRepository(IConfiguration config)
        {
            _connectionString = config.GetSection("MongoDbSettings:ConnectionString").Value ?? "defaultDatabaseName"; ;
            _databaseName = config.GetSection("MongoDbSettings:DatabaseName").Value ?? "defaultDatabaseName"; ;
            _collectionName = config.GetSection("MongoDbSettings:CollectionName").Value ?? "defaultDatabaseName"; ;

            var mongoClient = new MongoClient(_connectionString);
            var database = mongoClient.GetDatabase(_databaseName);
            _collection = database.GetCollection<ClientModels>(_collectionName);
        }

        #region GetAll
        public  Task<List<ClientModels>?> GetAll()
        {
            throw new NotImplementedException();
            /*
            List<ClientModels> ListClient = new List<ClientModels>
            {
                new ClientModels { ClientId = "1", Name = "name1", Lastname = "Apellidos1", Dni = "15001", Birthdate = DateTime.Now, Level = 1, },
                new ClientModels { ClientId = "1", Name = "name2", Lastname = "Apellidos2", Dni = "15002", Birthdate = DateTime.Now, Level = 1, },

            };

            return Task.FromResult<List<ClientModels>?>(ListClient);*/
        }
        #endregion
        #region GetOne
        public Task<ClientModels?> GetOne(string Id)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Add
        public async Task<bool> Add(ClientModels entity)
        {

            try
            {
                
                
                var client = new ClientModels
                {
                    ClientId = entity.ClientId,
                    Level = entity.Level,
                    Tipo = entity.Tipo,
                    Name = entity.Name,
                    Lastname = entity.Lastname,
                    Dni = entity.Dni,
                    Birthdate = entity.Birthdate

                };                
                await _collection.InsertOneAsync(client);
                return true;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
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
