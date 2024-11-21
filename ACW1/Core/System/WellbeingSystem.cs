using ACW1.Core.CLI;
using ACW1.Core.CLI.CommandReader;
using ACW1.Core.XML.Interfaces;
using ACW1.Features.Users.Data.Entity.User;
using ACW1.Features.Users.Presentation.Commands;
using ACW1.Features.Users.Presentation.Sequence;

namespace ACW1.Core.System;

public sealed class WellbeingSystem(IStorage<User> userStorage, ICommandReader commandReader)
{
    private bool _isInitialized = false;

    internal User? User = null;
    internal List<User> Users = [];
    internal int NextId => Users.Count + 1;

    internal void Dump() => userStorage.Save(Users);

    public void Initialize()
    {
        Users = userStorage.Load();
        _isInitialized = true;
    }

    public void Run(bool dryRun = false)
    {
        if (!_isInitialized)
            throw new InvalidOperationException("System is not initialized.");

        if (dryRun) return;

        if (Users.Count == 0)
        {
            var newUser = new UserCreationSequence(1, commandReader, UserType.Tutor).Run(0);
            Users.Add(newUser);
            Dump();
            new LeafCommand(commandReader).Run($"New user id is {newUser.Id}");
        }

        var user = new Login(commandReader).Run(Users);
        User = user;

        new Dashboard(commandReader).Run(this);
    }

    public void AddUser(User user)
    {
        Users.Add(user);
        Dump();
    }
}
