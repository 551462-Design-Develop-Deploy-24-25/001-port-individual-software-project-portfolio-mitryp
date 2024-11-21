using ACW1_Tests.Mocks;
using ACW1.Core.CLI.CommandReader;
using ACW1.Core.CLI.Menu;
using ACW1.Core.CLI.MenuRunner;

namespace ACW1_Tests.CliTests;

public class SequenceMenuRunnerTests
{
    [Test]
    public void SequenceMenuRunnerTest()
    {
        var menu1 = new InputMenu<int>("h", "c", int.Parse);
        var menu2 = new InputMenu<string>("h1", "c1", s => s)
        {
            Validator = s => s.Length > 1
        };

        var reader = new SequentialCommandReader(["asd", "1", "a", "abc"]);
        var sequenceRunner = new SequenceRunnerImpl(reader)
        {
            new MenuConnector<int, dynamic>(v => v, menu1),
            new MenuConnector<string, dynamic>(v => v, menu2),
        };

        var res = sequenceRunner.Run();
        Assert.AreEqual((1, "abc"), res);
    }

    private class SequenceRunnerImpl(ICommandReader reader) : SequenceMenuRunner<(int, string)>(reader)
    {
        protected override Converter<List<dynamic?>, (int, string)> Converter =>
            list => (list[0]!, list[1]!);
    }
}
