using ACW1.Core.CLI.CommandReader;
using ACW1.Core.CLI.Menu;
using ACW1.Core.CLI.MenuRunner;
using ACW1.Features.Users.Data.Entity.User;

namespace ACW1.Features.Users.Presentation.Sequence;

public class UserCreationSequence : SequenceMenuRunner<dynamic, User>
{
    private readonly int _nextId;
    private readonly UserType? _typeOverride;

    public override string CommandName => "Create User";

    // todo check supervisor ids?
    public UserCreationSequence(int nextId, ICommandReader commandReader, UserType? typeOverride = null)
        : base(commandReader)
    {
        _nextId = nextId;
        _typeOverride = typeOverride;

        var typeSelector =
            new ListSelector<UserType>("Select a type of user to create",
                Enum.GetValues<UserType>().ToList()
            );

        // will be skipped
        var idSelector = new ListMenu<int>("User ID");
        var nameSelector = new InputMenu<string>("User Full Name", "Input the user full name: ", s => s)
        {
            Validator = s => s.Split(' ').Length > 1
        };
        var emailSelector = new InputMenu<string>("User Email", "Input the user email: ", s => s)
        {
            Validator = s => s.Split('@').Length == 2
        };

        var supervisorIdSelector =
            new InputMenu<string>("Supervisor ID", "Input a valid supervisor ID for this student: ", s => s)
            {
                Validator = s => s.StartsWith('P'),
            };

        Add(typeSelector);
        Add(idSelector);
        Add(nameSelector);
        Add(emailSelector);
        Add(supervisorIdSelector);
    }

    protected override bool TryGetOverride(int index, List<dynamic?> results, out dynamic? result)
    {
        if (index == 0 && _typeOverride != null)
        {
            result = _typeOverride;
            return true;
        }
        
        // override the user id with the pre-defined value
        if (index != 1)
        {
            result = null;
            return false;
        }

        UserType type = results[0]!;
        var @override = type switch
        {
            UserType.Student => $"S{_nextId}",
            UserType.Supervisor => $"P{_nextId}",
            UserType.Tutor => $"T{_nextId}",
            _ => throw new ArgumentOutOfRangeException()
        };

        result = @override;
        return true;
    }

    protected override bool WillSkip(int index, List<dynamic?> results)
    {
        // todo remove student when reviews are added
        return results.Count >= 4 && (UserType)results[0]! is UserType.Tutor or UserType.Supervisor;
    }

    protected override Converter<List<dynamic?>, User> Converter => User.Create;
}
