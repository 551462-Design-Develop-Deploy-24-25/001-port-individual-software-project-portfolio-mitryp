using System.Xml;

namespace ACW1.Features.Users.Data.Entity.User;

public class Tutor(string id, string name, string email, string hash = "") : User(id, name, email, hash)
{
    public override UserType UserType => UserType.Tutor;

    public new static Tutor Create(XmlNode medium)
    {
        var (id, name, email, hash) = ParseBase(medium);
        return new Tutor(id, name, email, hash);
    }

    public new static Tutor Create(List<dynamic?> data)
    {
        var (_, id, name, email, hash) = ParseBase(data);
        return new Tutor(id, name, email, hash);
    }
}
