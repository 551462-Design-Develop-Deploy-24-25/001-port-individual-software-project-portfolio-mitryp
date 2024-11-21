using ACW1.Core.CLI.CommandReader;
using ACW1.Core.CLI.Menu;
using ACW1.Core.CLI.MenuOption;
using ACW1.Core.CLI.MenuRunner;
using ACW1.Core.System;
using ACW1.Features.Users.Data.Entity.User;
using ACW1.Features.Users.Presentation.Sequence;
using static ACW1.Core.CLI.MenuOption.MenuOption<int>;

namespace ACW1;

public class Program
{
    static void Main(string[] args)
    {
        var userCreation = new UserCreationSequence(1, new ConsoleCommandReader());
        var user = userCreation.Run(new WellbeingSystem(new ConsoleCommandReader()));
        
        Console.WriteLine(user);
    }
}
