using System.Collections;
using System.Text;
using ACW1.Core.CLI.Exceptions;
using ACW1.Core.CLI.MenuOption;

namespace ACW1.Core.CLI.Menu;

public class ListMenu<TReturn>(string header) : IMenu<TReturn>, IEnumerable<MenuOption<TReturn>>
{
    private readonly List<MenuOption<TReturn>> _options = [];

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<MenuOption<TReturn>> GetEnumerator() => _options.GetEnumerator();

    public void Add(MenuOption<TReturn> option) => _options.Add(option);

    public virtual MenuOption<TReturn> GetOption(string input)
    {
        var parsed = int.TryParse(input, out var optionSelection);
        if (!parsed)
            throw new InvalidCommandException("Input must be an integer");

        var index = optionSelection - 1;
        if (index < 0 || index >= _options.Count)
            throw new InvalidCommandException("Invalid index range");

        return _options[optionSelection - 1];
    }

    public override string ToString()
    {
        var sb = new StringBuilder(header).AppendLine();

        for (var index = 0; index < _options.Count; index++)
        {
            sb.Append(index + 1).Append(": ").AppendLine(_options[index].ToString());
        }

        return sb.ToString();
    }
}
