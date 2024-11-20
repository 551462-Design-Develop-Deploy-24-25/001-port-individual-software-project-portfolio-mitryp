using ACW1.Core.CLI.CommandReader;
using ACW1.Core.System;

namespace ACW1_Tests.SystemTests;

public class SystemTests
{
    [Test]
    public void InitializationTest()
    {
        var system = new WellbeingSystem(new ConsoleCommandReader());

        Assert.Throws<InvalidOperationException>(() => system.Run(true));

        system.Initialize();
        Assert.DoesNotThrow(() => system.Run(true));
    }
}
