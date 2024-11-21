using System.Text;
using ACW1.Core.CLI;
using ACW1.Core.CLI.CommandReader;
using ACW1.Core.CLI.Menu;
using ACW1.Core.CLI.MenuOption;
using ACW1.Core.CLI.MenuRunner;
using ACW1.Features.Users.Data.Entity.User;

namespace ACW1.Features.Users.Presentation.Commands;

public class DisplayStudentProfile(ICommandReader reader) : SimpleCommand<Student, DisplayStudentProfileAction>("Display Student Profile")
{
    public override DisplayStudentProfileAction Run(Student student)
    {
        Console.WriteLine($"{student.Id}: {student.Name}");
        Console.WriteLine();

        var sb = new StringBuilder($"{student.Id}: {student.Name}\nEmail: {student.Email}\n");
        sb.AppendLine("Reports:");
        // todo reports
        sb.AppendLine("Appointments:");
        // todo appointments
        
        var selector = new ListMenu<DisplayStudentProfileAction>(sb.ToString())
        {
            new MenuOption<DisplayStudentProfileAction>.MenuAction("Book an Appointment",
                () => DisplayStudentProfileAction.BookAppointment),
        };

        var runner = new MenuRunner<DisplayStudentProfileAction>(selector, reader);
        return runner.Run(DisplayStudentProfileAction.None);
    }
}

public enum DisplayStudentProfileAction
{
    BookAppointment, None
}
