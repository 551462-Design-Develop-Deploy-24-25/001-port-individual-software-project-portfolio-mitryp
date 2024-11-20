using System.Xml;

namespace ACW1.Core.XML.Interfaces;

public interface ISerializable<TMedium, out TSelf> where TSelf : ISerializable<TMedium, TSelf>
{
    public TMedium Serialize();

    static abstract TSelf Create(TMedium medium);
}

public interface IXmlSerializable<out TSelf> : ISerializable<XmlNode, TSelf> where TSelf : IXmlSerializable<TSelf>;
