using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Mobile.Services;
using EngineerProject.Mobile.Utility;
using EngineerProject.Mobile.Views.Group.Components;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Home.Pages
{
    public partial class InvitesPage : ContentPage
    {
        public InvitesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await HomePage.ReloadUserData();

            Invites.Children.Clear();
            Pending.Children.Clear();

            foreach (var group in HomePage.UserData.Invited)
                AddInvitesElement(group);

            foreach (var group in HomePage.UserData.Waiting)
                AddPendingElement(group);

            base.OnAppearing();
        }

        private void AddInvitesElement(GroupTileDto data)
        {
            var layout = new StackLayout { Orientation = StackOrientation.Horizontal };
            var label = new Label { Text = data.Name, HorizontalOptions = LayoutOptions.StartAndExpand, FontSize = NamedSize.Medium.GetFormattedLabelFontSize() };
            var acceptButton = new AppButton { Text = "Zaakceptuj", HorizontalOptions = LayoutOptions.EndAndExpand, BackgroundColor = Color.Transparent };
            var rejectButton = new AppButton { Text = "Odrzuć", BackgroundColor = Color.Transparent };
            var acceptGesture = new TapGestureRecognizer { CommandParameter = data };
            var rejectGesture = new TapGestureRecognizer { CommandParameter = data };
            var frame = new Frame { Content = layout, BackgroundColor = Color.WhiteSmoke, Padding = 6, Margin = 2 };

            acceptGesture.Tapped += (sender, args) => OnInviteAccept(sender, args);
            rejectGesture.Tapped += (sender, args) => OnInviteReject(sender, args);

            acceptButton.GestureRecognizers.Add(acceptGesture);
            rejectButton.GestureRecognizers.Add(rejectGesture);

            layout.Children.Add(label);
            layout.Children.Add(acceptButton);
            layout.Children.Add(rejectButton);

            Invites.Children.Add(frame);
        }

        private void AddPendingElement(GroupTileDto data)
        {
            var layout = new StackLayout();
            var frame = new Frame { Content = layout, BackgroundColor = Color.WhiteSmoke, Padding = 6, Margin = 2 };

            layout.Children.Add(new Label { Text = data.Name, FontSize = NamedSize.Medium.GetFormattedLabelFontSize() });

            Pending.Children.Add(frame);
        }

        private async void OnInviteAccept(object sender, EventArgs e)
        {
            var data = (e as TappedEventArgs).Parameter as GroupTileDto;
            var owner = (sender as AppButton).Parent.Parent as View;

            await ResolveInvite(owner, data, true);
        }

        private async void OnInviteReject(object sender, EventArgs e)
        {
            var data = (e as TappedEventArgs).Parameter as GroupTileDto;
            var owner = (sender as AppButton).Parent.Parent as View;

            await ResolveInvite(owner, data, true);
        }

        private async void OnLogout(object sender, EventArgs e) => await NavigationHelpers.LogoutUser();

        private async Task ResolveInvite(View owner, GroupTileDto data, bool value)
        {
            var requestResult = await new HomeService().ResolveGroupInvite(data.Id, value);

            if (requestResult.IsSuccessful)
            {
                Invites.Children.Remove(owner);
                HomePage.UserData.Invited.Remove(data);
            }
        }
    }
}