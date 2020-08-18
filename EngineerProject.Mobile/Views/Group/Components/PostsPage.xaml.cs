using EngineerProject.Mobile.Services;
using EngineerProject.Mobile.Utility;
using System;
using System.Linq;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Group.Components
{
    public partial class PostsPage : ContentPage
    {
        private int pagesLoaded = 0;

        public PostsPage()
        {
            InitializeComponent();

            LoadPosts();
        }

        private async void LoadPosts()
        {
            var service = new GroupService();

            var requestResult = await service.GetPosts(GroupPage.GroupId, pagesLoaded + 1, 10);

            if (requestResult.IsSuccessful && requestResult.Data.Any())
            {
                foreach (var post in requestResult.Data)
                    Posts.Children.Add(new Post(post));

                pagesLoaded++;
            }
        }

        private async void OnLogout(object sender, EventArgs e) => await NavigationHelpers.LogoutUser();

        private void OnScrollEndReached(object sender, ScrolledEventArgs e)
        {
            if (!(sender is ScrollView scrollView))
                return;

            var scrollingSpace = scrollView.ContentSize.Height - scrollView.Height;

            if (scrollingSpace > e.ScrollY)
                return;

            LoadPosts();
        }
    }
}