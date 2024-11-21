using System.Xml;
using ACW1.Core.XML.Exceptions;
using ACW1.Core.XML.Interfaces;

namespace ACW1.Features.Users.Data.Entity.User;

public abstract class User(string id, string name, string email) : IXmlSerializable<User>
{
    public const string IdAttribute = "id";
    public const string NameAttribute = "name";
    public const string EmailAttribute = "email";
    public const string TypeAttribute = "type";

    public string Id { get; } = id;
    public string Name { get; } = name;
    public string Email { get; } = email;
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

    protected static (string Id, string Name, string Email) ParseBase(XmlNode medium)
    {
        var attributes = medium.Attributes!;
        var id = attributes[IdAttribute]!.Value;
        var email = attributes[EmailAttribute]!.Value;
        var name = attributes[NameAttribute]!.Value;

        return new(id, name, email);
    }
}

public enum UserType
{
    Student,
    Supervisor,
    Tutor
}
