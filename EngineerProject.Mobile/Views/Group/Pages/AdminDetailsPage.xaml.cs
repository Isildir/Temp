using EngineerProject.Mobile.Utility;
using System;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Group.Pages
{
    public partial class AdminDetailsPage : ContentPage
    {
        public AdminDetailsPage()
        {
            InitializeComponent();
        }

        private async void OnLogout(object sender, EventArgs e) => await NavigationHelpers.LogoutUser();
    }
}