using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalR_Server.Hub
{
    public class OtherHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IHubContext<SomeHub, ISomeClient> _someHub;
        public OtherHub(IHubContext<SomeHub, ISomeClient> someHub) => _someHub = someHub;

        public async Task Login(string connectionId)
            => await _someHub.Clients.Client(connectionId).Login($"{Context.ConnectionId} tell you to login");
    }
}
