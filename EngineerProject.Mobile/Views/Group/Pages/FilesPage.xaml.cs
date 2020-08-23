using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Mobile.Services;
using EngineerProject.Mobile.Utility;
using EngineerProject.Mobile.Views.Group.Components;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Group.Pages
{
    public partial class FilesPage : ContentPage
    {
        private const int pageSize = 12;
        private bool dataEndReached = false;
        private int elementsCount = 0;
        private bool initialDataLoaded = false;
        private int pagesLoaded = 0;

        public FilesPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            if (!initialDataLoaded)
            {
                await LoadFiles();

                initialDataLoaded = true;
            }

            base.OnAppearing();
        }

        private async Task LoadFiles()
        {
            var service = new FileService();

            var requestResult = await service.GetFiles(GroupPage.GroupId, pagesLoaded + 1, pageSize);

            if (requestResult.IsSuccessful && requestResult.Data.Any())
            {
                foreach (var file in requestResult.Data)
                {
                    if (elementsCount % 3 == 0)
                        Files.RowDefinitions.Insert(0, new RowDefinition());

                    Files.Children.Add(new AppFile(file, OnFileClicked), elementsCount % 3, elementsCount / 3);

                    elementsCount++;
                }

                pagesLoaded++;
            }
            else
                dataEndReached = true;
        }

        private async void OnFileClicked(object sender, EventArgs e)
        {
            var file = (e as TappedEventArgs).Parameter as FileDto;

            var answer = await DisplayAlert("Pobieranie pliku", $"Czy chcesz pobrać plik {file.FileName}?", "Tak", "Nie");

            if (answer)
            {
                //TODO
            }
        }

        private async void OnLogout(object sender, EventArgs e) => await NavigationHelpers.LogoutUser();

        private async void OnScrollEndReached(object sender, ScrolledEventArgs e)
        {
            if (dataEndReached)
                return;

            var scrollView = sender as ScrollView;

            var scrollingSpace = scrollView.ContentSize.Height - scrollView.Height;

            if (scrollingSpace > e.ScrollY + 200)
                return;

            await LoadFiles();
        }
    }
}