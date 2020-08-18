using EngineerProject.Mobile.Utility;
using System;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Group.Components
{
    public partial class FilesPage : ContentPage
    {
        public FilesPage()
        {
            InitializeComponent();
        }

        private async void OnLogout(object sender, EventArgs e) => await NavigationHelpers.LogoutUser();
    }
}