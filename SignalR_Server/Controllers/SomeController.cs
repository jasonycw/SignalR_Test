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
        private readonly IHubContext<SomeHub> someHub;
        public SomeController(IHubContext<SomeHub> someHub) 
            => this.someHub = someHub;

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get() 
            => new[] { "Hello", "Some API" };

        [HttpGet("{something}")]
        public async Task Get(string something) 
            => await someHub.Clients.All.SendAsync("ReceiveSomething", "Jason", something);
    }
}
