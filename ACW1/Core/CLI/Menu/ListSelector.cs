using System.Text;
using ACW1.Core.CLI.Exceptions;
using ACW1.Core.CLI.MenuOption;

namespace ACW1.Core.CLI.Menu;

public class ListSelector<TReturn>(string header, IList<TReturn> options) : IMenu<TReturn>
{
    public string Prompt => "Select option: ";
    
    public MenuOption<TReturn> GetOption(string input)
    {
        var parsed = int.TryParse(input, out var optionSelection);
        if (!parsed)
            throw new InvalidCommandException("Input must be an integer");

        var index = optionSelection - 1;
        if (index < 0 || index >= options.Count)
            throw new InvalidCommandException("Invalid index range");

        return new MenuOption<TReturn>.MenuAction("", () => options[index]);
    }

    public override string ToString()
    {
        var sb = new StringBuilder(header).AppendLine();
        for (var index = 0; index < options.Count; index++)
        {
            sb.Append(index + 1).Append(": ").AppendLine(Convert.ToString(options[index]));
        }

        return sb.ToString();
    }
}
