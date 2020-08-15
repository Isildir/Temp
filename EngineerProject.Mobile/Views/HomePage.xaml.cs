using System;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            //App.IsUserLoggedIn = false;
            //App.Token = null;

            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }

        private async void OnProfessionsButtonClicked(object sender, EventArgs e)
        {
        }

        private async void OnProfileButtonClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new ProfilePage());
        }
    }
}