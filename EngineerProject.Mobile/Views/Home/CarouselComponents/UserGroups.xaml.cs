using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Mobile.Views.Groups;
using EngineerProject.Mobile.Views.Home.Create;
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

        private ObservableCollection<GroupTileDto> Groups { get; set; } = new ObservableCollection<GroupTileDto>();

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
            var t = e.Item as GroupTileDto;

            await Navigation.PushAsync(new GroupPage(t.Id));
        }

        private void OnLogout(object sender, EventArgs e) => App.LogoutUser();
    }
}