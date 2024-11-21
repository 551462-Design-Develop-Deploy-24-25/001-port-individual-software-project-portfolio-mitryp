using ACW1.Core.CLI.CommandReader;
using ACW1.Core.CLI.Menu;
using ACW1.Core.CLI.MenuOption;
using ACW1.Core.CLI.MenuRunner;
using ACW1.Core.System;
using static ACW1.Core.CLI.MenuOption.MenuOption<int>;

namespace ACW1;

public class Program
{
    static void Main(string[] args)
    {
        var menu = new InputMenu<string>("Please enter int between 1 and 4", "Enter int: ", s => s);
        var outerMenu = new ListMenu<int>("Top menu")
        {
            new NestedMenu("Nested input", new MenuConnector<string, int>(int.Parse, menu)),
            new MenuAction("Return -1", () => -1),
        };

        var runner = new MenuRunner<int>(outerMenu, new ConsoleCommandReader());

        var res = runner.Run();
        Console.WriteLine(res);
    }
}
