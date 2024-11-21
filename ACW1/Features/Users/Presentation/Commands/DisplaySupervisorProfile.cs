using System.Text;
using ACW1.Core.CLI;
using ACW1.Core.CLI.CommandReader;
using ACW1.Core.CLI.Menu;
using ACW1.Core.CLI.MenuOption;
using ACW1.Core.CLI.MenuRunner;
using ACW1.Features.Users.Data.Entity.User;

namespace ACW1.Features.Users.Presentation.Commands;

public class DisplaySupervisorProfile(ICommandReader reader) : SimpleCommand<Supervisor, object?>("Display Supervisors")
{
    public override object? Run(Supervisor supervisor)
    {
        Console.WriteLine($"{supervisor.Id}: {supervisor.Name}");
        Console.WriteLine();

        var sb = new StringBuilder($"{supervisor.Id}: {supervisor.Name}\nEmail: {supervisor.Email}\n");
        // todo appointments
        sb.AppendLine("Appointments:");
        
        var selector = new ListMenu<object>(sb.ToString());

        var runner = new MenuRunner<object>(selector, reader);
        return runner.Run();
    }
}
