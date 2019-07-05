using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalR_Server.Hub
{
    public class SomeHub : Hub<ISomeClient>
    {
        public async Task SendSomething(string user, string message)
            => await Clients.All.ReceiveSomething(user, message);

        public override async Task OnConnectedAsync() 
            => await Clients.Client(Context.ConnectionId).ReceiveSomething(Context.ConnectionId, "Connected");
    }

    public interface ISomeClient
    {
        Task ReceiveSomething(string user, string message);
    }
}
