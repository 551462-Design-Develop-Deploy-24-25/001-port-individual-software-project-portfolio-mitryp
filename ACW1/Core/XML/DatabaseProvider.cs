namespace ACW1.Core.XML;

public static class DatabaseProvider
{
    public static string GetDatabasePath()
    {
        var applicationFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "DDD_ACW1"
        );

        if (!Directory.Exists(applicationFolder))
        {
            Directory.CreateDirectory(applicationFolder);
        }

        var dataPath = Path.Combine(applicationFolder, "data.xml");

        if (!File.Exists(dataPath))
        {
            File.WriteAllText(dataPath, "<?xml version=\"1.0\" encoding=\"UTF-8\"?><users></users>");
        }

        return dataPath;
    }
}
