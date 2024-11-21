using ACW1.Core.CLI.CommandReader;
using ACW1.Core.System;
using ACW1.Core.XML;
using ACW1.Features.Users.Data.Storage;

namespace ACW1_Tests.SystemTests;

public class SystemTests
{
    [Test]
    public void InitializationTest()
    {
        var system = new WellbeingSystem(new UserStorage(DatabaseProvider.GetDatabasePath()), new ConsoleCommandReader());

        Assert.Throws<InvalidOperationException>(() => system.Run(true));

        system.Initialize();
        Assert.DoesNotThrow(() => system.Run(true));
    }
}
