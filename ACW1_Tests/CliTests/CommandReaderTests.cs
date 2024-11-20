using ACW1_Tests.Mocks;
using ACW1.Core.CLI.CommandReader;

namespace ACW1_Tests.CliTests;

public class CommandReaderTests
{
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
        const string testCommand1 = "testCommand1";
        const string testCommand2 = "testCommand2";
        ICommandReader reader = new SequentialCommandReader([testCommand1, testCommand2]);
        Assert.That(reader.ReadCommand(), Is.EqualTo(testCommand1));
        Assert.That(reader.ReadCommand(), Is.EqualTo(testCommand2));
    }
}
