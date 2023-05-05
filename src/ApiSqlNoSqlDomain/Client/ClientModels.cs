using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiNoSqlDomain.Client
{
    public enum LevelClient
    {
        Mayorista,
        Minorista,
        Otro
    }
    public class ClientModels:PersonModels
    {
        public int id { get; set; }
        public LevelClient level { get; set; }

        public new int personid { get; set; }
        public PersonModels? Person { get; set; }
    }
}
