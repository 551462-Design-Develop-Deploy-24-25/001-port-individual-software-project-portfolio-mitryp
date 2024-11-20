using ACW1.Core.CLI.CommandReader;
using ACW1.Core.CLI.Menu;
using ACW1.Core.CLI.MenuOption;
using ACW1.Core.CLI.MenuRunner;
using ACW1.Core.System;
using static ACW1.Core.CLI.MenuOption.MenuOption<int>;

namespace ACW1;

public class Program
{
    private static readonly MenuAction TopAction1 = new("a1", () => TopAction1Value);
    private const string TopAction1Key = "1";
    private const int TopAction1Value = 01;
    private static readonly MenuAction TopAction2 = new("a2", () => TopAction2Value);
    private const string TopAction2Key = "2";
    private const int TopAction2Value = 02;
    private const string TopMenu1Key = "3";
    private static readonly MenuAction Nested1Action1 = new("n1_a1", () => Nested1Action1Value);
    private const string Nested1Action1Key = "n1_a1";
    private const int Nested1Action1Value = 11;
    private static readonly MenuAction Nested1Action2 = new("n1_a2", () => Nested1Action2Value);
    private const string Nested1Action2Key = "n1_a2";
    private const int Nested1Action2Value = 12;
    private const string Nested2MenuKey = "n1_menu";
    private static readonly MenuAction Nested2Action = new("n2_a1", () => Nested2ActionValue);
    private const string Nested2ActionKey = "n2_a1";
    private const int Nested2ActionValue = 21;
    private const string TopMenu2Key = "4";

    static private readonly IMenu<int> _menu = new ListMenu<int>("Test menu")
    {
        TopAction1,
        TopAction2,
        new NestedMenu(
            "nested map",
            new MapMenu<int>("Nested map menu")
            {
                { Nested1Action1Key, Nested1Action1 },
                { Nested1Action2Key, Nested1Action2 },
                {
                    Nested2MenuKey, new NestedMenu(
                        "Nested nested menu",
                        new MapMenu<int>("Nested nested menu")
                        {
                            { Nested2ActionKey, Nested2Action }
                        })
                }
            }
        ),
        new NestedMenu(
            "nested input",
            new InputMenu<int>(
                "nested input menu",
                "enter value: ",
                int.Parse
            )
            {
                Validator = i => i is > 0 and < 5
            }
        ),
    };
    
    static void Main(string[] args)
    {
        // var menu = new InputMenu<int>("Please enter int between 1 and 4", "Enter int: ", int.Parse)
        // {
        //     Validator = i => i is > 0 and < 5
        // };
        // var outerMenu = new ListMenu<int>("Top menu")
        // {
        //     new NestedMenu("Nested input", menu),
        //     new MenuAction("Return -1", () => -1),
        // };
        
        var runner = new MenuRunner<int>(_menu, new ConsoleCommandReader());

        var res = runner.Run();
        Console.WriteLine(res);
    }
}
