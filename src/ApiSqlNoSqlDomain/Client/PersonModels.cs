using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiNoSqlDomain.Client
{
    public class PersonModels
    {
        public int personid { get; set; }
        public string? name { get; set; }
        public string? lastname { get; set; }
        public int dni { get; set; }
        public System.DateTime birthdate { get; set; }

        public List<ClientModels>? Clients { get; set; }

    }
}
