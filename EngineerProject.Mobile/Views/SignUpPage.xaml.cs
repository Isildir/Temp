using System;
using System.Linq;
using EngineerProject.Mobile.Services;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            errorMessage.IsVisible = false;

            var singupResult = await UserService.Register(usernameEntry.Text, passwordEntry.Text);

            if (!singupResult.IsSuccessful)
            {
                errorMessage.Text = singupResult.ErrorMessage;
                errorMessage.IsVisible = true;
            }
            else
                await Navigation.PopAsync();
        }
    }
}