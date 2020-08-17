using EngineerProject.Mobile.Services;
using System;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Home.Create
{
    public partial class GroupCreatePage : ContentPage
    {
        public GroupCreatePage()
        {
            InitializeComponent();
        }

        private void OnLogout(object sender, EventArgs e) => App.LogoutUser();

        private async void OnSubmit(object sender, System.EventArgs e)
        {
            var requestResult = await new HomeService().Create(Name.Text, Description.Text, IsPrivate.IsChecked);

            if (requestResult.IsSuccessful)
                await Navigation.PopAsync();
        }
    }
}