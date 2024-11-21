using System.Xml;
using ACW1.Core.XML.Exceptions;
using ACW1.Core.XML.Interfaces;

namespace ACW1.Features.Users.Data.Entity.User;

internal class RelatedUserId(string content) : IXmlSerializable<RelatedUserId>
{
    private const string NodeName = "UserId";
    public readonly string Content = content;
    
    public XmlNode Serialize()
    {
        var document = new XmlDocument();
        var root = document.CreateElement(NodeName);
        root.InnerText = Content;

        return root;
    }

    public static RelatedUserId Create(XmlNode medium)
    {
        var content = medium.InnerText;
        if (medium.Name != NodeName || content.Length == 0)
            throw new DecodeException("UserId");

        return new RelatedUserId(content);
    }
}
