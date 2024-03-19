using Microsoft.AspNetCore.SignalR;

namespace SignalRTemplate.Infrastructure;

public sealed class ItemHub : Hub
{
    public const string HubUrl = "/item";
}
