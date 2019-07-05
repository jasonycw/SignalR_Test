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
            await Clients.All.ReceiveSomething("Notification", $"{Context.ConnectionId} is connected");
            await Clients.Client(Context.ConnectionId).ReceiveSomething("Friendly neighbourhood", $"Call `http://localhost:11978/api/some/client/{Context.ConnectionId}/type_something`");
            var url = $"http://localhost:11978/api/some/client/{Context.ConnectionId}/login";
            await Clients.Client(Context.ConnectionId).GetQrCode(url, QrCodeHelper.Generate(url));
        }
    }

    public interface ISomeClient
    {
        Task ReceiveSomething(string user, string message);

        Task GetQrCode(string url, string data);
    }
}
