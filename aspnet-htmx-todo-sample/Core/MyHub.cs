using Microsoft.AspNetCore.SignalR;

namespace AspnetHtmxTodoSample;

public interface IMyHubClient
{
    Task HtmxTrigger(string selector, string eventName);
}

public class MyHub : Hub<IMyHubClient>
{
}
