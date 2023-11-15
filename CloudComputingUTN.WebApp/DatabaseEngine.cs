namespace CloudComputingUTN.WebApp
{
    internal enum DatabaseEngines
    {
        SQLite,
        MSSQL,
        MySQL
    }

    internal enum AppEnvironments
    {
        Development,
        Production
    }
    internal class DatabaseEngine
    {
        private static DatabaseEngines _engine;
        private static AppEnvironments _environments;
        private static DatabaseEngine _instance = null;
        private DatabaseEngine()
        {
            
        }

        public static DatabaseEngine InstanceDatabaseEngine()
        {
            if (_instance == null)
            {
                _instance = new DatabaseEngine();
            }
            return _instance;
        }
        
        public void SetEngine(DatabaseEngines engine)
        {
            _engine = engine;
        }

        public DatabaseEngines GetDatabaseEngine()
        {
            return _engine;
        }

        public void SetEnvironment(AppEnvironments environments)
        {
            _environments = environments;
        }

        public AppEnvironments GetEnvironment()
        {
            return _environments;
        }
    }
}
