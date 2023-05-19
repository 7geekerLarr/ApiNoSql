using ApiNoSqlDomain.Client;
using ApiNoSqlInfraestructure.Entitys;
using ApiNoSqlInfraestructure.Services;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using SharpCompress.Common;
using System.Net;
using System.Threading;

namespace ApiNoSqlInfraestructure.Repository
{
    public class ClientsMongoRepository:IClients
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
                throw new NotImplementedException("GetAll method is not implemented.", ex);
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
                        Level = (LevelClient)clientMDB.Level,
                        Tipo = clientMDB.Tipo,
                        Person = clientMDB.Person != null ? new PersonModels
                        {
                            Name = clientMDB.Person.Name,
                            Lastname = clientMDB.Person.Lastname,
                            Dni = clientMDB.Person.Dni,
                            ClientId = clientMDB.ClientId,
                            Birthdate = clientMDB.Person.Birthdate
                        } : null
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
                    Level = (int)entity.Level,
                    Tipo = entity.Tipo,
                    Person = new PersonModelsMDB
                    {
                        Name = entity.Person?.Name,
                        Lastname = entity.Person?.Lastname,
                        Dni = entity.Person?.Dni,
                        ClientId = entity.Person?.ClientId,
                        Birthdate = entity.Person?.Birthdate ?? DateTime.MinValue
                    }
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
                var entityMDB = new ClientModelsMDB
                {
                    ClientId = entity.ClientId,
                    Level = (int)entity.Level,
                    Tipo = entity.Tipo,
                    Person = new PersonModelsMDB()
                };

                if (entity.Person != null)
                {
                    entityMDB.Person.Name = entity.Person.Name;
                    entityMDB.Person.Lastname = entity.Person.Lastname;
                    entityMDB.Person.Dni = entity.Person.Dni;
                    entityMDB.Person.ClientId = entity.ClientId;  
                    entityMDB.Person.Birthdate = entity.Person.Birthdate ?? DateTime.MinValue;
                }

                var filter = Builders<ClientModelsMDB>.Filter.Eq("ClientId", entityMDB.ClientId);
                var update = Builders<ClientModelsMDB>.Update
                    .Set("Level", entityMDB.Level)
                    .Set("Tipo", entityMDB.Tipo)
                    .Set("Person.Name", entityMDB.Person.Name)
                    .Set("Person.Lastname", entityMDB.Person.Lastname)
                    .Set("Person.Dni", entityMDB.Person.Dni)
                    .Set("Person.ClientId", entityMDB.Person.ClientId)
                    .Set("Person.Birthdate", entityMDB.Person.Birthdate);

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
