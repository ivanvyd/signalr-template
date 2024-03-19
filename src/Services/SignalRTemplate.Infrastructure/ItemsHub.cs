using Microsoft.AspNetCore.SignalR;

namespace SignalRTemplate.Infrastructure;

public class ItemsHub : Hub
{
    public async Task SendItemsUpdate(string message)
    {
        await Clients.All.SendAsync("ReceiveItemsUpdate", message);
    }
}

