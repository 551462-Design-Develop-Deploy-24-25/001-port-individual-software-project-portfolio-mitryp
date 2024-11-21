using ACW1.Core.CLI;
using ACW1.Core.CLI.CommandReader;
using ACW1.Core.CLI.Menu;
using ACW1.Core.CLI.MenuRunner;
using ACW1.Core.System;
using ACW1.Features.Users.Data.Entity.User;

namespace ACW1.Features.Users.Presentation.Commands;

public class DisplaySupervisors(ICommandReader reader) : SimpleCommand<List<Supervisor>, Supervisor>("Supervisors List")
{
    public override Supervisor? Run(List<Supervisor> supervisors)
    {
        var menu = new ListSelector<Supervisor>("Supervisors", supervisors);
        var runner = new MenuRunner<Supervisor>(menu, reader);

        return runner.Run();
    }
}
