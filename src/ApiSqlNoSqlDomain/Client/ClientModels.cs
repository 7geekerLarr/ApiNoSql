using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiNoSqlDomain.Client
{
    public enum LevelClient
    {
        Mayorista=1,
        Minorista=2,
        Otro =3,
    }
    public class ClientModels
    {
        
        [BsonElement("clienteid")]
        public string? ClientId { get; set; }
        [BsonElement("level")]
        public int Level { get; set; }
        [BsonElement("tipo")]
        public string? Tipo { get; set; }
        [BsonElement("name")]
        public string? Name { get; set; }
        [BsonElement("lastname")]
        public string? Lastname { get; set; }
        [BsonElement("dni")]
        public string? Dni { get; set; }
        [BsonElement("nrocliente")]
        public string? NroCliente { get; set; }
        [BsonElement("birthdate")]
        public System.DateTime Birthdate { get; set; }

    }
}
