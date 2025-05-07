namespace DatabaseComparer;

public class AppSettings
{
    public DatabaseConfig DatabaseA { get; set; }
    public DatabaseConfig DatabaseB { get; set; }
    public List<string> TablesToCompare { get; set; }
}

public class DatabaseConfig
{
    public string ConnectionString { get; set; }
}

public class Difference
{
    public string Key { get; set; }
    public string Column { get; set; }
    public string ValueA { get; set; }
    public string ValueB { get; set; }
}

public class TableComparisonResult
{
    public List<Difference> Differences { get; set; } = new();
    public List<string> AddedKeys { get; set; } = new();
    public List<string> DeletedKeys { get; set; } = new();
}
