using ACW1.Core.CLI.MenuOption;

namespace ACW1.Core.CLI.Menu;

public interface IMenu<TReturn>
{
    public string Prompt { get; }
    
    public MenuOption<TReturn> GetOption(string input);

    public string ToString();
}
