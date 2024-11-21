using System.Collections;
using System.Text;
using ACW1.Core.CLI.Exceptions;
using ACW1.Core.CLI.MenuOption;

namespace ACW1.Core.CLI.Menu;

public class MapMenu<TReturn>(
    string header,
    bool caseSensitive = false) : IMenu<TReturn>, IEnumerable<KeyValuePair<string, MenuOption<TReturn>>>
{
    public string Prompt => "Enter command: ";

    private readonly Dictionary<string, MenuOption<TReturn>> _options = new();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<KeyValuePair<string, MenuOption<TReturn>>> GetEnumerator() => _options.GetEnumerator();

    public void Add(string key, MenuOption<TReturn> value)
    {
        if (!caseSensitive) key = key.ToLowerInvariant();

        _options.Add(key, value);
    }

    public virtual MenuOption<TReturn> GetOption(string input)
    {
        var optionKey = input.Trim();
        if (!caseSensitive) optionKey = optionKey.ToLowerInvariant();

        if (!_options.TryGetValue(optionKey, out var option))
            throw new InvalidCommandException("Such option does not exist");

        return option;
    }

    public override string ToString()
    {
        var sb = new StringBuilder(header).AppendLine();

        foreach (var (key, option) in _options)
        {
            sb.Append(key).Append(": ").AppendLine(option.ToString());
        }

        return sb.ToString();
    }
}
