using ACW1.Core.CLI.CommandReader;
using ACW1.Core.System;
using ACW1.Core.XML;
using ACW1.Features.Users.Data.Storage;

namespace ACW1;

public class Program
{
    static void Main(string[] args)
    {
        var dbPath = DatabaseProvider.GetDatabasePath();
        Console.WriteLine(dbPath);
        var storage = new UserStorage(dbPath);
        var system = new WellbeingSystem(storage, new ConsoleCommandReader());
        
        system.Initialize();
        system.Run();
    }
}
