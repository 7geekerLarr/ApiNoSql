using ApiNoSqlDomain.Client;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiNoSqlInfraestructure.Entitys
{
    public class PersonModelsMDB
    {

        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Dni { get; set; }
        public string? ClientId { get; set; }
        public System.DateTime? Birthdate { get; set; }
        public ClientModelsMDB? Client { get; set; }
    }
}
