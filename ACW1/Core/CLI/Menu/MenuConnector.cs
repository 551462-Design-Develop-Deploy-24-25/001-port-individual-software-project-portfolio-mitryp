using System.Data;
using ACW1.Core.CLI.MenuOption;

namespace ACW1.Core.CLI.Menu;

public class MenuConnector<TBranchReturn, TReturn>(
    Converter<TBranchReturn, TReturn> converter,
    IMenu<TBranchReturn> branchMenu) : IMenu<TReturn>
{
    public string Prompt => branchMenu.Prompt;

    public override string ToString() => branchMenu.ToString();

    public MenuOption<TReturn> GetOption(string input)
    {
        var option = branchMenu.GetOption(input);

        MenuOption<TReturn>? connectorOption = null;

        option.Match(
            action => connectorOption = new MenuOption<TReturn>.MenuAction(
                action.Name,
                () =>
                {
                    action.Execute(out var res);
                    return res == null ? default : converter(res);
                },
                action.Description
            ),
            nestedMenu => connectorOption = new MenuOption<TReturn>.NestedMenu(
                nestedMenu.Name,
                new MenuConnector<TBranchReturn, TReturn>(converter, nestedMenu.Menu),
                nestedMenu.Description
            )
        );

        if (connectorOption == null)
            throw new InvalidExpressionException("Branching menu is not valid");

        return connectorOption;
    }
}
