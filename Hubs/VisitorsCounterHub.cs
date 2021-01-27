using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class VisitorsCounterHub : Hub
    {

        static int visitorsCount = 0;
        public async Task RegisterVisitorEntrance()
        {
            visitorsCount++;

            await Clients.All.SendAsync("ReceiveVisitorEntrance", visitorsCount);
        }

        // public async override Task OnDisconnectedAsync(Exception exception)
        // {
        //     visitorsCount--;

        //     await Clients.All.SendAsync("ReceiveVisitorEntrance", visitorsCount);

        //     return base.OnDisconnectedAsync(exception);
        // }
    }
}