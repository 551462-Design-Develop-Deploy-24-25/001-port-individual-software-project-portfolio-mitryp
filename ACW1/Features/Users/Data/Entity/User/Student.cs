using System.Xml;

namespace ACW1.Features.Users.Data.Entity.User;

public class Student(string id, string name, string email) : User(id, name, email)
{
    public override UserType UserType => UserType.Student;

    public override XmlNode Serialize()
    {
        // todo add appointments and reports
        var userNode = base.Serialize();

        return userNode;
    }

    public new static Supervisor Create(XmlNode medium)
    {
        var (id, name, email) = ParseBase(medium);
        var ids = medium.ChildNodes.Cast<XmlNode>()
            .Select(RelatedUserId.Create)
            .Select(userId => userId.Content)
            .ToHashSet();

        return new Supervisor(id, name, email, ids);
    }
}
