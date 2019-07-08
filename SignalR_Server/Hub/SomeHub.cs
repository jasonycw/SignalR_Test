using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalR_Server.Hub
{
    public class SomeHub : Hub<ISomeClient>
    {
        public async Task SendSomethingFromClient(string user, string message)
            => await Clients.Others.ReceiveSomething($"{user} ({Context.ConnectionId})", message);

        public override async Task OnConnectedAsync()
        {
            // Just test sending message to client
            await Clients.All.ReceiveSomething("Notification", $"{Context.ConnectionId} is connected");
            await Clients.Client(Context.ConnectionId).ReceiveSomething("Friendly neighbourhood", $"Call `http://localhost:11978/api/some/client/{Context.ConnectionId}/type_something`");

            // Generate QR Code to client for all connections
            var url = $"http://localhost:11978/api/some/login/{Context.ConnectionId}";
            await Clients.Client(Context.ConnectionId).GetQrCode(url, QrCodeHelper.Generate(url));
        }

        public override async Task OnDisconnectedAsync(Exception exception) 
            => await Clients.Others.ReceiveSomething("From Server", $"{Context.ConnectionId} has disconnected");
    }

    // These are the event that client can received
    public interface ISomeClient
    {
        Task ReceiveSomething(string user, string message);
        Task GetQrCode(string url, string data);
        Task Login(string token);
    }
}
