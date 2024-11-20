namespace ACW1.Core.XML.Interfaces;

public interface IStorage<TItem>
{
    List<TItem> Load();

    void Save(IEnumerable<TItem> items);
}
