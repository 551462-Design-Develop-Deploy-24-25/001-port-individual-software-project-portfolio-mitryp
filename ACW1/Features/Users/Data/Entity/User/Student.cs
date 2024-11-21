using System.Xml;

namespace ACW1.Features.Users.Data.Entity.User;

public class Student(string id, string name, string email, string supervisorId, string hash = "")
    : User(id, name, email, hash)
{
    public readonly string SupervisorId = supervisorId;
    public override UserType UserType => UserType.Student;

    public override XmlNode Serialize()
    {
        // todo add reports
        var userNode = base.Serialize();

        var supervisorAttr = userNode.OwnerDocument!.CreateAttribute("supervisor");
        supervisorAttr.Value = SupervisorId;
        userNode.Attributes!.Append(supervisorAttr);

        return userNode;
    }

    public new static Student Create(XmlNode medium)
    {
        var (id, name, email, hash) = ParseBase(medium);
        var supervisorId = medium.Attributes!.GetNamedItem("supervisor")!.Value!;
        // todo add reports

        return new Student(id, name, email, supervisorId, hash);
    }

    public new static Student Create(List<dynamic?> data)
    {
        var (_, id, name, email, hash) = ParseBase(data);
        return new Student(id, name, email, data[5]!, hash);
    }
}
