

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiNoSqlDomain.Client
{
    public class PersonModels
    {
            
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Dni { get; set; }
        [Column("ClientId")]
        public string? ClientId { get; set; }
        public System.DateTime? Birthdate { get; set; }
       
         
    }
}
