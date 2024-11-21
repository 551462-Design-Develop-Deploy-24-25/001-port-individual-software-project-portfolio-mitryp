using System.Xml;
using ACW1.Core.XML.Exceptions;
using ACW1.Core.XML.Interfaces;

namespace ACW1.Core.XML;

public abstract class XmlFileStorageBase<TSerializable>(string file, string collectionName)
    : IStorage<TSerializable>
    where TSerializable : IXmlSerializable<TSerializable>
{
    protected string FilePath { get; } = file;
    protected string CollectionName { get; } = collectionName;

    private void ThrowValidationException(string message) =>
        throw new CollectionValidationException(CollectionName, message);

    private void EnsureElementsCorrect(IEnumerable<TSerializable> elements)
    {
        if (!elements.All(IsValidElement))
        {
            ThrowValidationException("Invalid element");
        }
    }

    protected virtual bool IsValidElement(TSerializable element) => true;

    public List<TSerializable> Load()
    {
        var document = new XmlDocument();
        document.Load(FilePath);

        var root = document.DocumentElement;

        if (root == null)
        {
            ThrowValidationException("No root element");
            return [];
        }

        var elements = root.ChildNodes.Cast<XmlNode>().Select(TSerializable.Create).ToList();

        EnsureElementsCorrect(elements);

        return elements;
    }

    public void Save(IEnumerable<TSerializable> items)
    {
        var elements = items.ToList();

        EnsureElementsCorrect(elements);

        var document = new XmlDocument();
        var collectionElement = document.CreateElement(CollectionName);

        var serializedItems = elements.Select(serializable => serializable.Serialize());
        foreach (var item in serializedItems)
        {
            var imported = document.ImportNode(item, true);
            collectionElement.AppendChild(imported);
        }

        using var writer = XmlWriter.Create(FilePath, new XmlWriterSettings { Indent = true });
        collectionElement.WriteTo(writer);
    }
}
