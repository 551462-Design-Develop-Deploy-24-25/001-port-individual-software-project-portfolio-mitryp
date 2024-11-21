using ACW1.Core.CLI.Menu;

namespace ACW1.Core.CLI.MenuOption;

public abstract class MenuOption<TReturn>
{
    public readonly string Name;
    public readonly string? Description;

    private MenuOption(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    public class MenuAction(string name, Func<TReturn?> action, string? description = null)
        : MenuOption<TReturn>(name, description)
    {
        public override void Match(Action<MenuAction> menuAction, Action<NestedMenu> nestedMenu) =>
            menuAction(this);

        public void Execute(out TReturn? res) => res = action();
    }

    public class NestedMenu(string name, IMenu<TReturn> menu, string? description = null)
        : MenuOption<TReturn>(name, description)
    {
        public IMenu<TReturn> Menu { get; } = menu;

        public override void Match(Action<MenuAction> menuAction, Action<NestedMenu> nestedMenu) => nestedMenu(this);
    }

    public abstract void Match(Action<MenuAction> menuAction, Action<NestedMenu> nestedMenu);

    public override string ToString() => Description != null ? $"{Name} ({Description})" : Name;
}
