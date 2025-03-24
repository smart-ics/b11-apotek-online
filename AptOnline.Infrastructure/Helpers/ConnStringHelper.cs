namespace AptOnline.Infrastructure.Helpers;

public static class ConnStringHelper
{
    private static string _connString = string.Empty;
    
    public static string Get(DatabaseOptions options)
    {
        if (_connString.Length == 0)
            _connString = Generate(options.Server, options.DbName);

        return _connString;
    }

    private static string Generate(string server, string db)
    {
        const string uid = "hospitalx";
        const string pass = "intersoftindo";
        var result = $"Server={server};Database={db};User Id={uid};Password={pass};";
        return result;
    }
}
