using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR_Server.Hub;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalR_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        private readonly IHubContext<SomeHub, ISomeClient> _someHub;
        public SomeController(IHubContext<SomeHub, ISomeClient> someHub) 
            => _someHub = someHub;

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get() 
            => new[] { "Hello", "Some API" };

        [HttpGet("{something}")]
        public async Task Get(string something) 
            => await _someHub.Clients.All.ReceiveSomething("From Server", something);

        [HttpGet("client/{clientId}/{something}")]
        public async Task Get(string clientId, string something) 
            => await _someHub.Clients.Client(clientId).ReceiveSomething("From Server", something);

        //[Authorize] //TODO: Pass the login token to client for login
        [HttpGet("login/{clientId}")]
        public async Task Login(string clientId)
        {
            // Call other API + get token
            await _someHub.Clients.Client(clientId).Login("login_token from request header");
        }
    }
}
