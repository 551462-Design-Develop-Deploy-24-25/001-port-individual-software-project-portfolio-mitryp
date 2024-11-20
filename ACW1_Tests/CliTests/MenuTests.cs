using ACW1.Core.CLI.Exceptions;
using ACW1.Core.CLI.Menu;
using ACW1.Core.CLI.MenuOption;

namespace ACW1_Tests.CliTests;

public class MenuTests
{
    [Test]
    public void ListMenuTest()
    {
        const string name = "list menu";
        const string action1Name = "action1";
        const int action1Value = 1;
        const string action2Name = "action2";
        const int action2Value = 2;

        var option1 = new MenuOption<int>.MenuAction(action1Name, () => action1Value);
        var option2 = new MenuOption<int>.MenuAction(action2Name, () => action2Value);

        IMenu<int> menu = new ListMenu<int>(name) { option1, option2 };

        Assert.Multiple(() =>
        {
            Assert.That(menu.ToString(), Does.StartWith(name));
            Assert.That(menu.ToString(), Does.Contain($"1: {action1Name}"));
            Assert.That(menu.ToString(), Does.Contain($"2: {action2Name}"));
        });

        Assert.Multiple(() =>
        {
            Assert.DoesNotThrow(() => menu.GetOption("1"));
            Assert.DoesNotThrow(() => menu.GetOption("2"));
            Assert.Throws<InvalidCommandException>(() => menu.GetOption("3"));
            Assert.Throws<InvalidCommandException>(() => menu.GetOption("0"));
            Assert.Throws<InvalidCommandException>(() => menu.GetOption("abc"));
        });

        Assert.Multiple(() =>
        {
            Assert.That(menu.GetOption("1"), Is.SameAs(option1));
            Assert.That(menu.GetOption("2"), Is.SameAs(option2));
        });
    }

    [Test]
    public void CaseInsensitiveMapMenuTest()
    {
        const string name = "list menu";
        const string action1Name = "action1";
        const int action1Value = 1;
        const string action2Name = "action2";
        const int action2Value = 2;

        const string option1Key = "k_Action1";
        const string option2Key = "K_Action2";

        var option1 = new MenuOption<int>.MenuAction(action1Name, () => action1Value);
        var option2 = new MenuOption<int>.MenuAction(action2Name, () => action2Value);

        IMenu<int> menu = new MapMenu<int>(name)
        {
            { option1Key, option1 },
            { option2Key, option2 },
        };

        Assert.Multiple(() =>
        {
            Assert.That(menu.ToString(), Does.StartWith(name));
            Assert.That(menu.ToString(), Does.Contain($"1: {action1Name}"));
            Assert.That(menu.ToString(), Does.Contain($"2: {action2Name}"));
        });

        Assert.Multiple(() =>
        {
            Assert.DoesNotThrow(() => menu.GetOption(option1Key.ToLowerInvariant()));
            Assert.DoesNotThrow(() => menu.GetOption(option2Key.ToUpperInvariant()));
            Assert.Throws<InvalidCommandException>(() => menu.GetOption("3"));
            Assert.Throws<InvalidCommandException>(() => menu.GetOption("0"));
            Assert.Throws<InvalidCommandException>(() => menu.GetOption("abc"));
        });

        Assert.Multiple(() =>
        {
            Assert.That(menu.GetOption(option1Key.ToUpperInvariant()), Is.SameAs(option1));
            Assert.That(menu.GetOption(option2Key.ToLowerInvariant()), Is.SameAs(option2));
        });
    }
    
    [Test]
    public void CaseSensitiveMapMenuTest()
    {
        const string name = "list menu";
        const string action1Name = "action1";
        const int action1Value = 1;
        const string action2Name = "action2";
        const int action2Value = 2;

        const string option1Key = "k_Action1";
        const string option2Key = "K_actioN2";

        var option1 = new MenuOption<int>.MenuAction(action1Name, () => action1Value);
        var option2 = new MenuOption<int>.MenuAction(action2Name, () => action2Value);

        IMenu<int> menu = new MapMenu<int>(name, caseSensitive: true)
        {
            { option1Key, option1 },
            { option2Key, option2 },
        };

        Assert.Multiple(() =>
        {
            Assert.That(menu.ToString(), Does.StartWith(name));
            Assert.That(menu.ToString(), Does.Contain($"1: {action1Name}"));
            Assert.That(menu.ToString(), Does.Contain($"2: {action2Name}"));
        });

        Assert.Multiple(() =>
        {
            Assert.DoesNotThrow(() => menu.GetOption(option1Key));
            Assert.Throws<InvalidCommandException>(() => menu.GetOption(option1Key.ToLowerInvariant()));
            
            Assert.DoesNotThrow(() => menu.GetOption(option2Key));
            Assert.Throws<InvalidCommandException>(() => menu.GetOption(option2Key.ToUpperInvariant()));
            
            Assert.Throws<InvalidCommandException>(() => menu.GetOption("3"));
            Assert.Throws<InvalidCommandException>(() => menu.GetOption("0"));
            Assert.Throws<InvalidCommandException>(() => menu.GetOption("abc"));
        });

        Assert.Multiple(() =>
        {
            Assert.That(menu.GetOption(option1Key), Is.SameAs(option1));
            Assert.That(menu.GetOption(option2Key), Is.SameAs(option2));
        });
    }
}
