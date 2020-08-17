using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Mobile.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Home.CarouselComponents
{
    public partial class GroupInvites : ContentPage
    {
        public GroupInvites()
        {
            InitializeComponent();

            InvitesList.ItemsSource = Invites;
            AwaitingList.ItemsSource = Awaiting;
        }

        private ObservableCollection<GroupTileDto> Awaiting { get; set; } = new ObservableCollection<GroupTileDto>();

        private ObservableCollection<GroupTileDto> Invites { get; set; } = new ObservableCollection<GroupTileDto>();

        protected override async void OnAppearing()
        {
            await HomePage.ReloadUserData();

            Invites.Clear();
            Awaiting.Clear();

            foreach (var group in HomePage.UserData.Invited)
                Invites.Add(group);

            foreach (var group in HomePage.UserData.Waiting)
                Awaiting.Add(group);

            base.OnAppearing();
        }

        private async void OnInviteAccept(object sender, EventArgs e)
        {
            var data = (sender as Button).CommandParameter as GroupTileDto;

            await ResolveInvite(data, true);
        }

        private async void OnInviteReject(object sender, EventArgs e)
        {
            var data = (sender as Button).CommandParameter as GroupTileDto;

            await ResolveInvite(data, true);
        }

        private void OnLogout(object sender, EventArgs e) => App.LogoutUser();

        private async Task ResolveInvite(GroupTileDto data, bool value)
        {
            var requestResult = await new HomeService().ResolveGroupInvite(data.Id, value);

            if (requestResult.IsSuccessful)
            {
                Invites.Remove(data);
                HomePage.UserData.Invited.Remove(data);
            }
        }
    }
}