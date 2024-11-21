using ACW1_Tests.Mocks;
using ACW1.Features.Users.Data.Entity.User;
using ACW1.Features.Users.Presentation.Sequence;

namespace ACW1_Tests.FeatureTests.Users;

public class UserCreationTests
{
    [Test]
    public void TestStudentCreation()
    {
        const string name = "John Doe";
        const string email = "john.doe@example.com";
        const string personalSupervisor = "P2";
        var reader = new SequentialCommandReader([
            "1", name, email, personalSupervisor,
        ]);
        const int nextId = 1;
        var sequence = new UserCreationSequence(nextId, reader);

        var user = sequence.Run();
        Assert.That(user, Is.InstanceOf<Student>());
        var student = (Student)user;
        Assert.Multiple(() =>
        {
            Assert.That(student.Id, Is.EqualTo($"S{nextId}"));
            Assert.That(student.Name, Is.EqualTo(name));
            Assert.That(student.Email, Is.EqualTo(email));
            Assert.That(student.SupervisorId, Is.EqualTo(personalSupervisor));
        });
    }
    
    [Test]
    public void TestSupervisorCreation()
    {
        const string name = "John Doe";
        const string email = "john.doe@example.com";
        const string personalSupervisor = "P2";
        var reader = new SequentialCommandReader([
            "2", name, email, personalSupervisor,
        ]);
        const int nextId = 1;
        var sequence = new UserCreationSequence(nextId, reader);

        var user = sequence.Run();
        Assert.That(user, Is.InstanceOf<Supervisor>());
        var supervisor = (Supervisor)user;
        Assert.Multiple(() =>
        {
            Assert.That(supervisor.Id, Is.EqualTo($"P{nextId}"));
            Assert.That(supervisor.Name, Is.EqualTo(name));
            Assert.That(supervisor.Email, Is.EqualTo(email));
        });
    }
    
    [Test]
    public void TestTutorCreation()
    {
        const string name = "John Doe";
        const string email = "john.doe@example.com";
        const string personalSupervisor = "P2";
        var reader = new SequentialCommandReader([
            "3", name, email, personalSupervisor,
        ]);
        const int nextId = 1;
        var sequence = new UserCreationSequence(nextId, reader);

        var user = sequence.Run();
        Assert.That(user, Is.InstanceOf<Tutor>());
        var tutor = (Tutor)user;
        Assert.Multiple(() =>
        {
            Assert.That(tutor.Id, Is.EqualTo($"T{nextId}"));
            Assert.That(tutor.Name, Is.EqualTo(name));
            Assert.That(tutor.Email, Is.EqualTo(email));
        });
    }
    
    [Test]
    public void TestTypeOverride()
    {
        const string name = "John Doe";
        const string email = "john.doe@example.com";
        const string personalSupervisor = "P2";
        var reader = new SequentialCommandReader([
            name, email, personalSupervisor,
        ]);
        const int nextId = 1;
        var sequence = new UserCreationSequence(nextId, reader, UserType.Tutor);

        var user = sequence.Run();
        Assert.That(user, Is.InstanceOf<Tutor>());
        var tutor = (Tutor)user;
        Assert.Multiple(() =>
        {
            Assert.That(tutor.Id, Is.EqualTo($"T{nextId}"));
            Assert.That(tutor.Name, Is.EqualTo(name));
            Assert.That(tutor.Email, Is.EqualTo(email));
        });
    }
}
