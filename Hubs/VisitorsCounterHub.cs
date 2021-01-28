using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class VisitorsCounterHub : Hub
    {

        static int visitorsCount = 0;
        static string connectionId = "";
        public async Task RegisterVisitorEntrance()
        {
            visitorsCount++;

            await Clients.All.SendAsync("ReceiveVisitorEntrance", visitorsCount);
        }

        public override Task OnConnectedAsync()
        {
            connectionId = Context.ConnectionId;
            return base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            visitorsCount--;

            await Clients.All.SendAsync("ReceiveVisitorEntrance", visitorsCount);

            await base.OnDisconnectedAsync(exception);
        }
    }
}