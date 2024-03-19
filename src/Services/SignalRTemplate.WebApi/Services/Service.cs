using SignalRTemplate.Domain;

namespace SignalRTemplate.WebApi.Services;

public sealed class Service(IRepository repository) : IService
{
    private readonly IRepository _repository = repository;

    public ValueTask<List<Item>> GetItems()
    {
        return ValueTask.FromResult(_repository.GetItems());
    }

    public ValueTask<Item> CreateItem(string name)
    {
        var items = _repository.GetItems();

        var maxId = items.MaxBy(i => i.Id)!.Id;

        var item = new Item(maxId + 1, name);

        return ValueTask.FromResult(_repository.CreateItem(item));
    }

    public ValueTask<bool> DeleteItem(int id)
    {
        return ValueTask.FromResult(_repository.DeleteItem(id));
    }
}

