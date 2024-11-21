using ACW1.Core.CLI;
using ACW1.Core.CLI.CommandReader;
using ACW1.Core.CLI.Menu;
using ACW1.Core.CLI.MenuRunner;
using ACW1.Core.System;
using ACW1.Features.Users.Data.Entity.User;

namespace ACW1.Features.Users.Presentation.Commands;

public class DisplayStudents(ICommandReader reader) : SimpleCommand<List<Student>, Student>("Students List")
{
    public override Student? Run(List<Student> students)
    {
        // todo cache?
        var menu = new ListSelector<Student>("Students", students);
        var runner = new MenuRunner<Student>(menu, reader);

        return runner.Run();
    }
}
