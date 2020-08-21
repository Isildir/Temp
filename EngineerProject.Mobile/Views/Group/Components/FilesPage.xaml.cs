using EngineerProject.Mobile.Services;
using EngineerProject.Mobile.Utility;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Group.Components
{
    public partial class FilesPage : ContentPage
    {
        private const int pageSize = 12;
        private bool dataEndReached = false;
        private int pagesLoaded = 0;
        private int elementsCount = 0;

        public FilesPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            await LoadFiles();

            base.OnAppearing();
        }

        private async void OnLogout(object sender, EventArgs e) => await NavigationHelpers.LogoutUser();

        private async Task LoadFiles()
        {
            var service = new FileService();

            var requestResult = await service.GetFiles(GroupPage.GroupId, pagesLoaded + 1, pageSize);

            if (requestResult.IsSuccessful && requestResult.Data.Any())
            {
                foreach (var file in requestResult.Data)
                {
                    var layout = new StackLayout();

                    layout.Children.Add(new Image { Aspect = Aspect.AspectFit, Source = "icon.png" });
                    layout.Children.Add(new Label { Text = file.FileName });
                    layout.Children.Add(new Label { Text = file.Size });

                    if (elementsCount % 3 == 0)
                        Files.RowDefinitions.Insert(0, new RowDefinition());

                    Files.Children.Add(layout, elementsCount % 3, elementsCount / 3);

                    elementsCount++;
                }

                pagesLoaded++;
            }
            else
                dataEndReached = true;
        }

        private async void OnScrollEndReached(object sender, ScrolledEventArgs e)
        {
            if (!(sender is ScrollView scrollView))
                return;

            var scrollingSpace = scrollView.ContentSize.Height - scrollView.Height;

            if (scrollingSpace > e.ScrollY || dataEndReached)
                return;

            await LoadFiles();
        }
    }
}