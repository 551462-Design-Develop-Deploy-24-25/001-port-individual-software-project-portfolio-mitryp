using ACW1.Core.CLI;
using ACW1.Core.CLI.CommandReader;
using ACW1.Core.CLI.Menu;
using ACW1.Core.CLI.MenuRunner;
using ACW1.Features.Users.Data.Auth;
using ACW1.Features.Users.Data.Entity.User;

namespace ACW1.Features.Users.Presentation.Commands;

public class Login(ICommandReader reader) : SimpleCommand<List<User>, User>("Login")
{
    public override User? Run(List<User> users)
    {
        var menu = new InputMenu<string>("Please enter your ID and password", "Use the ID:PASSWORD format: ",
            s => s)
        {
            Validator = s => s.Split(':').Length == 2
        };

        var runner = new MenuRunner<string>(menu, reader, cancellable: false);

        User? user = null;
        do
        {
            var credentials = runner.Run();

            if (credentials == null)
                continue;

            var parts = credentials.Split(':', 2);
            var id = parts[0];
            var password = parts[1];
            var passwordHash = new PasswordHash().HashPassword(password);

            user = users.FirstOrDefault(u => u.Id == id && u.PasswordHash == passwordHash);
        } while (user == null);

        return user;
    }
}
