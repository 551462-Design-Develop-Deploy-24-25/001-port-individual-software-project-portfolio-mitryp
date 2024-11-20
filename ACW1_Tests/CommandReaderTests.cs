using ACW1_Tests.Mocks;
using ACW1.Core.CLI.CommandReader;

namespace ACW1_Tests;

public class CommandReaderTests
{
    [SetUp]
    public void SetUp()
    {
    }

    [Test]
    public void ReadCommandTest()
    {
        ICommandReader reader = new ConsoleCommandReader();

        const string testCommand = "testCommand";
        Console.SetIn(new StringReader(testCommand));

        Assert.That(reader.ReadCommand(), Is.EqualTo(testCommand));
    }
    
    [Test]
    public void MockReadCommandTest()
    {
        const string testCommand = "testCommand";
        ICommandReader reader = new FixedCommandReader(testCommand);
        Assert.That(reader.ReadCommand(), Is.EqualTo(testCommand));
    }
}
