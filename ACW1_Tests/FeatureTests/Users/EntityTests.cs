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
        var parsed = Tutor.Create(xml);

        Assert.Multiple(() =>
        {
            Assert.AreEqual(id, tutor.Id);
            Assert.AreEqual(name, tutor.Name);
            Assert.AreEqual(email, tutor.Email);
        });

        Assert.Multiple(() =>
        {
            Assert.AreEqual(parsed.Id, tutor.Id);
            Assert.AreEqual(parsed.Name, tutor.Name);
            Assert.AreEqual(parsed.Email, tutor.Email);
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
        
        var tutor = new Supervisor(id, name, email, ids);
        var xml = tutor.Serialize();
        var parsed = Supervisor.Create(xml);

        Assert.Multiple(() =>
        {
            Assert.AreEqual(id, tutor.Id);
            Assert.AreEqual(name, tutor.Name);
            Assert.AreEqual(email, tutor.Email);
            Assert.AreSame(ids, tutor.AssignedStudents);
        });

        Assert.Multiple(() =>
        {
            Assert.AreEqual(parsed.Id, tutor.Id);
            Assert.AreEqual(parsed.Name, tutor.Name);
            Assert.AreEqual(parsed.Email, tutor.Email);
            CollectionAssert.AreEquivalent(parsed.AssignedStudents, tutor.AssignedStudents);
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
        var parsed = Student.Create(xml);

        Assert.Multiple(() =>
        {
            Assert.AreEqual(id, student.Id);
            Assert.AreEqual(name, student.Name);
            Assert.AreEqual(email, student.Email);
        });

        Assert.Multiple(() =>
        {
            Assert.AreEqual(parsed.Id, student.Id);
            Assert.AreEqual(parsed.Name, student.Name);
            Assert.AreEqual(parsed.Email, student.Email);
        });
    }
}
