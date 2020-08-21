using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Mobile.Utility;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace EngineerProject.Mobile.Services
{
    public class SignalRService
    {
        private HubConnection connection;

        private Task<string> GetToken() => Task.Run(() => ConfigurationData.Token);

        public EventHandler<MessageDto> OnMessageReceived;

        public void ConfigureConnection(int groupId)
        {
            var url = $"{ConfigurationData.ChatUrl}?groupId={groupId}";

            connection = new HubConnectionBuilder().WithUrl(url, options => options.AccessTokenProvider = GetToken).WithAutomaticReconnect().Build();

            connection.On<MessageDto>("appendMessage", data => OnMessageReceived.Invoke(this, data));
        }

        public async Task StartConnection()
        {
            await connection.StartAsync();

            var t = connection.State;
        }

        public async Task SendMessage(string content) => await connection.SendAsync("SendMessage", content);
    }
}