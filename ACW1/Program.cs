using ACW1.Core.CLI.CommandReader;
using ACW1.Core.CLI.Menu;
using ACW1.Core.CLI.MenuOption;
using ACW1.Core.CLI.MenuRunner;

namespace ACW1;

public class Program
{
    static void Main(string[] args)
    {
        IMenu<int> menu = new ListMenu<int>("Test menu")
        {
            new MenuOption<int>.MenuAction("Return 100", () => 100),
            new MenuOption<int>.NestedMenu(
                "Show Nested Menu",
                new MapMenu<int>("Nested Menu")
                {
                    { "cmd", new MenuOption<int>.MenuAction("Return 200", () => 200) }
                }
            ),
        };

        // Console.WriteLine(menu);
        //
        // Console.Write("Please, enter command: ");
        // var command = new ConsoleCommandReader().ReadCommand();
        // var option = menu.GetOption(command);
        //
        // var res = -1;
        // option?.Match(
        //     action => action.Execute(out res),
        //     nestedMenu => Console.WriteLine(nestedMenu)
        // );
        //
        // Console.WriteLine($"Res: {res}");

        var runner = new MenuRunner<int>(menu, new ConsoleCommandReader());
        var result = runner.Run();

        Console.WriteLine($"Result: {result}");
    }

    public static int Add(int a, int b) => a + b;
}
