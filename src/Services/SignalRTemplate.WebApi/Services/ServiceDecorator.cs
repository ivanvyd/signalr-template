using Microsoft.AspNetCore.SignalR;
using SignalRTemplate.Domain;
using SignalRTemplate.Infrastructure;

namespace SignalRTemplate.WebApi.Services;

public sealed class ServiceDecorator(IService service, IHubContext<ItemsHub> hubContext) : IService
{
    private readonly IService _service = service;
    private readonly IHubContext<ItemsHub> _hubContext = hubContext;

    public async ValueTask<Item> CreateItem(string name)
    {
        var item = await _service.CreateItem(name);
        
        await _hubContext.Clients.All.SendAsync("ReceiveItemsUpdate", "An item was created.");

        return item;
    }

    public async ValueTask<bool> DeleteItem(int id)
    {
        var result = await _service.DeleteItem(id);

        if (result)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveItemsUpdate", $"An item {id} was deleted.");
        }

        return result;
    }

    public ValueTask<List<Item>> GetItems() => _service.GetItems();
}
