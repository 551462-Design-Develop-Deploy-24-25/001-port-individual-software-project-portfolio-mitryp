using ACW1_Tests.Mocks;
using ACW1.Features.Users.Data.Auth;
using ACW1.Features.Users.Data.Entity.User;
using ACW1.Features.Users.Presentation.Commands;

namespace ACW1_Tests.FeatureTests.Users;

public class LoginTests
{
    private static List<User> _users = new()
    {
        new Tutor("T1", "name1", "email@ea", new PasswordHash().HashPassword("password1")),
        new Student("S1", "name2", "email@ew", "P1", new PasswordHash().HashPassword("password2")),
        new Supervisor("P1", "name3", "email@eq", new PasswordHash().HashPassword("password3")),
    };

    [Test]
    public void TutorLoginTest()
    {
        var reader = new SequentialCommandReader([
            "asd", "login:pass", "T1:password2", "T1:password1"
        ]);
        var command = new Login(reader);

        var user = command.Run(_users);

        Assert.That(user, Is.SameAs(_users[0]));
    }

    [Test]
    public void SupervisorLoginTest()
    {
        var reader = new SequentialCommandReader([
            "asd", "login:pass", "P1:password2", "P1:password3"
        ]);
        var command = new Login(reader);

        var user = command.Run(_users);

        Assert.That(user, Is.SameAs(_users[2]));
    }

    [Test]
    public void StudentLoginTest()
    {
        var reader = new SequentialCommandReader([
            "asd", "login:pass", "S1:password1", "S1:password2"
        ]);
        var command = new Login(reader);

        var user = command.Run(_users);

        Assert.That(user, Is.SameAs(_users[1]));
    }
}
