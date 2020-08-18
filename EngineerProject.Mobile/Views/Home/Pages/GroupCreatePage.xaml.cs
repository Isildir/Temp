using EngineerProject.Mobile.Services;
using EngineerProject.Mobile.Utility;
using System;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Home.Pages
{
    public partial class GroupCreatePage : ContentPage
    {
        public GroupCreatePage()
        {
            InitializeComponent();
        }

        private async void OnLogout(object sender, EventArgs e) => await NavigationHelpers.LogoutUser();

        private async void OnSubmit(object sender, EventArgs e)
        {
            var requestResult = await new HomeService().Create(Name.Text, Description.Text, IsPrivate.IsChecked);

            if (requestResult.IsSuccessful)
                await Navigation.PopAsync();
        }
    }
}