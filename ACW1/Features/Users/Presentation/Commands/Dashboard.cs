using ACW1.Core.CLI;
using ACW1.Core.CLI.CommandReader;
using ACW1.Core.CLI.Menu;
using ACW1.Core.CLI.MenuOption;
using ACW1.Core.CLI.MenuRunner;
using ACW1.Core.System;
using ACW1.Features.Users.Data.Entity.User;
using ACW1.Features.Users.Presentation.Sequence;

namespace ACW1.Features.Users.Presentation.Commands;

public class Dashboard(ICommandReader reader) : SimpleCommand<WellbeingSystem, int>("Dashboard")
{
    private static List<MenuOption<DashboardAction>> _studentActions =
    [
        new MenuOption<DashboardAction>.MenuAction("Submit a Report", () => DashboardAction.Report),
        new MenuOption<DashboardAction>.MenuAction("Log Out", () => DashboardAction.LogOut),
    ];

    private static List<MenuOption<DashboardAction>> _supervisorActions =
    [
        new MenuOption<DashboardAction>.MenuAction("Review Student Reports", () => DashboardAction.ReviewReports),
        new MenuOption<DashboardAction>.MenuAction("List Assigned Students",
            () => DashboardAction.ListSupervisorStudents),
        new MenuOption<DashboardAction>.MenuAction("Log Out", () => DashboardAction.LogOut),
    ];

    private static List<MenuOption<DashboardAction>> _tutorActions =
    [
        new MenuOption<DashboardAction>.MenuAction("List All Students", () => DashboardAction.ListAllStudents),
        new MenuOption<DashboardAction>.MenuAction("List Supervisors", () => DashboardAction.ListSupervisors),
        new MenuOption<DashboardAction>.MenuAction("Create User", () => DashboardAction.AddUser),
        new MenuOption<DashboardAction>.MenuAction("Log Out", () => DashboardAction.LogOut),
    ];

    public override int Run(WellbeingSystem system)
    {
        var user = system.User;
        while (user != null)
        {
            var commands = user.UserType switch
            {
                UserType.Student => _studentActions,
                UserType.Supervisor => _supervisorActions,
                UserType.Tutor => _tutorActions,
                _ => throw new ArgumentOutOfRangeException()
            };

            var menu = new ListMenu<DashboardAction>($"Logged in as {user}", commands);
            var runner = new MenuRunner<DashboardAction>(menu, reader, cancellable: false);
            var action = runner.Run(DashboardAction.None);

            switch (action)
            {
                case DashboardAction.None:
                    continue;
                case DashboardAction.LogOut:
                    system.User = null;
                    return -1;
                case DashboardAction.Report or DashboardAction.ReviewReports:
                {
                    var actionMenu = new ListMenu<dynamic>("Reports not implemented yet");
                    var actionRunner = new MenuRunner<dynamic>(actionMenu, reader);
                    actionRunner.Run();
                    break;
                }
                case DashboardAction.ListAllStudents:
                    var users = system.Users.Where(u => u is Student).Cast<Student>().ToList();
                    var student = new DisplayStudents(reader).Run(users);
                    if (student != null)
                    {
                        ProcessStudentProfileAction(new DisplayStudentProfile(reader).Run(student));
                    }

                    continue;
                case DashboardAction.ListSupervisorStudents:
                {
                    var assignedStudents = system.Users
                        .Where(u => u is Student).Cast<Student>().Where(u => u.SupervisorId == user.Id).ToList();
                    var sStudent = new DisplayStudents(reader).Run(assignedStudents);
                    if (sStudent != null)
                        ProcessStudentProfileAction(new DisplayStudentProfile(reader).Run(sStudent));
                    continue;
                }
                case DashboardAction.ListSupervisors:
                {
                    var supervisors = system.Users
                        .Where(u => u is Supervisor).Cast<Supervisor>().ToList();
                    var supervisor = new DisplaySupervisors(reader).Run(supervisors);

                    if (supervisor != null)
                        new DisplaySupervisorProfile(reader).Run(supervisor);

                    continue;
                }
                case DashboardAction.AddUser:
                {
                    var newUser = new UserCreationSequence(system.NextId, reader).Run(0);

                    if (newUser is Student student1 && !system.UserExists(student1.SupervisorId))
                    {
                        new LeafCommand(reader).Run(
                            $"No Supervisor with ID {student1.SupervisorId} exists. Please try again");
                        continue;
                    }

                    system.AddUser(newUser);
                    new LeafCommand(reader).Run($"New User id is {newUser.Id}");
                    continue;
                }
            }

            user = system.User;
        }

        return 0;
    }

    private void ProcessStudentProfileAction(DisplayStudentProfileAction action)
    {
        if (action == DisplayStudentProfileAction.None)
            return;

        new LeafCommand(reader).Run("Appointments not implemented yet");
    }
}

enum DashboardAction
{
    Report,
    ListSupervisorStudents,
    ListAllStudents,
    ListSupervisors,
    ReviewReports,
    AddUser,
    LogOut,
    None
}
