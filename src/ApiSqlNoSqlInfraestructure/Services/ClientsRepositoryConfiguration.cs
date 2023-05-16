using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiNoSqlInfraestructure.Services
{
    public  class ClientsRepositoryConfiguration
    {
        public enum RepositoryType
        {
            SqlServer,
            MongoDB,
            CosmosDB
        }

        public RepositoryType Type { get; set; }
        public string? ConnectionString { get; set; }
    }
}
