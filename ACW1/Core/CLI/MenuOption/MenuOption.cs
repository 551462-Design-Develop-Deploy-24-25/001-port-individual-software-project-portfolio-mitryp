using ACW1.Core.CLI.Menu;

namespace ACW1.Core.CLI.MenuOption;

public abstract class MenuOption<TReturn>
{
    private readonly string _name;
    private readonly string? _description;

    private MenuOption(string name, string? description)
    {
        _name = name;
        _description = description;
    }

    public class MenuAction(string name, Func<TReturn?> action, string? description = null)
        : MenuOption<TReturn>(name, description)
    {
        public override void Match(Action<MenuAction> menuAction, Action<NestedMenu> nestedMenu) =>
            menuAction(this);

        public virtual void Execute(out TReturn? res) => res = action();
    }

    public class NestedMenu(string name, IMenu<TReturn> menu, string? description = null)
        : MenuOption<TReturn>(name, description)
    {
        public IMenu<TReturn> Menu { get; } = menu;
        
        public override void Match(Action<MenuAction> menuAction, Action<NestedMenu> nestedMenu) => nestedMenu(this);
    }

    public abstract void Match(Action<MenuAction> menuAction, Action<NestedMenu> nestedMenu);

    public override string ToString() => _description != null ? $"{_name} ({_description})" : _name;
}
