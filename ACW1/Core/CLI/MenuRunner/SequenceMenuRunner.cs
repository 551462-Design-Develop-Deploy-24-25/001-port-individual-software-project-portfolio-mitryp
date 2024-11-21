using System.Collections;
using ACW1.Core.CLI.CommandReader;
using ACW1.Core.CLI.Menu;

namespace ACW1.Core.CLI.MenuRunner;

public abstract class SequenceMenuRunner<TReturn>(ICommandReader commandReader)
    : IEnumerable
{
    protected abstract Converter<List<dynamic?>, TReturn> Converter { get; }

    protected virtual bool WillSkip(int index, List<dynamic?> results) => false;

    protected virtual bool TryGetOverride(int index, List<dynamic?> results, out dynamic? result)
    {
        result = null;
        return false;
    }

    private readonly List<IMenu<dynamic>> _menus = [];

    IEnumerator IEnumerable.GetEnumerator() => _menus.GetEnumerator();
    public void Add(IMenu<dynamic> menu) => _menus.Add(menu);
    public void Add<T>(IMenu<T> menu) => _menus.Add(new MenuConnector<T, dynamic>(i => i!, menu));

    public TReturn Run()
    {
        var results = new List<dynamic?>(_menus.Count);

        for (var i = 0; i < _menus.Count; i++)
        {
            if (TryGetOverride(i, results, out var @override))
            {
                results.Add(@override);
                continue;
            }

            if (WillSkip(i, results)) continue;

            var menu = _menus[i];
            var runner = new MenuRunner<dynamic>(menu, commandReader);

            results.Add(runner.Run());
        }

        return Converter(results);
    }
}
