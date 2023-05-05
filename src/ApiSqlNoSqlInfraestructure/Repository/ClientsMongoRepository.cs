using ApiNoSqlDomain.Client;
using ApiNoSqlInfraestructure.Entitys;
using ApiNoSqlInfraestructure.Services;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;


namespace ApiNoSqlInfraestructure.Repository
{
    public class ClientsMongoRepository: IClients
    {
        #region Vars       
        private readonly string _connectionString;
        private readonly string _databaseName;
        private readonly string _collectionName;
        private readonly IMongoCollection<ClientModelsMDB> _collection;
        private readonly IMapper _mapper;
        #endregion
        #region ClientsMongoRepository       
        public ClientsMongoRepository(IConfiguration config, IMapper mapper)
        {
            _connectionString = config.GetSection("MongoDbSettings:ConnectionString").Value ?? "defaultDatabaseName"; ;
            _databaseName = config.GetSection("MongoDbSettings:DatabaseName").Value ?? "defaultDatabaseName"; ;
            _collectionName = config.GetSection("MongoDbSettings:CollectionName").Value ?? "defaultDatabaseName"; ;

            var mongoClient = new MongoClient(_connectionString);
            var database = mongoClient.GetDatabase(_databaseName);
            _collection = database.GetCollection<ClientModelsMDB>("clients");
            _mapper = mapper;
        }
        #endregion
        #region GetAll
        public async Task<List<ClientModels>?> GetAll()
        {
            try
            {
                var result = await _collection.Find<ClientModelsMDB>(x => true).Limit(25).ToListAsync();
                return _mapper.Map<List<ClientModels>>(result);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("GetAll metodo no implementado.", ex);
            }
        }
        #endregion
        #region GetOne
        public async Task<ClientModels?> GetOne(string Id)
        {
            try
            {
                var filter = Builders<ClientModelsMDB>.Filter.Eq("ClientId", Id);
                var cancellationToken = new CancellationToken(); // Define e inicializa el token de cancelación
                var result = await _collection.FindAsync<ClientModelsMDB>(filter, null, cancellationToken);
                var clientMDB = result.FirstOrDefault();
                if (clientMDB != null)
                {
                    var client = new ClientModels
                    {
                        ClientId = clientMDB.ClientId,                        
                        Level = clientMDB.Level,
                        Tipo = clientMDB.Tipo,
                        Name = clientMDB.Name,
                        Lastname = clientMDB.Lastname,
                        Dni = clientMDB.Dni,
                        NroCliente = clientMDB.NroCliente,
                        Birthdate = clientMDB.Birthdate, 


                    };
                    return client;
                }
                return null;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }



        #endregion
        #region Add
        public async Task<bool> Add(ClientModels entity)
        {
            try
            {
                var client = new ClientModelsMDB
                {
                    ClientId = entity.ClientId,
                    Level = entity.Level,
                    Tipo = entity.Tipo,
                    Name = entity.Name,
                    Lastname = entity.Lastname,
                    Dni = entity.Dni,
                    NroCliente = entity.NroCliente,
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
        public async Task<bool> Upd(ClientModels entity)
        {
            try
            {
                var entityMDB = _mapper.Map<ClientModelsMDB>(entity);

                var filter = Builders<ClientModelsMDB>.Filter.Eq("ClientId", entity.ClientId);
                var update = Builders<ClientModelsMDB>.Update
                    .Set("Level", entityMDB.Level)
                    .Set("Tipo", entityMDB.Tipo)
                    .Set("Name", entityMDB.Name)
                    .Set("Lastname", entityMDB.Lastname)
                    .Set("Dni", entityMDB.Dni)
                    .Set("NroCliente", entityMDB.NroCliente)
                    .Set("Birthdate", entityMDB.Birthdate);

                var result = await _collection.UpdateOneAsync(filter, update);

                return result.ModifiedCount == 1;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        #endregion
        #region Del
        public async Task<bool> Del(string Id)
        {
            try
            {
                var filter = Builders<ClientModelsMDB>.Filter.Eq(s => s.ClientId, Id);
                var result = await _collection.DeleteOneAsync(filter);

                if (result.DeletedCount > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
