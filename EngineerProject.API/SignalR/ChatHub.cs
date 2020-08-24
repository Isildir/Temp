using EngineerProject.API.Entities;
using EngineerProject.API.Entities.Models;
using EngineerProject.API.Utility;
using EngineerProject.Commons.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineerProject.API.SignalR
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly Dictionary<string, int> dictionary = new Dictionary<string, int>();

        private readonly IServiceProvider provider;

        public ChatHub(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;

            var httpContext = Context.GetHttpContext();
            var groupId = httpContext.Request.Query["groupId"];

            if (int.TryParse(groupId, out int value))
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

        public async Task SendMessage(string content)
        {
            var currentDate = DateTime.UtcNow;
            var groupId = dictionary[Context.ConnectionId];
            var userId = ClaimsReader.GetUserId(Context.User);

            using var scope = provider.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<EngineerContext>();

            var message = new Message
            {
                GroupId = groupId,
                SenderId = userId,
                Content = content,
                DateAdded = currentDate
            };

            dbContext.Messages.Add(message);

            try
            {
                dbContext.SaveChanges();

                var response = new MessageDto
                {
                    Content = content,
                    DateAdded = DateTime.UtcNow,
                    Owner = dbContext.Users.Find(userId).Login
                };

                if (GetReceivers(groupId, out IClientProxy clients))
                    await clients.SendAsync("appendMessage", response);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private bool GetReceivers(int id, out IClientProxy clientProxy)
        {
            var connectionsIds = dictionary.Where(a => a.Value == id).Select(a => a.Key);

            clientProxy = Clients?.Clients(connectionsIds.ToList()) ?? null;

            return clientProxy != null;
        }
    }
}