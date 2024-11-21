using ACW1.Core.CLI.CommandReader;
using ACW1.Core.CLI.Menu;
using ACW1.Core.CLI.MenuRunner;

namespace ACW1.Core.CLI;

public class LeafCommand(ICommandReader reader) : SimpleCommand<string, int>("Leaf")
{
    public override int Run(string content)
    {
        var menu = new ListMenu<int>(content);
        var runner = new MenuRunner<int>(menu, reader);
        runner.Run();
        
        return 0;
    }
}
