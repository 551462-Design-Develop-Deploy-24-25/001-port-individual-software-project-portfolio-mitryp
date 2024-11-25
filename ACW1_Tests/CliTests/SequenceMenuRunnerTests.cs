using ACW1_Tests.Mocks;
using ACW1.Core.CLI.CommandReader;
using ACW1.Core.CLI.Menu;
using ACW1.Core.CLI.MenuRunner;
using ACW1.Core.System;

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
        var res = sequenceRunner.Run(0);
        Assert.That(res, Is.EqualTo((1, "abc")));
    }

    private class SequenceRunnerImpl(ICommandReader reader) : SequenceMenuRunner<dynamic, (int, string)>(reader, false)
    {
        public override string CommandName => "";

        protected override Converter<List<dynamic?>, (int, string)> Converter =>
            list => (list[0]!, list[1]!);
    }
}
