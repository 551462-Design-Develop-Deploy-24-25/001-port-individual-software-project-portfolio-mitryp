using ACW1_Tests.Mocks;
using ACW1.Features.Users.Data.Entity.User;
using ACW1.Features.Users.Presentation.Commands;

namespace ACW1_Tests.FeatureTests.Users;

public class DisplayUsersTest
{
    [Test]
    public void TestDisplayStudents1()
    {
        var reader = new SequentialCommandReader(["2"]);
        var command = new DisplayStudents(reader);
        var students = new List<Student>
        {
            new("S1", "name", "name@emal.com", "P1"),
            new("S2", "name2", "name2@emal.com", "P1"),
        };

        var selection = command.Run(students);

        Assert.That(selection, Is.SameAs(students[1]));
    }
    
    [Test]
    public void TestDisplayStudents2()
    {
        var reader = new SequentialCommandReader(["q"]);
        var command = new DisplayStudents(reader);
        var students = new List<Student>
        {
            new("S1", "name", "name@emal.com", "P1"),
            new("S2", "name2", "name2@emal.com", "P1"),
        };

        var selection = command.Run(students);

        Assert.That(selection, Is.Null);
    }

    [Test]
    public void TestDisplaySupervisors1()
    {
        var reader = new SequentialCommandReader(["1"]);
        var command = new DisplaySupervisors(reader);
        var supervisors = new List<Supervisor>
        {
            new("P1", "name", "name@emal.com"),
            new("P2", "name2", "name2@emal.com"),
        };

        var selection = command.Run(supervisors);

        Assert.That(selection, Is.SameAs(supervisors[0]));
    }
    
    [Test]
    public void TestDisplaySupervisors2()
    {
        var reader = new SequentialCommandReader(["q"]);
        var command = new DisplaySupervisors(reader);
        var supervisors = new List<Supervisor>
        {
            new("P1", "name", "name@emal.com"),
            new("P2", "name2", "name2@emal.com"),
        };

        var selection = command.Run(supervisors);

        Assert.That(selection, Is.Null);
    }

    [Test]
    public void TestDisplayStudentProfile1()
    {
        var reader = new SequentialCommandReader(["1"]);
        var command = new DisplayStudentProfile(reader);
        var s = new Student("S2", "name2", "name2@emal.com", "P1");

        var selection = command.Run(s);

        Assert.That(selection, Is.EqualTo(DisplayStudentProfileAction.BookAppointment));
    }

    [Test]
    public void TestDisplayStudentProfile2()
    {
        var reader = new SequentialCommandReader(["q"]);
        var command = new DisplayStudentProfile(reader);
        var s = new Student("S2", "name2", "name2@emal.com", "P1");

        var selection = command.Run(s);

        Assert.That(selection, Is.EqualTo(DisplayStudentProfileAction.None));
    }

    [Test]
    public void TestDisplayPsProfile1()
    {
        var reader = new SequentialCommandReader(["1", "q"]);
        var command = new DisplaySupervisorProfile(reader);
        var s = new Supervisor("P2", "name", "name1@emal.com");

        var selection = command.Run(s);
        Assert.That(selection, Is.Null);
    }

    [Test]
    public void TestDisplayPsProfile2()
    {
        var reader = new SequentialCommandReader(["q"]);
        var command = new DisplaySupervisorProfile(reader);
        var s = new Supervisor("P2", "name", "name1@emal.com");

        var selection = command.Run(s);
        Assert.That(selection, Is.Null);
    }
}
