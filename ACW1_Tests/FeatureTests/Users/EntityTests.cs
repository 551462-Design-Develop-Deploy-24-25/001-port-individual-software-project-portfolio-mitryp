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
}
