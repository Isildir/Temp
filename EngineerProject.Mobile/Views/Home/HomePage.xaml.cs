using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Mobile.Services;
using EngineerProject.Mobile.Views.Groups;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Home
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            GroupsView.ItemsSource = Groups;
        }

        private ObservableCollection<GroupTileDto> Groups { get; set; } = new ObservableCollection<GroupTileDto>();

        private async void OnLogoutButtonClicked(object sender, EventArgs e) => await this.OnLogout();

        protected override async void OnAppearing()
        {
            Groups.Clear();

            var service = new HomeService();

            var userGroups = await service.GetUserGroups();

            foreach (var value in userGroups.Data.Participant)
                Groups.Add(value);

            base.OnAppearing();
        }

        private async void OnGroupSelect(object sender, ItemTappedEventArgs e)
        {
            var t = e.Item as GroupTileDto;

            await Navigation.PushAsync(new GroupPage(t.Id));
        }
    }
}