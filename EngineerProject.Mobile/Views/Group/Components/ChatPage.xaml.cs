using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Mobile.Services;
using EngineerProject.Mobile.Utility;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Group.Components
{
    public partial class ChatPage : ContentPage
    {
        private int pagesLoaded = 0;
        private const int pageSize = 10;
        private bool dataEndReached = false;

        private readonly SignalRService chatService;

        public ChatPage()
        {
            chatService = new SignalRService();
            chatService.OnMessageReceived += AppendMessage;
            chatService.ConfigureConnection(GroupPage.GroupId);

            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            await LoadMessages();
            await chatService.StartConnection();

            base.OnAppearing();
        }

        private async void OnLogout(object sender, EventArgs e) => await NavigationHelpers.LogoutUser();

        private void AppendMessage(object sender, MessageDto message)
        {
            var layout = new StackLayout();
            var secondLayout = new StackLayout { Orientation = StackOrientation.Horizontal };

            secondLayout.Children.Add(new Label { Text = message.Owner, HorizontalOptions = LayoutOptions.StartAndExpand });
            secondLayout.Children.Add(new Label { Text = message.DateAdded.ToString(), HorizontalOptions = LayoutOptions.End });

            layout.Children.Add(new Label { Text = message.Content });
            layout.Children.Add(secondLayout);

            Messages.Children.Insert(0, layout);
        }

        private async Task LoadMessages()
        {
            var t = MessagesWrapper.ScrollY;

            var service = new GroupService();
            
            var requestResult = await service.GetMessages(GroupPage.GroupId, pagesLoaded + 1, pageSize);

            if (requestResult.IsSuccessful && requestResult.Data.Any())
            {
                foreach (var message in requestResult.Data)
                    AppendMessage(null, message);

                pagesLoaded++;

                await MessagesWrapper.ScrollToAsync(0, t, false);

            }
            else
                dataEndReached = true;
        }

        private async void OnScrollEndReached(object sender, ScrolledEventArgs e)
        {
            if (!(sender is ScrollView scrollView))
                return;

            var scrollingSpace = scrollView.ContentSize.Height - scrollView.Height;

            if (scrollingSpace > e.ScrollY || dataEndReached)
                return;

            await LoadMessages();
        }

        private async void OnMessageSend(object sender, EventArgs e)
        {
            await chatService.SendMessage(Message.Text);

            Message.Text = string.Empty;
        }
    }
}