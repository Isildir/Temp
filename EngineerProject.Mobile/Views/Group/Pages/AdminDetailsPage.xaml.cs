using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Mobile.Components;
using EngineerProject.Mobile.Services;
using EngineerProject.Mobile.Utility;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Group.Pages
{
    public partial class AdminDetailsPage : ContentPage
    {
        private bool initialDataLoaded = false;

        public AdminDetailsPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            if (!initialDataLoaded)
            {
                await LoadData();

                initialDataLoaded = true;
            }

            base.OnAppearing();
        }

        private void AddCandidate(GroupCandidateDto data) => Candidates.Children.Add(ComponentsBuilder.BuildAcceptableFrame(data.UserLogin, data, OnCandidateAccept, OnCandidateReject));

        private async Task LoadData()
        {
            var service = new GroupService();

            var requestResult = await service.GetAdminGroupDetails(GroupPage.GroupId);

            if (requestResult.IsSuccessful)
            {
                Name.Text = requestResult.Data.Name;
                Description.Text = requestResult.Data.Description;
                IsPrivate.IsChecked = requestResult.Data.IsPrivate;

                foreach (var candidate in requestResult.Data.Candidates)
                    AddCandidate(candidate);
            }
        }

        private async void OnCandidateAccept(object sender, EventArgs e)
        {
            var data = (e as TappedEventArgs).Parameter as GroupCandidateDto;
            var owner = (sender as AppButton).Parent.Parent as View;

            await ResolveCandidate(owner, data, true);
        }

        private async void OnCandidateReject(object sender, EventArgs e)
        {
            var data = (e as TappedEventArgs).Parameter as GroupCandidateDto;
            var owner = (sender as AppButton).Parent.Parent as View;

            await ResolveCandidate(owner, data, true);
        }

        private async void OnInvite(object sender, EventArgs e)
        {
            var service = new GroupService();

            var requestResult = await service.InviteUser(GroupPage.GroupId, InviteBar.Text);

            if (requestResult.IsSuccessful)
                InviteBar.Text = string.Empty;
        }

        private async void OnLogout(object sender, EventArgs e) => await NavigationHelpers.LogoutUser();

        private async void OnSubmitChanges(object sender, EventArgs e)
        {
            var service = new GroupService();

            await service.ModifyGroup(GroupPage.GroupId, Name.Text, Description.Text, IsPrivate.IsChecked);
        }

        private async Task ResolveCandidate(View owner, GroupCandidateDto candidate, bool value)
        {
            var service = new GroupService();

            var requestResult = await service.ResolveApplication(GroupPage.GroupId, candidate.UserId, value);

            if (requestResult.IsSuccessful)
                Candidates.Children.Remove(owner);
        }
    }
}