using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Mobile.Services;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Home.CarouselComponents
{
    public partial class GroupSearch : ContentPage
    {
        public GroupSearch()
        {
            InitializeComponent();

            GroupsList.ItemsSource = AvailableGroups;
        }

        private ObservableCollection<GroupTileDto> AvailableGroups { get; set; } = new ObservableCollection<GroupTileDto>();

        protected override void OnAppearing()
        {
            ReloadAvailableGroups();

            base.OnAppearing();
        }

        private async void OnGroupSelect(object sender, ItemTappedEventArgs e)
        {
            var t = e.Item as GroupTileDto;

            var answer = await DisplayAlert("Dołączenie do grupy", $"Czy na pewno chcesz dołączyć do grupy {t.Name}?", "Tak", "Nie");

            if (answer)
            {
                var requestResponse = await new HomeService().AskForInvite(t.Id);

                if (requestResponse.IsSuccessful)
                    AvailableGroups.Remove(t);
            }

            GroupsList.SelectedItem = null;
        }

        private async void OnLogout(object sender, EventArgs e) => await NavigationHelpers.LogoutUser();

        private async void ReloadAvailableGroups()
        {
            AvailableGroups.Clear();

            var availableGroups = await new HomeService().GetGroups(1, 10, SearchBarEntry.Text ?? string.Empty);

            foreach (var value in availableGroups.Data)
                AvailableGroups.Add(value);
        }

        private void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            ReloadAvailableGroups();
        }
    }
}