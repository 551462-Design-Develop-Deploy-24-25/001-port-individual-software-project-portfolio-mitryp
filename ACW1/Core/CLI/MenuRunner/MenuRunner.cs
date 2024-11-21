using ACW1.Core.CLI.CommandReader;
using ACW1.Core.CLI.Exceptions;
using ACW1.Core.CLI.Menu;
using ACW1.Core.CLI.MenuOption;

namespace ACW1.Core.CLI.MenuRunner;

public class MenuRunner<TReturn>
{
    public const string ReturnCommand = "q";
    
    private readonly Stack<IMenu<TReturn>> _menuHistory = new();
    private readonly ICommandReader _commandReader;

    public MenuRunner(IMenu<TReturn> menu, ICommandReader commandReader)
    {
        _commandReader = commandReader;
        _menuHistory.Push(menu);
    }

    public TReturn? Run(TReturn defaultValue = default(TReturn?))
    {
        while (true)
        {
            var hasMenu = _menuHistory.TryPeek(out var menu);
            if (!hasMenu || menu == null) return defaultValue;

            Console.Clear();
            Console.Write(menu);
            Console.WriteLine($"{ReturnCommand}: Return back");
            Console.Write(menu.Prompt);

            MenuOption<TReturn>? option = null;
            do
            {
                try
                {
                    var input = _commandReader.ReadCommand().Trim();

                    if (input == ReturnCommand)
                    {
                        _menuHistory.Pop();
                        return Run(defaultValue: defaultValue);
                    }

                    option = menu.GetOption(input);
                }
                catch (InvalidCommandException e)
                {
                    Console.Write($"{e.Message}. Please, try again: ");
                }
            } while (option == null);

            var result = defaultValue;
            option.Match(action => action.Execute(out result), nestedMenu =>
            {
                _menuHistory.Push(nestedMenu.Menu);
                result = Run(defaultValue: defaultValue);
            });

            if (result != null) return result;

            _menuHistory.Pop();
        }
    }
}
