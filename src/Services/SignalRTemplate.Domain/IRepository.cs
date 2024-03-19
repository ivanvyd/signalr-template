
namespace SignalRTemplate.Domain;

public interface IRepository
{
    Item CreateItem(Item item);
    bool DeleteItem(int id);
    List<Item> GetItems();
}