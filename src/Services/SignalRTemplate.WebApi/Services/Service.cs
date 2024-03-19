using SignalRTemplate.Domain;

namespace SignalRTemplate.WebApi.Services;

public sealed class Service(IRepository repository) : IService
{
    private readonly IRepository _repository = repository;

    public List<Item> GetItems()
    {
        return _repository.GetItems();
    }

    public Item CreateItem(string name)
    {
        var items = _repository.GetItems();

        var maxId = items.MaxBy(i => i.Id)!.Id;

        var item = new Item(maxId + 1, name);

        return _repository.CreateItem(item);
    }

    public bool DeleteItem(int id)
    {
        return _repository.DeleteItem(id);
    }
}

