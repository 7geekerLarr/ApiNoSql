using ApiNoSqlDomain.Client;

namespace ApiNoSqlApplication.Helper
{
    public class HelperLevel
    {
        private readonly LevelClient levelTipo;

        public HelperLevel(int level)
        {
            levelTipo = (LevelClient)level;
        }

        public LevelClient GetLevelTipo()
        {
            return levelTipo;
        }
    }
}
