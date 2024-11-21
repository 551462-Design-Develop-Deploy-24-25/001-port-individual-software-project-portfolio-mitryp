using System.Xml;
using ACW1.Core.XML.Exceptions;
using ACW1.Core.XML.Interfaces;

namespace ACW1.Features.Users.Data.Entity.User;

public abstract class User(string id, string name, string email, string passwordHash) : IXmlSerializable<User>
{
    public const string IdAttribute = "id";
    public const string NameAttribute = "name";
    public const string EmailAttribute = "email";
    public const string TypeAttribute = "type";
    public const string HashAttribute = "hash";

    public string Id { get; } = id;
    public string Name { get; } = name;
    public string Email { get; } = email;
    public string PasswordHash { get; } = passwordHash;
    public abstract UserType UserType { get; }

    public virtual XmlNode Serialize()
    {
        var document = new XmlDocument();
        var rootElement = document.CreateElement("User");

        var idAttr = document.CreateAttribute(IdAttribute);
        idAttr.Value = Id;
        rootElement.Attributes.Append(idAttr);

        var typeAttr = document.CreateAttribute(TypeAttribute);
        typeAttr.Value = UserType.ToString();
        rootElement.Attributes.Append(typeAttr);

        var emailAttr = document.CreateAttribute(EmailAttribute);
        emailAttr.Value = Email;
        rootElement.Attributes.Append(emailAttr);

        var nameAttr = document.CreateAttribute(NameAttribute);
        nameAttr.Value = Name;
        rootElement.Attributes.Append(nameAttr);

        var hashAttr = document.CreateAttribute(HashAttribute);
        hashAttr.Value = PasswordHash;
        rootElement.Attributes.Append(hashAttr);

        return rootElement;
    }

    public override string ToString() => $"{Id}: {Name}";

    public static User Create(XmlNode medium)
    {
        var typeStr = medium.Attributes![TypeAttribute]!.Value;
        var typeParsed = Enum.TryParse(typeStr, out UserType type);
        if (!typeParsed)
            throw new DecodeException("User");

        return type switch
        {
            UserType.Tutor => Tutor.Create(medium),
            UserType.Supervisor => Supervisor.Create(medium),
            UserType.Student => Student.Create(medium),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static User Create(List<dynamic?> data)
    {
        UserType type = data[0]!;

        return type switch
        {
            UserType.Tutor => Tutor.Create(data),
            UserType.Supervisor => Supervisor.Create(data),
            UserType.Student => Student.Create(data),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    protected static (string Id, string Name, string Email, string Hash) ParseBase(XmlNode medium)
    {
        var attributes = medium.Attributes!;
        var id = attributes[IdAttribute]!.Value;
        var email = attributes[EmailAttribute]!.Value;
        var name = attributes[NameAttribute]!.Value;
        var hash = attributes[HashAttribute]!.Value;

        return new(id, name, email, hash);
    }

    protected static (UserType Type, string Id, string Name, string Email, string Hash) ParseBase(List<dynamic?> data)
    {
        UserType type = data[0]!;
        string id = data[1]!;
        string name = data[2]!;
        string email = data[3]!;
        string hash = data[4]!;

        return new(type, id, name, email, hash);
    }
}

public enum UserType
{
    Student,
    Supervisor,
    Tutor
}
