using System.Xml;

namespace ACW1.Features.Users.Data.Entity.User;

public class Supervisor(string id, string name, string email, HashSet<string> assignedStudents) : User(id, name, email)
{
    public HashSet<string> AssignedStudents { get; } = assignedStudents;

    public override UserType UserType => UserType.Supervisor;

    public override XmlNode Serialize()
    {
        var userNode = base.Serialize();
        var idNodes = AssignedStudents.Select(id => new RelatedUserId(id).Serialize());

        foreach (var node in idNodes)
        {
            var imported = userNode.OwnerDocument!.ImportNode(node, true);
            userNode.AppendChild(imported);
        }

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
