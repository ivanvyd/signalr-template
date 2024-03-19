using SignalRTemplate.Domain;

namespace SignalRTemplate.WebApi.Services;

public interface IService
{
    ValueTask<Item> CreateItem(string name);
    ValueTask<bool> DeleteItem(int id);
    ValueTask<List<Item>> GetItems();
}
