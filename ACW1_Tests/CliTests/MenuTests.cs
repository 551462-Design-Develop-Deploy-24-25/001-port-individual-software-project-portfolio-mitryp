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
        const string name = "map menu";
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
        const string name = "map menu";
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

    [Test]
    public void InputMenuTest()
    {
        const string inputHeader = "input header";
        const string content = "input\ncontent";
        const string prompt = "Enter your value: ";

        var menu = new InputMenu<int>(
            inputHeader,
            content,
            prompt,
            int.Parse
        )
        {
            Validator = i => i is > 0 and < 5
        };

        Assert.Multiple(() =>
        {
            Assert.That(menu.ToString(), Does.StartWith($"{inputHeader}\n"));
            Assert.That(menu.ToString(), Does.Contain($"{content}\n"));
            Assert.That(menu.Prompt, Is.EqualTo(prompt));
        });

        var res = default(int);
        menu.GetOption("1").Match(action => action.Execute(out res), _ => res = default);

        Assert.Multiple(() =>
        {
            Assert.That(res, Is.EqualTo(1));
            Assert.Throws<InvalidCommandException>(() => menu.GetOption("abc"));
            Assert.Throws<InvalidCommandException>(() => menu.GetOption("10"));
            Assert.Throws<InvalidCommandException>(() => menu.GetOption("0"));
        });
    }

    [Test]
    public void MenuConnectorTest()
    {
        var inputMenu = new InputMenu<string>("input", "enter: ", s => s)
        {
            Validator = s => s.Length > 1,
        };
        var outerMenu = new MenuConnector<string, int>(int.Parse, inputMenu);

        Assert.Multiple(() =>
        {
            Assert.That(outerMenu.ToString(), Is.EqualTo(inputMenu.ToString()));
            Assert.That(outerMenu.Prompt, Is.EqualTo(inputMenu.Prompt));
        });

        Assert.Multiple(() =>
        {
            Assert.Throws<InvalidCommandException>(() => inputMenu.GetOption("1"));
            Assert.Throws<InvalidCommandException>(() => outerMenu.GetOption("1"));
        });

        const string key = "33";
        string? input = null;
        int convertedInput = default;
        inputMenu.GetOption(key).Match(action => action.Execute(out input), _ => Assert.Fail());
        outerMenu.GetOption(key).Match(action => action.Execute(out convertedInput), _ => Assert.Fail());

        if (input == null)
        {
            Assert.Fail();
            return;
        }

        Assert.That(int.Parse(input), Is.EqualTo(convertedInput));
    }
}
