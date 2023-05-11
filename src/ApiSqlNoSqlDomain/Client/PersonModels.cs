 

namespace ApiNoSqlDomain.Client
{
    public class PersonModels
    {
            
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Dni { get; set; }
        public string? ClientId { get; set; }
        public System.DateTime? Birthdate { get; set; }
        public ClientModels? Client { get; set; }
    }
}
