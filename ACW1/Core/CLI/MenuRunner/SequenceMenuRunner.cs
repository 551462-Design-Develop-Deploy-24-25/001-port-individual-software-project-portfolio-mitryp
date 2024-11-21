using System.Collections;
using ACW1.Core.CLI.CommandReader;
using ACW1.Core.CLI.Menu;

namespace ACW1.Core.CLI.MenuRunner;

public abstract class SequenceMenuRunner<TReturn>(ICommandReader commandReader) : IEnumerable
{
    protected abstract Converter<List<dynamic?>, TReturn> Converter { get; }

    private readonly List<IMenu<dynamic>> _menus = [];

    IEnumerator IEnumerable.GetEnumerator() => _menus.GetEnumerator();
    public void Add(IMenu<dynamic> menu) => _menus.Add(menu);

    public TReturn? Run()
    {
        var results = new List<dynamic?>(_menus.Count);

        for (var i = 0; i < _menus.Count; i++)
        {
            var menu = _menus[i];
            var runner = new MenuRunner<dynamic>(menu, commandReader);

            results.Add(runner.Run());
        }

        return Converter(results);
    }
}
