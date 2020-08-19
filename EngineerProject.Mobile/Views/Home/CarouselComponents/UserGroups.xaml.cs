using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Mobile.Utility;
using EngineerProject.Mobile.Views.Group;
using EngineerProject.Mobile.Views.Home.Pages;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Home.CarouselComponents
{
    public partial class UserGroups : ContentPage
    {
        public UserGroups()
        {
            InitializeComponent();

            GroupsView.ItemsSource = Groups;
        }

        private ObservableCollection<UserGroupTileDto> Groups { get; set; } = new ObservableCollection<UserGroupTileDto>();

        protected override async void OnAppearing()
        {
            await HomePage.ReloadUserData();

            Groups.Clear();

            foreach (var group in HomePage.UserData.Participant)
                Groups.Add(group);

            base.OnAppearing();
        }

        private async void OnGroupCreateClick(object sender, EventArgs e) => await Navigation.PushAsync(new GroupCreatePage());

        private async void OnGroupSelect(object sender, ItemTappedEventArgs e)
        {
            var groupData = e.Item as UserGroupTileDto;

            await Navigation.PushAsync(new GroupPage(groupData.Id, groupData.Name, groupData.IsOwner));
        }

        private async void OnLogout(object sender, EventArgs e) => await NavigationHelpers.LogoutUser();
    }
}