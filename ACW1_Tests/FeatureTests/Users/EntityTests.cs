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
        var tutor = new Tutor(id, name, email);
        var xml = tutor.Serialize();
        var parsed = User.Create(xml);

        Assert.That(parsed, Is.InstanceOf<Tutor>());
        var parsedTutor = (Tutor)parsed;

        Assert.Multiple(() =>
        {
            Assert.AreEqual(id, tutor.Id);
            Assert.AreEqual(name, tutor.Name);
            Assert.AreEqual(email, tutor.Email);
        });

        Assert.Multiple(() =>
        {
            Assert.AreEqual(parsedTutor.Id, tutor.Id);
            Assert.AreEqual(parsedTutor.Name, tutor.Name);
            Assert.AreEqual(parsedTutor.Email, tutor.Email);
        });
    }

    [Test]
    public void TutorFromDataTest()
    {
        const string id = "T1";
        const string name = "Tutor Name";
        const string email = "tutor@email.com";
        var data = new List<dynamic?> { UserType.Tutor, id, name, email };
        var user = User.Create(data);

        Assert.That(user, Is.InstanceOf<Tutor>());
        var tutor = (Tutor)user;

        Assert.Multiple(() =>
        {
            Assert.AreEqual(id, tutor.Id);
            Assert.AreEqual(name, tutor.Name);
            Assert.AreEqual(email, tutor.Email);
        });
    }

    [Test]
    public void SupervisorSerializationTest()
    {
        const string id = "P1";
        const string name = "Supervisor name";
        const string email = "supervisor@email.com";
        var ids = new HashSet<string>
        {
            "S1",
            "S5",
            "S33"
        };

        var supervisor = new Supervisor(id, name, email, ids);
        var xml = supervisor.Serialize();
        var parsed = User.Create(xml);

        Assert.That(parsed, Is.InstanceOf<Supervisor>());

        var parsedSupervisor = (Supervisor)parsed;

        Assert.Multiple(() =>
        {
            Assert.AreEqual(id, supervisor.Id);
            Assert.AreEqual(name, supervisor.Name);
            Assert.AreEqual(email, supervisor.Email);
            Assert.AreSame(ids, supervisor.AssignedStudents);
        });

        Assert.Multiple(() =>
        {
            Assert.AreEqual(parsedSupervisor.Id, supervisor.Id);
            Assert.AreEqual(parsedSupervisor.Name, supervisor.Name);
            Assert.AreEqual(parsedSupervisor.Email, supervisor.Email);
            CollectionAssert.AreEquivalent(parsedSupervisor.AssignedStudents, supervisor.AssignedStudents);
        });
    }

    [Test]
    public void SupervisorFromDataTest()
    {
        const string id = "P1";
        const string name = "Supervisor Name";
        const string email = "supervisor@email.com";
        var ids = new HashSet<string>
        {
            "S1",
            "S8"
        };

        var data = new List<dynamic?> { UserType.Supervisor, id, name, email, ids };
        var user = User.Create(data);

        Assert.That(user, Is.InstanceOf<Supervisor>());
        var supervisor = (Supervisor)user;

        Assert.Multiple(() =>
        {
            Assert.AreEqual(id, supervisor.Id);
            Assert.AreEqual(name, supervisor.Name);
            Assert.AreEqual(email, supervisor.Email);
            CollectionAssert.AreEqual(ids, supervisor.AssignedStudents);
        });
    }

    [Test]
    public void StudentSerializationTest()
    {
        const string id = "S1";
        const string name = "Student Name";
        const string email = "studenT@email.com";
        var student = new Student(id, name, email);
        var xml = student.Serialize();
        var parsed = User.Create(xml);

        Assert.That(parsed, Is.InstanceOf<Student>());

        var parsedStudent = (Student)parsed;

        Assert.Multiple(() =>
        {
            Assert.AreEqual(id, student.Id);
            Assert.AreEqual(name, student.Name);
            Assert.AreEqual(email, student.Email);
        });

        Assert.Multiple(() =>
        {
            Assert.AreEqual(parsedStudent.Id, student.Id);
            Assert.AreEqual(parsedStudent.Name, student.Name);
            Assert.AreEqual(parsedStudent.Email, student.Email);
            // todo add reports
        });
    }


    [Test]
    public void StudentFromDataTest()
    {
        const string id = "S1";
        const string name = "Student Name";
        const string email = "student@email.com";
        // todo add reports

        var data = new List<dynamic?> { UserType.Student, id, name, email };
        var user = User.Create(data);

        Assert.That(user, Is.InstanceOf<Student>());
        var supervisor = (Student)user;

        Assert.Multiple(() =>
        {
            Assert.AreEqual(id, supervisor.Id);
            Assert.AreEqual(name, supervisor.Name);
            Assert.AreEqual(email, supervisor.Email);
        });
    }
}
