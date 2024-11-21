using System.Xml;

namespace ACW1.Features.Users.Data.Entity.User;

public class Tutor(string id, string name, string email) : User(id, name, email)
{
    public override UserType UserType => UserType.Tutor;

    public new static Tutor Create(XmlNode medium)
    {
        var attributes = medium.Attributes!;
        var id = attributes[IdAttribute]!.Value;
        var email = attributes[EmailAttribute]!.Value;
        var name = attributes[NameAttribute]!.Value;

        return new Tutor(id, name, email);
    }
}
