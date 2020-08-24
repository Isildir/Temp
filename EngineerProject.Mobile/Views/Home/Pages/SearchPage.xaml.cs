using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Mobile.Services;
using EngineerProject.Mobile.Utility;
using System;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Home.Pages
{
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ReloadAvailableGroups();

            base.OnAppearing();
        }

        private void AddGroup(GroupTileDto data)
        {
            var label = new Label { Text = data.Name, FontSize = NamedSize.Medium.GetFormattedLabelFontSize() };
            var gesture = new TapGestureRecognizer { CommandParameter = data };
            var frame = new Frame { Content = label, BackgroundColor = Color.WhiteSmoke, Padding = 6, Margin = 2 };

            gesture.Tapped += (sender, args) => OnGroupSelect(sender, args);
            label.GestureRecognizers.Add(gesture);

            FilteredGroups.Children.Add(frame);
        }

        private async void OnGroupSelect(object sender, EventArgs e)
        {
            var selectedItem = (e as TappedEventArgs).Parameter as GroupTileDto;

            var answer = await DisplayAlert("Dołączenie do grupy", $"Czy na pewno chcesz dołączyć do grupy {selectedItem.Name}?", "Tak", "Nie");

            if (answer)
            {
                var requestResponse = await new HomeService().AskForInvite(selectedItem.Id);

                if (requestResponse.IsSuccessful)
                    ReloadAvailableGroups();
            }
        }

        private async void OnLogout(object sender, EventArgs e) => await NavigationHelpers.LogoutUser();

        private async void ReloadAvailableGroups()
        {
            FilteredGroups.Children.Clear();

            var availableGroups = await new HomeService().GetGroups(1, 10, SearchBarEntry.Text ?? string.Empty);

            foreach (var value in availableGroups.Data)
                AddGroup(value);
        }

        private void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            ReloadAvailableGroups();
        }
    }
}