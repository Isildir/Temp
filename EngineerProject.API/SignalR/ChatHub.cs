using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineerProject.API.SignalR
{
    public class ChatHub : Hub
    {
        private readonly Dictionary<string, int> dictionary = new Dictionary<string, int>();

        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;

            var httpContext = Context.GetHttpContext();
            var characterId = httpContext.Request.Query["characterId"];

            if (int.TryParse(characterId, out int value))
                dictionary.Add(connectionId, value);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;

            if (dictionary.ContainsKey(connectionId))
                dictionary.Remove(connectionId);

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(int characterId, string message)
        {
            if (GetReceivers(characterId, out IClientProxy clients))
                await clients.SendAsync("sendMessage", message);
        }

        private bool GetReceivers(int id, out IClientProxy clientProxy)
        {
            var connectionsIds = dictionary.Where(a => a.Value == id).Select(a => a.Key);

            clientProxy = Clients?.Clients(connectionsIds.ToList()) ?? null;

            return clientProxy != null;
        }
    }
}