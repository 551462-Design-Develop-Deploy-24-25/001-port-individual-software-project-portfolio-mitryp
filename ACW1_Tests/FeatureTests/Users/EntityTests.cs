using ACW1.Features.Users.Data.Entity.User;

namespace ACW1_Tests.FeatureTests.Users;

public class EntityTests
{
    [Test]
    public void TutorSerializationTest()
    {
        const string id = "T1";
        const string name = "Tutor Name";
        const string email = "tutor@email.com";
        const string hash = "hash";
        var tutor = new Tutor(id, name, email, hash);
        var xml = tutor.Serialize();
        var parsed = User.Create(xml);

        Assert.That(parsed, Is.InstanceOf<Tutor>());
        var parsedTutor = (Tutor)parsed;

        Assert.Multiple(() =>
        {
            Assert.That(tutor.Id, Is.EqualTo(id));
            Assert.That(tutor.Name, Is.EqualTo(name));
            Assert.That(tutor.Email, Is.EqualTo(email));
            Assert.That(tutor.PasswordHash, Is.EqualTo(hash));
        });

        Assert.Multiple(() =>
        {
            Assert.That(tutor.Id, Is.EqualTo(parsedTutor.Id));
            Assert.That(tutor.Name, Is.EqualTo(parsedTutor.Name));
            Assert.That(tutor.Email, Is.EqualTo(parsedTutor.Email));
            Assert.That(tutor.PasswordHash, Is.EqualTo(parsedTutor.PasswordHash));
        });
    }

    [Test]
    public void TutorFromDataTest()
    {
        const string id = "T1";
        const string name = "Tutor Name";
        const string email = "tutor@email.com";
        const string hash = "hash";
        var data = new List<dynamic?> { UserType.Tutor, id, name, email, hash };
        var user = User.Create(data);

        Assert.That(user, Is.InstanceOf<Tutor>());
        var tutor = (Tutor)user;

        Assert.Multiple(() =>
        {
            Assert.That(tutor.Id, Is.EqualTo(id));
            Assert.That(tutor.Name, Is.EqualTo(name));
            Assert.That(tutor.Email, Is.EqualTo(email));
            Assert.That(tutor.PasswordHash, Is.EqualTo(hash));
        });
    }

    [Test]
    public void SupervisorSerializationTest()
    {
        const string id = "P1";
        const string name = "Supervisor name";
        const string email = "supervisor@email.com";
        const string hash = "hash";

        var supervisor = new Supervisor(id, name, email, hash);
        var xml = supervisor.Serialize();
        var parsed = User.Create(xml);

        Assert.That(parsed, Is.InstanceOf<Supervisor>());

        var parsedSupervisor = (Supervisor)parsed;

        Assert.Multiple(() =>
        {
            Assert.That(supervisor.Id, Is.EqualTo(id));
            Assert.That(supervisor.Name, Is.EqualTo(name));
            Assert.That(supervisor.Email, Is.EqualTo(email));
            Assert.That(supervisor.PasswordHash, Is.EqualTo(hash));
        });

        Assert.Multiple(() =>
        {
            Assert.That(supervisor.Id, Is.EqualTo(parsedSupervisor.Id));
            Assert.That(supervisor.Name, Is.EqualTo(parsedSupervisor.Name));
            Assert.That(supervisor.Email, Is.EqualTo(parsedSupervisor.Email));
            Assert.That(supervisor.PasswordHash, Is.EqualTo(parsedSupervisor.PasswordHash));
        });
    }

    [Test]
    public void SupervisorFromDataTest()
    {
        const string id = "P1";
        const string name = "Supervisor Name";
        const string email = "supervisor@email.com";
        const string hash = "hash";

        var data = new List<dynamic?> { UserType.Supervisor, id, name, email, hash };
        var user = User.Create(data);

        Assert.That(user, Is.InstanceOf<Supervisor>());
        var supervisor = (Supervisor)user;

        Assert.Multiple(() =>
        {
            Assert.That(supervisor.Id, Is.EqualTo(id));
            Assert.That(supervisor.Name, Is.EqualTo(name));
            Assert.That(supervisor.Email, Is.EqualTo(email));
            Assert.That(supervisor.PasswordHash, Is.EqualTo(hash));
        });
    }

    [Test]
    public void StudentSerializationTest()
    {
        const string id = "S1";
        const string name = "Student Name";
        const string email = "studenT@email.com";
        const string supervisor = "P1";
        const string hash = "hash";

        var student = new Student(id, name, email, supervisor, hash);
        var xml = student.Serialize();
        var parsed = User.Create(xml);

        Assert.That(parsed, Is.InstanceOf<Student>());

        var parsedStudent = (Student)parsed;

        Assert.Multiple(() =>
        {
            Assert.That(student.Id, Is.EqualTo(id));
            Assert.That(student.Name, Is.EqualTo(name));
            Assert.That(student.Email, Is.EqualTo(email));
            Assert.That(student.SupervisorId, Is.EqualTo(supervisor));
            Assert.That(student.PasswordHash, Is.EqualTo(hash));
        });

        Assert.Multiple(() =>
        {
            Assert.That(student.Id, Is.EqualTo(parsedStudent.Id));
            Assert.That(student.Name, Is.EqualTo(parsedStudent.Name));
            Assert.That(student.Email, Is.EqualTo(parsedStudent.Email));
            Assert.That(student.SupervisorId, Is.EqualTo(parsedStudent.SupervisorId));
            Assert.That(student.PasswordHash, Is.EqualTo(parsedStudent.PasswordHash));
            // todo add reports
        });
    }


    [Test]
    public void StudentFromDataTest()
    {
        const string id = "S1";
        const string name = "Student Name";
        const string email = "student@email.com";
        const string supervisor = "P1";
        const string hash = "hash";
        // todo add reports

        var data = new List<dynamic?> { UserType.Student, id, name, email, hash, supervisor };
        var user = User.Create(data);

        Assert.That(user, Is.InstanceOf<Student>());
        var student = (Student)user;

        Assert.Multiple(() =>
        {
            Assert.That(student.Id, Is.EqualTo(id));
            Assert.That(student.Name, Is.EqualTo(name));
            Assert.That(student.Email, Is.EqualTo(email));
            Assert.That(student.SupervisorId, Is.EqualTo(supervisor));
            Assert.That(student.PasswordHash, Is.EqualTo(hash));
        });
    }
}
