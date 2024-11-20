using ACW1.Core.CLI.CommandReader;

namespace ACW1_Tests.Mocks;

public class FixedCommandReader(string output) : ICommandReader
{
    public string ReadCommand() => output;
}
