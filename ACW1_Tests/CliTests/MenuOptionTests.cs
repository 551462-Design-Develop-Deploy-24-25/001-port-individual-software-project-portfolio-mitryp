using ACW1.Core.CLI.Menu;
using ACW1.Core.CLI.MenuOption;

namespace ACW1_Tests.CliTests;

public class MenuOptionTests
{
    [Test]
    public void TestMenuActionWithoutDescription()
    {
        const string name = "Name";
        const int matcherValue = 12;

        var option = new MenuOption<int>.MenuAction(name, () => matcherValue);
        Assert.That(option.ToString(), Is.EqualTo(name));
        option.Execute(out var res);
        Assert.That(res, Is.EqualTo(matcherValue));
    }

    [Test]
    public void TestMenuActionWithDescription()
    {
        const string name = "Name";
        const string description = "Description";
        const int matcherValue = 0;

        var option = new MenuOption<int>.MenuAction(name, () => matcherValue, description);
        Assert.That(option.ToString(), Is.EqualTo($"{name} ({description})"));
        option.Execute(out var res);
        Assert.That(res, Is.EqualTo(matcherValue));
    }

    [Test]
    public void TestNestedMenuAction()
    {
        const string name = "Name";
        const string description = "Description";

        var menu = new ListMenu<int>("Menu");

        var option = new MenuOption<int>.NestedMenu(name, menu, description);
        Assert.Multiple(() =>
        {
            Assert.That(option.ToString(), Is.EqualTo($"{name} ({description})"));
            Assert.That(option.Menu, Is.SameAs(menu));
        });
    }

    [Test]
    public void TestMenuOptionMatch1()
    {
        const string name = "Name";

        var menu = new ListMenu<int>("Menu");
        MenuOption<int> option = new MenuOption<int>.NestedMenu(name, menu);

        var actionCalled = false;
        var nestedMenuCalled = false;
        option.Match(
            _ => actionCalled = true,
            _ => nestedMenuCalled = true
        );
        
        Assert.Multiple(() =>
        {
            Assert.That(actionCalled, Is.False);
            Assert.That(nestedMenuCalled, Is.True);
        });
    }

    [Test]
    public void TestMenuOptionMatch2()
    {
        const string name = "Name";
        const int matcherValue = 1;
        MenuOption<int> option = new MenuOption<int>.MenuAction(name, () => matcherValue);

        var actionCalled = false;
        var nestedMenuCalled = false;
        option.Match(
            _ => actionCalled = true,
            _ => nestedMenuCalled = true
        );
        
        Assert.Multiple(() =>
        {
            Assert.That(actionCalled, Is.True);
            Assert.That(nestedMenuCalled, Is.False);
        });
    }
}
