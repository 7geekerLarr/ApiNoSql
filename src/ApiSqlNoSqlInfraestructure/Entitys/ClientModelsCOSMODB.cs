using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiNoSqlInfraestructure.Entitys
{
    public class ClientModelsCOSMODB
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }
        [JsonProperty(PropertyName = "partitionKey")]
        public string? PartitionKey { get; set; }
        public string? ClientId { get; set; }
        public int Level { get; set; }
        public string? Tipo { get; set; }
        public PersonModelsCOSMODB? Person { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
