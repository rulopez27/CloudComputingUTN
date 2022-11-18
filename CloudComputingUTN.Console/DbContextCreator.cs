using CloudComputingUTN.Middleware;

public static class DbContextCreator
{
    public static BaseDbContext DbContextFactory(DataBaseEngines databaseEngineType)
    {
        BaseDbContext baseDbContext;
        switch (databaseEngineType)
        {
            case DataBaseEngines.MySQL:
                baseDbContext = new MySQLDbContext();
                break;
            case DataBaseEngines.MSSQL:
                baseDbContext= new MSSQLDbContext();
                break;
            default:
                baseDbContext = new MySQLDbContext();
                break;
        }
        return baseDbContext;
    }
}
