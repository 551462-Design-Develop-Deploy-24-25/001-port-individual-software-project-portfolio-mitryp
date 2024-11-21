using System.Xml;

namespace ACW1.Features.Users.Data.Entity.User;

public class Supervisor(string id, string name, string email, string hash = "")
    : User(id, name, email, hash)
{
    public override UserType UserType => UserType.Supervisor;

    public new static Supervisor Create(XmlNode medium)
    {
        var (id, name, email, hash) = ParseBase(medium);

        return new Supervisor(id, name, email, hash);
    }

    public new static Supervisor Create(List<dynamic?> data)
    {
        var (_, id, name, email, hash) = ParseBase(data);

        return new Supervisor(id, name, email, hash);
    }
}
