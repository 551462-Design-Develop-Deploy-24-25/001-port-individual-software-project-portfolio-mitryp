namespace ACW1.Core.CLI.CommandReader;

public class ConsoleCommandReader : ICommandReader
{
    public string ReadCommand() => Console.ReadLine() ?? "";
}
