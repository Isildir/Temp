using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Mobile.Services;
using EngineerProject.Mobile.Utility;
using System;
using System.Collections.ObjectModel;
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

            CandidatesList.ItemsSource = Candidates;
        }

        private ObservableCollection<GroupCandidateDto> Candidates { get; set; } = new ObservableCollection<GroupCandidateDto>();

        protected async override void OnAppearing()
        {
            if (!initialDataLoaded)
            {
                var service = new GroupService();

                var requestResult = await service.GetAdminGroupDetails(GroupPage.GroupId);

                if (requestResult.IsSuccessful)
                {
                    Name.Text = requestResult.Data.Name;
                    Description.Text = requestResult.Data.Description;
                    Private.IsChecked = requestResult.Data.IsPrivate;

                    foreach (var candidate in requestResult.Data.Candidates)
                        Candidates.Add(candidate);
                }

                initialDataLoaded = true;
            }

            base.OnAppearing();
        }

        private async void OnCandidateAccept(object sender, EventArgs e)
        {
            var data = (sender as Button).CommandParameter as GroupCandidateDto;

            await ResolveCandidate(data, true);
        }

        private async void OnCandidateReject(object sender, EventArgs e)
        {
            var data = (sender as Button).CommandParameter as GroupCandidateDto;

            await ResolveCandidate(data, true);
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

            await service.ModifyGroup(GroupPage.GroupId, Name.Text, Description.Text, Private.IsChecked);
        }

        private async Task ResolveCandidate(GroupCandidateDto candidate, bool value)
        {
            var service = new GroupService();

            var requestResult = await service.ResolveApplication(GroupPage.GroupId, candidate.UserId, value);

            if (requestResult.IsSuccessful)
                Candidates.Remove(candidate);
        }
    }
}