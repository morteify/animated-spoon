using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Linq;

namespace SignalRChat.Hubs
{
    public class ProductGroupHub : Hub
    {

        internal static ConcurrentDictionary<string, string> CurrentVisits = new ConcurrentDictionary<string, string>();
        internal static ConcurrentDictionary<string, int> VisitorsCounts = new ConcurrentDictionary<string, int>();

        public async Task AddToGroup(string groupName)
        {
            if (!VisitorsCounts.Keys.Any(x => x == groupName))
            {
                VisitorsCounts.TryAdd(groupName, 1);
            }
            else
            {
                VisitorsCounts.TryAdd(groupName, VisitorsCounts[groupName]++);
            }

            if (!CurrentVisits.Keys.Any(x => x == Context.ConnectionId))
            {
                CurrentVisits.TryAdd(Context.ConnectionId, groupName);
            }


            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("ReceiveProductEntrance", VisitorsCounts[groupName], groupName, Context.ConnectionId);
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            var currentGroupName = CurrentVisits[Context.ConnectionId];
            VisitorsCounts.TryAdd(currentGroupName, VisitorsCounts[currentGroupName]--);
            await Clients.Group(currentGroupName).SendAsync("ReceiveProductEntrance", VisitorsCounts[currentGroupName], currentGroupName, Context.ConnectionId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, currentGroupName);
            await base.OnDisconnectedAsync(exception);
        }
    }
}