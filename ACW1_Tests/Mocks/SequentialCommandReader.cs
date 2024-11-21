using ACW1.Core.CLI.CommandReader;

namespace ACW1_Tests.Mocks;

public class SequentialCommandReader(List<string> commands) : ICommandReader
{
    private int _reads = 0;
    
    public string ReadCommand() => commands[_reads++];
}
