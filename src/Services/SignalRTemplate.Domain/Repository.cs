namespace SignalRTemplate.Domain;

public sealed class Repository : IRepository
{
    private readonly List<Item> _items;

    public Repository()
    {
        _items = [];
        for (int i = 1; i <= 5; i++)
        {
            _items.Add(new Item(i, $"Item {i}"));
        }
    }

    public List<Item> GetItems()
    {
        return _items;
    }

    public Item CreateItem(Item item)
    {
        _items.Add(item);
        return item;
    }

    public bool DeleteItem(int id)
    {
        var item = _items.Find(i => i.Id == id);
        if (item != null)
        {
            _items.Remove(item);
            return true;
        }
        return false;
    }
}

public sealed record Item(int Id, string Name);