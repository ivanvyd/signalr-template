using SignalRTemplate.Domain;

namespace SignalRTemplate.WebApi.Services;

public interface IService
{
    Item CreateItem(string name);
    bool DeleteItem(int id);
    List<Item> GetItems();
}
