using ACW1.Core.CLI.Exceptions;
using ACW1.Core.CLI.MenuOption;

namespace ACW1.Core.CLI.Menu;

public class InputMenu<TReturn>(string header, string prompt, Converter<string, TReturn> converter)
    : IMenu<TReturn>
{
    public Predicate<TReturn> Validator = _ => true;
    private readonly string _content = string.Empty;

    public string Prompt { get; } = prompt;

    public InputMenu(string header, string content, string prompt, Converter<string, TReturn> converter)
        : this(header, prompt, converter)
    {
        _content = content;
    }

    public MenuOption<TReturn> GetOption(string input)
    {
        try
        {
            var converted = converter(input);
            
            if (!Validator(converted)) 
                throw new InvalidCommandException("Invalid input");
            
            return new MenuOption<TReturn>.MenuAction("", () => converted);
        }
        catch (Exception)
        {
            throw new InvalidCommandException("Invalid input format");
        }
    }

    public override string ToString()
    {
        var text = header;

        if (_content.Length > 0) text += $"\n{_content}";

        return $"{text}\n";
    }
}
