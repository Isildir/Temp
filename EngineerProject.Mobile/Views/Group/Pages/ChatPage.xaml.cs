using EngineerProject.Commons.Dtos;
using EngineerProject.Mobile.Services;
using EngineerProject.Mobile.Utility;
using EngineerProject.Mobile.Views.Group.Components;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Group.Pages
{
    public partial class ChatPage : ContentPage
    {
        private const int pageSize = 10;
        private SignalRService chatService;
        private bool dataEndReached = false;
        private bool initialDataLoaded = false;
        private int pagesLoaded = 0;

        public ChatPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            if (!initialDataLoaded)
            {
                chatService = new SignalRService();
                chatService.OnMessageReceived += AppendMessage;
                chatService.ConfigureConnection(GroupPage.GroupId);

                await LoadMessages();

                initialDataLoaded = true;
            }

            if (!chatService.IsConnected)
                await chatService.StartConnection();

            base.OnAppearing();
        }

        private void AppendMessage(object sender, MessageDto message) => Messages.Children.Insert(0, new Message(message));

        private async Task LoadMessages()
        {
            var service = new GroupService();

            var requestResult = await service.GetMessages(GroupPage.GroupId, pagesLoaded + 1, pageSize);

            if (requestResult.IsSuccessful && requestResult.Data.Any())
            {
                foreach (var message in requestResult.Data)
                    Messages.Children.Add(new Message(message));

                pagesLoaded++;
            }
            else
                dataEndReached = true;
        }

        private async void OnLogout(object sender, EventArgs e) => await NavigationHelpers.LogoutUser();

        private async void OnMessageSend(object sender, EventArgs e)
        {
            await chatService.SendMessage(Message.Text);

            Message.Text = string.Empty;
        }

        private async void OnScrollEndReached(object sender, ScrolledEventArgs e)
        {
            if (dataEndReached)
                return;

            var scrollView = sender as ScrollView;

            var scrollingSpace = scrollView.ContentSize.Height - scrollView.Height;

            if (scrollingSpace > e.ScrollY + 200)
                return;

            await LoadMessages();
        }
    }
}