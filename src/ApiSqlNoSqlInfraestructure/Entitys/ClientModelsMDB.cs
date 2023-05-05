using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiNoSqlInfraestructure.Entitys
{
    public class ClientModelsMDB
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }       
        public string? ClientId { get; set; }
        public int Level { get; set; }
        public string? Tipo { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Dni { get; set; }
        public string? NroCliente { get; set; }
        public System.DateTime Birthdate { get; set; }
    }
}
