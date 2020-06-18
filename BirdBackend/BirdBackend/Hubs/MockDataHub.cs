using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BirdBackend.Hubs
{
    public class MockDataHub : Hub
    {
        // if you need a client to send message to all clients
        // u cant use this method.
        // if you need a Controller send a message to all client, use DI service of IHubContext<MockDataHub>
        //public async Task SendNewData(string category, int value)
        //{
        //    await Clients.All.SendAsync("UpdateData", category, value);
        //}
    }
}