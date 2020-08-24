using EngineerProject.Commons.Dtos;
using EngineerProject.Mobile.Utility;
using EngineerProject.Mobile.Views.Group;
using System;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Home.Pages
{
    public partial class UserGroupsPage : ContentPage
    {
        public UserGroupsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await HomePage.ReloadUserData();

            UserGroups.Children.Clear();

            foreach (var group in HomePage.UserData.Participant)
                AddGroup(group);

            base.OnAppearing();
        }

        private void AddGroup(GroupTileDto data)
        {
            var label = new Label { Text = data.Name, FontSize = NamedSize.Medium.GetFormattedLabelFontSize() };
            var gesture = new TapGestureRecognizer { CommandParameter = data };
            var frame = new Frame { Content = label, BackgroundColor = Color.WhiteSmoke, Padding = 6, Margin = 2 };

            gesture.Tapped += (sender, args) => OnGroupSelect(sender, args);
            label.GestureRecognizers.Add(gesture);

            UserGroups.Children.Add(frame);
        }

        private async void OnGroupCreateClick(object sender, EventArgs e) => await Navigation.PushAsync(new GroupCreatePage());

        private async void OnGroupSelect(object sender, EventArgs e)
        {
            var selectedItem = (e as TappedEventArgs).Parameter as UserGroupTileDto;

            await Navigation.PushAsync(new GroupPage(selectedItem.Id, selectedItem.Name, selectedItem.IsOwner));
        }

        private async void OnLogout(object sender, EventArgs e) => await NavigationHelpers.LogoutUser();
    }
}