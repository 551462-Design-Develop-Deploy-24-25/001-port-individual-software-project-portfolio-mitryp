using ACW1_Tests.Mocks;
using ACW1.Core.CLI.Menu;
using ACW1.Core.CLI.MenuRunner;
using static ACW1.Core.CLI.MenuOption.MenuOption<int>;

namespace ACW1_Tests.CliTests;

public class MenuRunnerTests
{
    private const int DefaultValue = -123;

    private static readonly MenuAction TopAction1 = new("a1", () => TopAction1Value);
    private const string TopAction1Key = "1";
    private const int TopAction1Value = 01;
    private static readonly MenuAction TopAction2 = new("a2", () => TopAction2Value);
    private const string TopAction2Key = "2";
    private const int TopAction2Value = 02;
    private const string Nested1MenuKey = "3";
    private static readonly MenuAction Nested1Action1 = new("n1_a1", () => Nested1Action1Value);
    private const string Nested1Action1Key = "n1_a1";
    private const int Nested1Action1Value = 11;
    private static readonly MenuAction Nested1Action2 = new("n1_a2", () => Nested1Action2Value);
    private const string Nested1Action2Key = "n1_a2";
    private const int Nested1Action2Value = 12;
    private const string Nested2MenuKey = "n1_menu";
    private static readonly MenuAction Nested2Action = new("n2_a1", () => Nested2ActionValue);
    private const string Nested2ActionKey = "n2_a1";
    private const int Nested2ActionValue = 21;

    private readonly IMenu<int> _menu = new ListMenu<int>("Test menu")
    {
        TopAction1,
        TopAction2,
        new NestedMenu(
            "nested",
            new MapMenu<int>("Nested map menu")
            {
                { Nested1Action1Key, Nested1Action1 },
                { Nested1Action2Key, Nested1Action2 },
                {
                    Nested2MenuKey, new NestedMenu(
                        "Nested nested menu",
                        new MapMenu<int>("Nested nested menu")
                        {
                            { Nested2ActionKey, Nested2Action }
                        })
                }
            }
        )
    };

    [Test]
    public void ImmediateReturnTest()
    {
        var reader = new SequentialCommandReader([MenuRunner<int>.ReturnCommand]);
        var runner = new MenuRunner<int>(_menu, commandReader: reader);

        Assert.That(runner.Run(defaultValue: DefaultValue), Is.EqualTo(DefaultValue));
    }

    [Test]
    public void TopAction1Test()
    {
        var reader = new SequentialCommandReader([TopAction1Key]);
        var runner = new MenuRunner<int>(_menu, commandReader: reader);

        Assert.That(runner.Run(defaultValue: DefaultValue), Is.EqualTo(TopAction1Value));
    }

    [Test]
    public void TopAction2Test()
    {
        var reader = new SequentialCommandReader([TopAction2Key]);
        var runner = new MenuRunner<int>(_menu, commandReader: reader);

        Assert.That(runner.Run(defaultValue: DefaultValue), Is.EqualTo(TopAction2Value));
    }

    [Test]
    public void Nested1Action1Test()
    {
        var reader = new SequentialCommandReader([Nested1MenuKey, Nested1Action1Key]);
        var runner = new MenuRunner<int>(_menu, commandReader: reader);

        Assert.That(runner.Run(defaultValue: DefaultValue), Is.EqualTo(Nested1Action1Value));
    }

    [Test]
    public void Nested1Action2Test()
    {
        var reader = new SequentialCommandReader([Nested1MenuKey, Nested1Action2Key]);
        var runner = new MenuRunner<int>(_menu, commandReader: reader);

        Assert.That(runner.Run(defaultValue: DefaultValue), Is.EqualTo(Nested1Action2Value));
    }

    [Test]
    public void Nested2ActionTest()
    {
        var reader = new SequentialCommandReader([Nested1MenuKey, Nested2MenuKey, Nested2ActionKey]);
        var runner = new MenuRunner<int>(_menu, commandReader: reader);

        Assert.That(runner.Run(defaultValue: DefaultValue), Is.EqualTo(Nested2ActionValue));
    }

    [Test]
    public void MultipleReturnsTest()
    {
        var reader = new SequentialCommandReader([
            Nested1MenuKey, MenuRunner<int>.ReturnCommand, Nested1MenuKey, Nested1MenuKey, Nested1Action2Key
        ]);
        var runner = new MenuRunner<int>(_menu, commandReader: reader);

        Assert.That(runner.Run(defaultValue: DefaultValue), Is.EqualTo(Nested1Action2Value));
    }
}
