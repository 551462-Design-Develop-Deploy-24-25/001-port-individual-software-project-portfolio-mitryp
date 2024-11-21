using System.Xml;

namespace ACW1.Features.Users.Data.Entity.User;

public class Student(string id, string name, string email) : User(id, name, email)
{
    public override UserType UserType => UserType.Student;

    public override XmlNode Serialize()
    {
        // todo add reports
        var userNode = base.Serialize();

        return userNode;
    }

    public new static Student Create(XmlNode medium)
    {
        var (id, name, email) = ParseBase(medium);
        // todo add reports

        return new Student(id, name, email);
    }

    public new static Student Create(List<dynamic?> data)
    {
        var (_, id, name, email) = ParseBase(data);
        return new Student(id, name, email);
    }
}
