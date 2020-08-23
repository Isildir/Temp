using EngineerProject.Mobile.Services;
using EngineerProject.Mobile.Utility;
using EngineerProject.Mobile.Views.Group.Components;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Group.Pages
{
    public partial class PostsPage : ContentPage
    {
        private const int pageSize = 10;
        private bool dataEndReached = false;
        private bool initialDataLoaded = false;
        private int pagesLoaded = 0;

        public PostsPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            if (!initialDataLoaded)
            {
                await LoadPosts();

                initialDataLoaded = true;
            }

            base.OnAppearing();
        }

        private async Task LoadPosts()
        {
            var service = new GroupService();

            var requestResult = await service.GetPosts(GroupPage.GroupId, pagesLoaded + 1, pageSize);

            if (requestResult.IsSuccessful && requestResult.Data.Any())
            {
                foreach (var post in requestResult.Data)
                    Posts.Children.Add(new Post(post));

                pagesLoaded++;
            }
            else
                dataEndReached = true;
        }

        private async void OnLogout(object sender, EventArgs e) => await NavigationHelpers.LogoutUser();

        private async void OnPostCreateSubmit(object sender, EventArgs e)
        {
            var title = Title.Text;
            var description = Description.Text;

            var service = new GroupService();

            var requestResult = await service.AddPost(GroupPage.GroupId, title, description);

            if (requestResult.IsSuccessful)
            {
                Title.Text = string.Empty;
                Description.Text = string.Empty;

                pagesLoaded = 0;

                Posts.Children.Clear();

                await LoadPosts();
            }
        }

        private async void OnScrollEndReached(object sender, ScrolledEventArgs e)
        {
            if (dataEndReached)
                return;

            var scrollView = sender as ScrollView;

            var scrollingSpace = scrollView.ContentSize.Height - scrollView.Height;

            if (scrollingSpace > e.ScrollY + 200)
                return;

            await LoadPosts();
        }
    }
}