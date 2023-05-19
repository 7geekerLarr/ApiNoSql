 

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
        public string? ClientId { get; set; }
        public LevelClient Level { get; set; }
        public string? LevelName => Enum.GetName(typeof(LevelClient), Level); 
        public string? Tipo { get; set; }        
        public PersonModels? Person { get; set; }        
    }
}
